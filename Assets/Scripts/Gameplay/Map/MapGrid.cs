using UnityEngine;

namespace Assets.Scripts.Gameplay.Map
{
    public class MapGrid
    {
        public Vector2Int MapSize { get; private set; }
        private bool[][] _occupationGrid;


        public MapGrid(Vector2Int size)
        {
            MapSize = size;
            _occupationGrid = new bool[size.x][];
            for (int i = 0; i < size.x; i++)
            {
                _occupationGrid[i] = new bool[size.y];
            }
        }


        public bool InsertIntoGrid(Vector2Int origin, Vector2Int size)
        {
            if (!IsSlotAvailable(origin, size))
                return false;

            SetGrid(origin, size, true);
            return true;
        }

        public bool RemoveFromGrid(Vector2Int origin, Vector2Int size)
        {
            SetGrid(origin, size, false);
            return true;
        }

        public bool IsSlotAvailable(Vector2Int origin, Vector2Int size)
        {
            int startX = origin.x;
            int startY = origin.y;

            int endX = startX + size.x;
            int endY = startY + size.y;

            if (startX < 0 || endX >= MapSize.x ||
                startY < 0 || endY >= MapSize.y)
            {
                // Out of bounds
                return false;
            }


            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    if (_occupationGrid[x][y])
                        return false;
                }
            }

            return true;
        }


        private void SetGrid(Vector2Int origin, Vector2Int size, bool occupation)
        {
            int startX = origin.x;
            int startY = origin.y;

            int endX = startX + size.x;
            int endY = startY + size.y;

            if (startX < 0 || endX >= MapSize.x ||
                startY < 0 || endY >= MapSize.y)
            {
                // Out of bounds
                return;
            }


            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    _occupationGrid[x][y] = occupation;
                }
            }
        }
    }
}
