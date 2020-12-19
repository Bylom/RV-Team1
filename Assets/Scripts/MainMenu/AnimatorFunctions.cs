using UnityEngine;

namespace Scenes.MainMenu.Scripts
{
	public class AnimatorFunctions : MonoBehaviour
	{
		[SerializeField] MenuButtonController menuButtonController;
		public bool disableOnce;

		private void PlaySound(AudioClip whichSound){
			if (whichSound != null){
				if (!disableOnce)
				{
					menuButtonController.audioSource.PlayOneShot(whichSound);
				}
				else
				{
					disableOnce = false;
				}
			}
		}
	}
}	
