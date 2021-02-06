using General;
using UnityEngine;

namespace Golf
{
    public class Astronaut : MonoBehaviour
    {
        [SerializeField] private CameraController cameraController;
        [SerializeField] private Animator anim;
        [SerializeField] private GameState state;

        private static readonly int Hit1 = Animator.StringToHash("Hit");

        // Start is called before the first frame update
        public void Hit()
        {
            cameraController.HitBall();
        }

        public void Pause()
        {
            state.SetPaused(true);
        }

        public void Restart()
        {
            anim.SetBool(Hit1, false);
            state.SetPaused(false);
        }
    }
}