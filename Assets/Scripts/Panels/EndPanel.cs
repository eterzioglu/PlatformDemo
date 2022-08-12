using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
