using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GeneralUI
{
    public class DialogueManager : MonoBehaviour
    {
        public Text nameText;
        public Text dialogueText;

        public Animator animator;
        [SerializeField] private bool mouseNeeded;
        [SerializeField] private bool pauseNeeded;

        [SerializeField] private GameState gameState;
        
        private Queue<string> _sentences;
        private static readonly int IsOpen = Animator.StringToHash("IsOpen");
        private bool _runningDialogue;

        public bool endScene;

        // Use this for initialization
        void Start()
        {
            _sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            if (_runningDialogue) return;
            if(pauseNeeded)
                gameState.SetPaused(true);
            if(mouseNeeded)
                gameState.SetMouseNeeded(true);
            animator.SetBool(IsOpen, true);
            nameText.text = dialogue.name;
            _runningDialogue = true;

            _sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                _sentences.Enqueue(sentence);
            }

            if (mouseNeeded)
                Cursor.lockState = CursorLockMode.None;
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (_sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = _sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence(string sentence)
        {
            dialogueText.text = "";
            foreach (char letter in sentence)
            {
                dialogueText.text += letter;
                yield return null;
            }
        }

        void EndDialogue()
        {
            _runningDialogue = false;
            animator.SetBool(IsOpen, false);
            if(pauseNeeded)
                gameState.SetPaused(false);
            if (mouseNeeded)
                if(mouseNeeded)
                {
                    gameState.SetMouseNeeded(false);
                    Cursor.lockState = CursorLockMode.Locked;
                }

            if (endScene)
            {
                SceneManager.LoadScene("Scenes/Missioni");
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}