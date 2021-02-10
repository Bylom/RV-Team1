using System;
using GeneralUI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Golf
{
    public class Ball : MonoBehaviour
    {
        public Vector3 originPoint;
        [FormerlySerializedAs("_dialogueTrigger")] [SerializeField] private DialogueTrigger dialogueTrigger;
        private bool _firstCollision = true;

        private void OnCollisionEnter(Collision other)
        {
            if(_firstCollision){
                _firstCollision = false;
                if(other.gameObject.CompareTag("Ground")){
                    var dist = Vector3.Distance(originPoint, transform.position);
                    Debug.Log("Dist " + dist);
                }
            }

        }
    }
}
