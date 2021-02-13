using General;
using GeneralUI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Golf
{
    public class BallCount : MonoBehaviour
    {
        [SerializeField] private GameState gameState;

        [SerializeField] private DialogueTrigger dialogueTrigger;

        private bool _first = true;
        [SerializeField] private int limit = 3;

        private int _count;

        // Update is called once per frame
        void Update()
        {
            if (!gameState.GetPaused() && _first && _count == limit)
            {
                DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
                dialogueManager.endScene = true;
                _first = false;
                dialogueTrigger.dialogue = new Dialogue()
                {
                    name = "Mission control", sentences = new[]
                    {
                        "That was your last ball.", 
                        "Even with that bulky suit it's easy to make a shot with this gravity, isn't it?"
                    }
                };
                dialogueTrigger.TriggerDialogue();
            }
        }

        public void IncreaseCount()
        {
            _count++;
        }
    }
}