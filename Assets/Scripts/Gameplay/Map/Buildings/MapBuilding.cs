using Assets.Scripts.Gameplay.Map.Buildings.Production;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Map.Buildings
{
    public enum BuildingType
    {
        Residence,
        WoodProduction,
        SteelProduction
    }

    public enum BuildState
    {
        Placement,
        Constructing,
        Finished
    }

    public class MapBuilding : MonoBehaviour
    {
        public MapManager Map;
        public Vector2Int Size;
        public Vector2Int CurrentPosition;

        [SerializeField]
        private BuildState _state;
        public BuildingType Type;

        public BuildingDragger Drag;
        public BuildingClick Click;
        public BuildingPlace Place;
        public AutoTimedProduction Production;
        public BuildingCanvas Canvas;
        public BuildingConstructor Construction;

        public BuildState State
        {
            get { return _state; }
            set
            {
                _state = value;
                SetState();
            }
        }

        private void Start()
        {
            SetState();
        }

        private void SetState()
        {
            Click.Active = State == BuildState.Finished;

            Place.enabled = State == BuildState.Placement;
            Canvas.State = _state;

            if (Production != null) Production.Active = State == BuildState.Finished;
        }


        public void SetMapGridPosition(MapGridPosition position)
        {
            CurrentPosition = position.GridPosition;
            SetAdjustedPosition(position.WorldPosition);
            Map.Grid.InsertIntoGrid(position.GridPosition, Size);
        }


        public Vector3 GetTileTopLeft()
        {
            Vector2 worldTileSize = Map.WorldTileSize;
            Vector2 halfSize = (worldTileSize * Size) / 2f;
            Vector3 result = transform.position;
            result.x -= halfSize.x;
            result.z += halfSize.y;
            return result;
        }

        public void SetAdjustedPosition(Vector3 position)
        {
            Vector2 worldTileSize = Map.WorldTileSize;
            Vector2 halfSize = (worldTileSize * Size) / 2f;
            position.x += halfSize.x;
            position.z -= halfSize.y;
            transform.position = position;
        }
    }
}
