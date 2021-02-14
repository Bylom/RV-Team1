using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCount : MonoBehaviour
{
    private Animator opening;

    public int rockCount = 0;
    public int ballCount = 0;

    private static readonly int Open = Animator.StringToHash("Open");



    // Start is called before the first frame update
    void Start()
    {
        opening = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider rock)
    {
        if (rock.gameObject.CompareTag("Rock"))
        {
            opening.SetBool("Open", true);

            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("presaaaaaa");
                rock.gameObject.SetActive(false);
                rockCount++;
            }
        }

        if (rock.gameObject.CompareTag("Palla"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("pallaaaaa");
                rock.gameObject.SetActive(false);
                ballCount++;
            }
        }
    }
}
