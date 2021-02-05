using UnityEngine;

namespace General
{
    public class GameState : MonoBehaviour
    {
        private int _paused;

        public bool GetPaused()
        {
            return _paused > 0;
        }

        public void SetPaused(bool paused)
        {
            if (paused)
                _paused += 1;
            else if (_paused > 0)
                _paused -= 1;
        }
    }
}