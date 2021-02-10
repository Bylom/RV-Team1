using UnityEngine;

namespace General
{
    public class GameState : MonoBehaviour
    {
        private int _paused;
        private int _mouseNeeded;

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

        public bool GetMouseNeeded()
        {
            return _mouseNeeded > 0;
        }
        
        public void SetMouseNeeded(bool mouse)
        {
            if (mouse)
                _mouseNeeded += 1;
            else if (_mouseNeeded > 0)
                _mouseNeeded -= 1;
        }
    }
}