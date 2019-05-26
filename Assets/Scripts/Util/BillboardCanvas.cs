using UnityEngine;

namespace Assets.Scripts.Util
{
    [RequireComponent(typeof(Canvas)), ExecuteInEditMode]
    public class BillboardCanvas : MonoBehaviour
    {
        public Canvas Canvas;
        public Transform TargetTransform;
        public Camera TargetCamera;

        public void Awake()
        {
            if (TargetTransform == null)
                TargetTransform = transform;

            if (TargetCamera == null)
                TargetCamera = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<Camera>();
            if (Canvas.worldCamera == null)
                Canvas.worldCamera = TargetCamera;
        }

        private void Update()
        {
            TargetTransform.rotation = TargetCamera.transform.rotation;
        }
    }
}
