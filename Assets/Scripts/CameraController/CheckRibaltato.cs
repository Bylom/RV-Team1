using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckRibaltato : MonoBehaviour
{
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Terrain>() != null)
        {
            FindObjectOfType<AudioManager>().Play("GameOver");
            image.CrossFadeAlpha(1, 2, false);
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(5);
        
    }

}
