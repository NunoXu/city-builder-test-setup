using Assets.Scripts.Gameplay;
using Assets.Scripts.Gameplay.Map.Buildings;
using UnityEngine;

namespace Assets.Scripts.UI.Map.Building
{
    public class BuyBuildingButton : MonoBehaviour
    {
        public BuildingType TargetBuildingType;

        public void BuyBuilding()
        {
            BuildingManager buildingManager = LevelManager.Instance.Buildings;
            if (buildingManager.HasEnoughResourcesToSpawn(TargetBuildingType))
                buildingManager.SpawnMapBuilding(TargetBuildingType);
            else
                Debug.Log("Not enought resources");
        }

    }
}
