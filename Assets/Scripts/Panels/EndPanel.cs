using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanel : Panel
{
    public GameObject winScreen;
    public GameObject loseScreen;

    public void Success()
    {
        winScreen.SetActive(true);
        loseScreen.SetActive(false);
    }

    public void Fail()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        LevelManager.instance.SetupNewLevel();
    }
}
