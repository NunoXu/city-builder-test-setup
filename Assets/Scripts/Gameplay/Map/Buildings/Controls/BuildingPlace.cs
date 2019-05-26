using UnityEngine;

namespace Assets.Scripts.Gameplay.Map.Buildings
{
    [RequireComponent(typeof(MapBuilding))]
    public class BuildingPlace : MonoBehaviour
    {
        public MapBuilding Building;
        private Camera _mainCamera;
        private int _floorMask;

        private void Awake()
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<Camera>();
            _floorMask = LayerMask.GetMask("MapFloor");
        }

        private void Update()
        {
            PreviewBuildingPosition();
            if (Input.GetMouseButtonDown(0))
            {
                SetBuildingPosition();
            }
            if (Input.GetButtonDown("Cancel"))
            {
                LevelManager.Instance.Buildings.ClearPlacement();
                LevelManager.Instance.Buildings.DespawnMapBuilding(Building);
            }
        }

        private void SetBuildingPosition()
        {
            if (TryGetMouseGridPosition(out MapGridPosition closestGridPosition))
            {
                bool enoughResources = Building.Construction.HasEnoughResources();
                if (!enoughResources)
                {
                    Debug.Log("Not enough resources!");
                    return;
                }

                bool isSlotAvailable = Building.Map.Grid.IsSlotAvailable(closestGridPosition.GridPosition, Building.Size);
                if (isSlotAvailable)
                {
                    Building.SetMapGridPosition(closestGridPosition);
                    Building.Construction.StartConstruction();
                    LevelManager.Instance.Buildings.ClearPlacement();
                }
            }
        }

        private void PreviewBuildingPosition()
        {
            if (TryGetMouseGridPosition(out MapGridPosition closestGridPosition))
                Building.SetAdjustedPosition(closestGridPosition.WorldPosition);
        }

        private bool TryGetMouseGridPosition(out MapGridPosition result)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _mainCamera.farClipPlane, _floorMask))
            {
                Vector3 mousePosition = hitInfo.point;
                result = Building.Map.GetClosestGridPosition(mousePosition);
                return true;
            }

            result = default;
            return false;
        }

    }
}
