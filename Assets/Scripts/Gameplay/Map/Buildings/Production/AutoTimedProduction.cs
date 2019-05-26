using Assets.Scripts.Gameplay.Resources;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gameplay.Map.Buildings.Production
{
    public class AutoTimedProduction : MonoBehaviour
    {
        public ResourceType Resource;
        public float ProductionPerSecond;
        public float ProductionPeriodicity;

        public bool MidProduction { get { return _productionRoutine != null; } }
        public bool AutoTrigger = false;

        public Button ActivateProductionButton;
        public Slider ProductionSlider;
        private Coroutine _productionRoutine = null;

        private bool _active;
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                SetActive();
            }
        }

        private void OnDisable()
        {
            _productionRoutine = null;
        }

        public void ActivateProduction()
        {
            if (_active)
                _productionRoutine = StartCoroutine(ProductionRoutine());
        }

        private void SetActive()
        {
            if (_active && _productionRoutine == null && AutoTrigger)
                ActivateProduction();

            if (!_active && _productionRoutine != null)
            {
                StopCoroutine(_productionRoutine);
                _productionRoutine = null;
            }
        }

        private IEnumerator ProductionRoutine()
        {
            SetProductionUI(true);

            float startTime = Time.time;
            float timePassed = 0f;
            while (timePassed < ProductionPeriodicity)
            {
                float timeRatio = Mathf.Clamp01(timePassed / ProductionPeriodicity);
                ProductionSlider.value = timeRatio;
                yield return null;
                timePassed = Time.time - startTime;
            }

            int production = Mathf.CeilToInt(ProductionPerSecond * ProductionPeriodicity);
            LevelManager.Instance.Resources.AddResource(Resource, production);

            if (AutoTrigger)
            {
                _productionRoutine = StartCoroutine(ProductionRoutine());
            }
            else
            {
                SetProductionUI(false);
            }
        }

        private void SetProductionUI(bool active)
        {
            ProductionSlider.gameObject.SetActive(active);
            ActivateProductionButton.gameObject.SetActive(!active);
        }
    }
}
