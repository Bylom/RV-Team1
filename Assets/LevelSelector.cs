using UnityEngine;
using UnityEngine.UI;
using General;

public class LevelSelector: MonoBehaviour
{
    public Button[] lvlButtons;
    private GameValues _gameValues;
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);
        for (int i = levelAt; i < lvlButtons.Length; i++)
        {
            lvlButtons[i].interactable = false;
        }
    }
}
