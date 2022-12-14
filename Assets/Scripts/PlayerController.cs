using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    #region Variables
    Animator anim;
    [SerializeField] private float speed = 0;
    #endregion

    #region Singleton
    public static PlayerController instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    private void Start()
    {
        anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        MovePlayer();
    }

    public void ChangeXPos(float targetX)
    {
        gameObject.transform.DOMoveX(targetX, 0.25f);
    }

    public void TriggerAnimation(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    private void MovePlayer()
    {
        if(UIManager.instance.gameStart)
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "finish")
        {
            CameraManager.instance.FinishLevel();
            UIManager.instance.EndGame(true);
            other.gameObject.SetActive(false);
        }
        else if(other.tag == "fall")
        {
            UIManager.instance.EndGame(false);
            CameraManager.instance.FailLevel();
        }
    }
}
