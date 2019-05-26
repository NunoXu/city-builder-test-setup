using Assets.Scripts.Gameplay.Map;
using Assets.Scripts.Gameplay.Map.Buildings;
using Assets.Scripts.Gameplay.Resources;
using Assets.Scripts.Util;

namespace Assets.Scripts.Gameplay
{
    public class LevelManager : SingletonMonoBehaviour<LevelManager>
    {
        public BuildingManager Buildings;

        public MapManager Map;
        public ResourceManager Resources;
    }
}
