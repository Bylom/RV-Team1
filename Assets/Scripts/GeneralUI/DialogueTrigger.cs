using UnityEngine;

namespace GeneralUI
{
	public class DialogueTrigger : MonoBehaviour {

		public Dialogue dialogue;

		public void TriggerDialogue ()
		{
			FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
		}

		public void SetDialog(Dialogue dial)
		{
			this.dialogue = dial;
		}


	}
}
