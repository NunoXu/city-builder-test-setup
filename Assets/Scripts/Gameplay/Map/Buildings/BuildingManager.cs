using Assets.Scripts.UI.Map;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Map.Buildings
{
    public class BuildingManager : MonoBehaviour
    {
        public MapManager Map;
        public Transform BuildingContainer;
        public MapBuilding[] BuildPrefabArray;

        private List<MapBuilding> _spawnedMapBuildingList;
        private MapBuilding _currentPlacement;

        private void Start()
        {
            MapBuilding[] buildingArray = FindObjectsOfType<MapBuilding>();
            foreach (MapBuilding building in buildingArray)
            {
                Map.InsertBuildingIntoMap(building);
            }
            _spawnedMapBuildingList = new List<MapBuilding>(buildingArray);
        }

        public void SetUIMode(UIMode uiMode)
        {
            foreach (MapBuilding building in _spawnedMapBuildingList)
            {
                building.Drag.Active = uiMode == UIMode.Build;
            }
        }


        public bool HasEnoughResourcesToSpawn(BuildingType type)
        {
            if (BuildPrefabArray != null && _currentPlacement == null)
            {
                foreach (MapBuilding buildingPrefab in BuildPrefabArray)
                {
                    if (buildingPrefab.Type == type)
                    {
                        return buildingPrefab.Construction.HasEnoughResources();
                    }
                }
            }

            return false;
        }

        public MapBuilding SpawnMapBuilding(BuildingType type)
        {
            MapBuilding buildingInstance = null;
            if (BuildPrefabArray != null && _currentPlacement == null)
            {
                foreach (MapBuilding buildingPrefab in BuildPrefabArray)
                {
                    if (buildingPrefab.Type == type)
                    {
                        buildingInstance = Instantiate(buildingPrefab, BuildingContainer, false);
                        buildingInstance.Map = Map;
                        _currentPlacement = buildingInstance;
                        break;
                    }
                }
            }

            return buildingInstance;
        }

        public void ClearPlacement()
        {
            _currentPlacement = null;
        }
    }
}
