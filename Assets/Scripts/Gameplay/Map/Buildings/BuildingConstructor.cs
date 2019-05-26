using Assets.Scripts.Gameplay.Resources;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gameplay.Map.Buildings
{
    [RequireComponent(typeof(MapBuilding))]
    public class BuildingConstructor : MonoBehaviour
    {
        public ResourceAmount[] BuildingCostAmountArray;
        public MapBuilding Building;
        public Slider ConstructionSlider;
        public float ConstructionTime = 10f;

        public void StartConstruction()
        {
            PayResourceCost();
            Building.State = BuildState.Constructing;
            StartCoroutine(ConstructionRoutine());
        }

        private void PayResourceCost()
        {
            ResourceManager resourceManager = LevelManager.Instance.Resources;

            foreach (ResourceAmount resourceAmount in BuildingCostAmountArray)
            {
                resourceManager.TryPayResource(resourceAmount.Type, resourceAmount.Amount);
            }
        }

        public bool HasEnoughResources()
        {
            ResourceManager resourceManager = LevelManager.Instance.Resources;
            return resourceManager.HasEnoughResourceArray(BuildingCostAmountArray);
        }

        private IEnumerator ConstructionRoutine()
        {
            float startTime = Time.time;
            float timePassed = 0f;
            while (timePassed < ConstructionTime)
            {
                float timeRatio = Mathf.Clamp01(timePassed / ConstructionTime);
                ConstructionSlider.value = timeRatio;
                yield return null;
                timePassed = Time.time - startTime;
            }


            Building.State = BuildState.Finished;
        }

    }
}
