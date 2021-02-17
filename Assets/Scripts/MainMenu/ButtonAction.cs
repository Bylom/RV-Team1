using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

namespace Scenes.MainMenu.Scripts
{
    public class ButtonAction : StateMachineBehaviour
    {
        ArrayList list= new ArrayList();
        private static readonly int Pressed = Animator.StringToHash("pressed");
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            animator.SetBool(Pressed, false);
            GameObject gameObject;
            int index = (gameObject = animator.gameObject).GetComponent<MenuButton>().thisIndex;
            Debug.Log(gameObject +  " " + index);
            switch (index)
            {
                case 0:
                    Debug.Log(index);
                    SceneManager.LoadScene(0);
                    break;
                case 1:
                    Debug.Log(index);
                    break;
                case 2:
                    Debug.Log(index);
                    Application.Quit();
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
        }
    }
}
