using Assets.Scripts.Gameplay;
using System;
using UnityEngine;

namespace Assets.Scripts.UI.Map
{
    public enum UIMode
    {
        Regular,
        Build
    }

    [Serializable]
    public struct ModeObjectStruct
    {
        public UIMode UIMode;
        public Transform[] ObjectArray;
    }

    [ExecuteInEditMode]
    public class MapUIManager : MonoBehaviour
    {
        [SerializeField]
        private UIMode _uiMode;
        public UIMode UIMode
        {
            get { return _uiMode; }
            set
            {
                _uiMode = value;
                SetUIMode();
            }
        }

        public ModeObjectStruct[] ModeObjectArray;


        private void SetUIMode()
        {
            if (ModeObjectArray != null)
            {
                foreach (ModeObjectStruct modeObject in ModeObjectArray)
                {
                    if (modeObject.ObjectArray != null)
                    {
                        bool isCorrectMode = modeObject.UIMode == _uiMode;
                        foreach (Transform objectTransform in modeObject.ObjectArray)
                        {
                            objectTransform.gameObject.SetActive(isCorrectMode);
                        }
                    }
                }
            }

            if (LevelManager.Instance != null)
                LevelManager.Instance.Buildings.SetUIMode(_uiMode);
        }


#if UNITY_EDITOR
        private void Update()
        {
            if (!Application.isPlaying)
            {
                SetUIMode();
            }
        }
#endif
    }
}
