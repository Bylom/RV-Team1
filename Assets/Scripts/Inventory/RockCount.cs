using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCount : MonoBehaviour
{

    int rockCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        void OnTriggerStay(Collider rock)
        {
            if (rock.gameObject.CompareTag("Rock"))
            {

                if (Input.GetKey(KeyCode.E))
                {
                    Debug.Log("presaaaaaa");
                    //rock.gameObject.SetActive(false);
                    rockCount++;
                }
            }
        }
    }
}
