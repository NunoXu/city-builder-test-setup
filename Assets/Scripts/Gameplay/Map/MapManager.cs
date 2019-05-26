using Assets.Scripts.Gameplay.Map.Buildings;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Map
{
    public struct MapGridPosition
    {
        public Vector2Int GridPosition;
        public Vector3 WorldPosition;

        public MapGridPosition(Vector2Int gridPosition, Vector3 worldPosition)
        {
            GridPosition = gridPosition;
            WorldPosition = worldPosition;
        }
    }

    public class MapManager : MonoBehaviour
    {
        public Vector2Int MapSize = new Vector2Int(12, 12);
        public Vector2 WorldTileSize = new Vector2(10, 10);

        public MapGrid Grid;

        private void Awake()
        {
            Grid = new MapGrid(MapSize);
        }

        public void InsertBuildingIntoMap(MapBuilding building)
        {
            var closestGridPosition = GetClosestGridPosition(building.GetTileTopLeft());
            building.SetMapGridPosition(closestGridPosition);
        }

        public MapGridPosition GetClosestGridPosition(Vector3 position)
        {
            float xPosition = position.x;
            float zPosition = position.z;

            int closestX = Mathf.FloorToInt(xPosition / WorldTileSize.x);
            int closestZ = Mathf.FloorToInt(Mathf.Abs(zPosition) / WorldTileSize.y);

            Vector2Int gridPosition = new Vector2Int(closestX, closestZ);
            Vector3 worldPosition = new Vector3(gridPosition.x * WorldTileSize.x, 0, -gridPosition.y * WorldTileSize.y);


            return new MapGridPosition(gridPosition, worldPosition);
        }

    }
}
