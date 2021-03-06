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
        public Text nameText, historicNameText;
        public Text dialogueText, historicDialogueText;

        public Animator animator, historicAnimator;
        [SerializeField] private bool mouseNeeded;
        [SerializeField] private bool pauseNeeded;

        [SerializeField] private GameState gameState;

        [SerializeField] private Dialogue historicDialogue;
        private Queue<string> _sentences;
        private static readonly int IsOpen = Animator.StringToHash("IsOpen");
        private bool _runningDialogue, _close;

        public bool endScene;

        // Use this for initialization
        void Start()
        {
            _sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            if (_runningDialogue) return;
            if (pauseNeeded)
                gameState.SetPaused(true);
            if (mouseNeeded)
                gameState.SetMouseNeeded(true);
            animator.SetBool(IsOpen, true);
            nameText.text = dialogue.name;
            _runningDialogue = true;

            if (_sentences is null)
                _sentences = new Queue<string>();
            _sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                _sentences.Enqueue(sentence);
            }

            if (mouseNeeded)
                Cursor.lockState = CursorLockMode.None;
            DisplayNextSentence();
        }
        
        public void StartHistoricDialogue(Dialogue dialogue)
        {
            _close = true;
            if (pauseNeeded)
                gameState.SetPaused(true);
            if (mouseNeeded)
                gameState.SetMouseNeeded(true);
            historicAnimator.SetBool(IsOpen, true);
            historicNameText.text = dialogue.name;
            _runningDialogue = true;

            _sentences.Clear();

            foreach (string sentence in historicDialogue.sentences)
            {
                _sentences.Enqueue(sentence);
            }

            if (mouseNeeded)
                Cursor.lockState = CursorLockMode.None;
            DisplayNextHistoricSentence();
        }

        private void LateUpdate()
        {
            if (!_close && _runningDialogue && Input.GetKeyDown(KeyCode.Space))
                DisplayNextSentence();
            else if (_runningDialogue && Input.GetKeyDown(KeyCode.Space))
            {
                
                DisplayNextHistoricSentence();
            }
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
        
        public void DisplayNextHistoricSentence()
        {
            if (_sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = _sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeHistoricSentence(sentence));
        }

        
        IEnumerator TypeHistoricSentence(string sentence)
        {
            historicDialogueText.text = "";
            foreach (char letter in sentence)
            {
                historicDialogueText.text += letter;
                yield return null;
            }
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

        public void EndDialogue()
        {
            _runningDialogue = false;
            animator.SetBool(IsOpen, false);
            historicAnimator.SetBool(IsOpen, false);
            if (pauseNeeded)
                gameState.SetPaused(false);
            if (mouseNeeded)
                if (mouseNeeded)
                {
                    gameState.SetMouseNeeded(false);
                    Cursor.lockState = CursorLockMode.Locked;
                }
            
            if (_close)
            {
                PlayerPrefs.SetInt("levelAt", SceneManager.GetActiveScene().buildIndex);
                
                SceneManager.LoadScene("Scenes/Missioni");
                Cursor.lockState = CursorLockMode.None;
                return;
            }

            if (endScene)
            {
                StartHistoricDialogue(historicDialogue);
            }
        }
    }
}