using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GeneralUI
{
	public class DialogueManager : MonoBehaviour {

		public Text nameText;
		public Text dialogueText;

		public Animator animator;

		private Queue<string> m_Sentences;
		private static readonly int IsOpen = Animator.StringToHash("IsOpen");

		// Use this for initialization
		void Start () {
			m_Sentences = new Queue<string>();
		}

		public void StartDialogue (Dialogue dialogue)
		{
			animator.SetBool(IsOpen, true);

			nameText.text = dialogue.name;

			m_Sentences.Clear();

			foreach (string sentence in dialogue.sentences)
			{
				m_Sentences.Enqueue(sentence);
			}

			DisplayNextSentence();
		}

		public void DisplayNextSentence ()
		{
			
			if (m_Sentences.Count == 0)
			{
				EndDialogue();
				return;
			}

			string sentence = m_Sentences.Dequeue();
			Debug.Log(sentence);
			StopAllCoroutines();
			StartCoroutine(TypeSentence(sentence));
		}

		IEnumerator TypeSentence (string sentence)
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
			animator.SetBool(IsOpen, false);
		}

	}
}
