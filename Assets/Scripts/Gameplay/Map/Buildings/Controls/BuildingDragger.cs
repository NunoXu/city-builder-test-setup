using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gameplay.Map.Buildings
{
    [RequireComponent(typeof(MapBuilding))]
    public class BuildingDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public MapBuilding Building;
        public bool Active = false;
        private Vector3 _startingPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!Active)
                return;
            _startingPosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!Active)
                return;

            MapGridPosition closestPosition = GetClosestMouseGridPosition(eventData);
            Building.SetAdjustedPosition(closestPosition.WorldPosition);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!Active)
                return;

            MapGridPosition closestPosition = GetClosestMouseGridPosition(eventData);
            Vector2Int gridPosition = closestPosition.GridPosition;
            Building.Map.Grid.RemoveFromGrid(Building.CurrentPosition, Building.Size);
            if (Building.Map.Grid.IsSlotAvailable(gridPosition, Building.Size))
            {
                Building.SetMapGridPosition(closestPosition);
            }
            else
            {
                Building.Map.Grid.InsertIntoGrid(Building.CurrentPosition, Building.Size);
                transform.position = _startingPosition;
            }
        }

        private MapGridPosition GetClosestMouseGridPosition(PointerEventData eventData)
        {
            Vector3 mouseWorldPosition = eventData.pointerCurrentRaycast.worldPosition;
            return Building.Map.GetClosestGridPosition(mouseWorldPosition);
        }
    }
}
