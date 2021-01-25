using UnityEngine;

namespace General
{
    public class GameState : MonoBehaviour
    {
        private bool _paused;

        public bool GetPaused()
        {
            return _paused;
        }

        public void SetPaused(bool paused)
        {
            _paused = paused;
        }
    }
}