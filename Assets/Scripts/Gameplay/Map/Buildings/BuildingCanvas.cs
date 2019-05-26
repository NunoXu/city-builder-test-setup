using UnityEngine;

namespace Assets.Scripts.Gameplay.Map.Buildings
{
    [ExecuteInEditMode]
    public class BuildingCanvas : MonoBehaviour
    {
        [SerializeField]
        private BuildState _state;
        public BuildState State
        {
            get { return _state; }
            set
            {
                _state = value;
                SetState();
            }
        }

        public RectTransform ConstructionUIContainer;
        public RectTransform ProductionUIContainer;

        private void Start()
        {
            SetState();
        }

        private void SetState()
        {
            //auto activate canvas if in construct
            gameObject.SetActive(gameObject.activeSelf || _state == BuildState.Constructing);

            ConstructionUIContainer.gameObject.SetActive(_state == BuildState.Constructing);
            ProductionUIContainer.gameObject.SetActive(_state == BuildState.Finished);
        }



#if UNITY_EDITOR
        private void Update()
        {
            if (!Application.isPlaying)
            {
                SetState();
            }
        }
#endif
    }
}
