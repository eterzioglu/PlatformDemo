using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
}
