using UnityEngine;

namespace Assets.Scripts.UI.Map
{
    public class SetUIModeButton : MonoBehaviour
    {
        public MapUIManager UIManager;

        public void SetBuildMode()
        {
            UIManager.UIMode = UIMode.Build;
        }

        public void SetRegularMode()
        {
            UIManager.UIMode = UIMode.Regular;
        }

    }
}
