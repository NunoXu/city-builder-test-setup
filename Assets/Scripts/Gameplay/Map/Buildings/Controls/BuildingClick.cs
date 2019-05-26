using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Gameplay.Map.Buildings
{
    public class BuildingClick : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler
    {
        public BuildingCanvas BuildingCanvas;
        public bool Active = false;
        private bool _dragging;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragging = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_dragging && Active)
                BuildingCanvas.gameObject.SetActive(!BuildingCanvas.gameObject.activeSelf);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _dragging = false;
        }
    }
}
