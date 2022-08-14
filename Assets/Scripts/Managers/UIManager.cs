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

    [HideInInspector] public bool gameStart = false;
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

        CameraManager.instance.SetFollowOffset(new Vector3(3, 7, -5));
        CameraManager.instance.SetCinemachineRotation(new Vector3(35, -20, 0));

        PlayerController.instance.TriggerAnimation("run");
        gameStart = true;
    }

    public void EndGame(bool win)
    {
        gamePanel.ActiveSmooth(false);
        endPanel.ActiveSmooth(true);

        gameStart = false;

        if (win)
        {
            PlayerController.instance.TriggerAnimation("dance");
            endPanel.Success();
        }
        else
        {
            PlayerController.instance.TriggerAnimation("fall");
            endPanel.Fail();
        }
    }
}
