using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCount : MonoBehaviour
{
    private Animator opening;
    public int rockCount = 0;
    public int ballCount = 0;

    private int numSassi = 6;
    public GameObject[] inseriti;

    private static readonly int Open = Animator.StringToHash("Open");



    // Start is called before the first frame update
    void Start()
    {
        inseriti = new GameObject[numSassi];
        for(int i=0; i<numSassi; i++)
        {
            inseriti[i] = GameObject.Find("sasso" + i);
            inseriti[i].SetActive(false);
        }

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
                rock.gameObject.SetActive(false);
                inseriti[rockCount].SetActive(true);
                rockCount++;
            }
        }

        if (rock.gameObject.CompareTag("Palla"))
        {
            opening.SetBool("Open", true);

            if (Input.GetKey(KeyCode.E))
            {
                rock.gameObject.SetActive(false);
                inseriti[5].SetActive(true);
                ballCount++;
            }
        }
    }
}
