using GeneralUI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Golf
{
    public class Ball : MonoBehaviour
    {
        private Vector3 _originPoint;
        [FormerlySerializedAs("_dialogueTrigger")] [SerializeField] private DialogueTrigger dialogueTrigger;
        private bool _firstCollision = true;

        private void Start()
        {
            var position = transform.position;
            _originPoint = new Vector3(position.x, position.y, position.z);
        }

        private void OnCollisionEnter(Collision other)
        {
            if(_firstCollision && Vector3.Distance(_originPoint, transform.position) > 1.5f){
                _firstCollision = false;
                if (!other.gameObject.CompareTag("Ground")) return;
                //
                var position = transform.position;
                var dist = Vector3.Distance(_originPoint, position);
                // Debug.Log("Origin point: " + _originPoint + "\nBall: " + position);
                var dialogue = new Dialogue
                {
                    name = "Mission control",
                    sentences = new[]
                    {
                        "That was a " + dist.ToString("0.0") +"m shot!"
                    }
                };
                dialogueTrigger.SetDialog(dialogue);
                dialogueTrigger.TriggerDialogue();
            }

        }


        private void Update()
        {
            var position = transform.position;
            if (position.y > 0) return;
            if(_firstCollision && Vector3.Distance(_originPoint, transform.position) > 1.5f){
                _firstCollision = false;
                //
                var dist = Vector3.Distance(_originPoint, position);
                // Debug.Log("Origin point: " + _originPoint + "\nBall: " + position);
                var dialogue = new Dialogue
                {
                    name = "Mission control",
                    sentences = new[]
                    {
                        "That was a " + dist.ToString("0.0") +"m shot!"
                    }
                };
                dialogueTrigger.SetDialog(dialogue);
                dialogueTrigger.TriggerDialogue();
            }
        }
    }
}
