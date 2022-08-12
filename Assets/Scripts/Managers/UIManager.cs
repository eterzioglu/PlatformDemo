using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region Variables
    public StartPanel startPanel;
    public GamePanel gamePanel;
    public EndPanel endPanel;

    [HideInInspector] public int scoreCount = 0;
    [HideInInspector] public int gridCount = 0;
    #endregion

    #region Singleton
    public static UIManager instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    void Start()
    {
        startPanel.Active(true);
        gamePanel.Active(false);
        endPanel.Active(false);
    }

    public void PlayGame()
    {
        gamePanel.ActiveSmooth(true);
        startPanel.ActiveSmooth(false);
    }
}
