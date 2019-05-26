using Assets.Scripts.Gameplay;
using Assets.Scripts.Gameplay.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ResourceBinder : MonoBehaviour
    {
        public Text ResourceText;
        public ResourceType Type;

        public void Start()
        {
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.Resources.BindResource(Type, ResourceBinding);
            }
        }

        private void ResourceBinding(int amount)
        {
            ResourceText.text = string.Format("[{0}]", amount);
        }
    }
}
