using UnityEngine;
using UnityEngine.UI;

public class LevelSelector: MonoBehaviour
{
    public Button[] lvlButtons;
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);
        for(int i=0; i<lvlButtons.Length; i++)
        {
            if (i + 3 > levelAt)
                lvlButtons[i].interactable = false;
        }
    }
}
