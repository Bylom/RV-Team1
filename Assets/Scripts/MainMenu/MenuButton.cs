using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scenes.MainMenu.Scripts
{
    public class MenuButton : MonoBehaviour
        , IPointerClickHandler
        , IPointerEnterHandler
        , IPointerExitHandler
    {

        [SerializeField] private MenuButtonController menuButtonController;
        [SerializeField] Animator animator;
        [SerializeField] AnimatorFunctions animatorFunctions;
        [SerializeField] public int thisIndex;
        private static readonly int Pressed = Animator.StringToHash("pressed");
        private static readonly int Selected = Animator.StringToHash("selected");


        // Update is called once per frame
        void Update()
        {
            if (menuButtonController.index == thisIndex)
            {
                animator.SetBool("selected", true);
            }
            else
            {
                animator.SetBool("selected", false);
            }   
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            menuButtonController.index = thisIndex;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //animator.SetBool(Selected, false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            animator.SetBool(Pressed, true);
            animator.SetBool(Selected, false);
        }
    }
}


// // Update is called once per frame
// void Update()
// {
// // if(menuButtonController.index == thisIndex)
// // {
// // 	animator.SetBool ("selected", true);
// // 	if(Math.Abs(Input.GetAxis ("Submit") - 1) < TOLLERANCE){
// // 		animator.SetBool (Pressed, true);
// // 	}else if (animator.GetBool (Pressed)){
// // 		animator.SetBool ("pressed", false);
// // 		animatorFunctions.disableOnce = true;
// // 	}
// // }else{
// // 	animator.SetBool ("selected", false);
// // }
// }
//
// public void OnPointerEnter(PointerEventData eventData)
// {
// animator.SetBool(Selected, true);
// }
//
// public void OnPointerExit(PointerEventData eventData)
// {
// animator.SetBool(Selected, false);
// }
//
// public void OnPointerClick(PointerEventData eventData)
// {
// animator.SetBool(Pressed, true);
// }

// if(menuButtonController.index == thisIndex)
// {
// 	animator.SetBool ("selected", true);
// 	if(Math.Abs(Input.GetAxis ("Submit") - 1) < TOLLERANCE){
// 		animator.SetBool (Pressed, true);
// 	}else if (animator.GetBool (Pressed)){
// 		animator.SetBool ("pressed", false);
// 		animatorFunctions.disableOnce = true;
// 	}
// }else{
// 	animator.SetBool ("selected", false);
// }