using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject platformParent;

    #region Singleton
    public static LevelManager instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    public void SetupNewLevel()
    {
        CameraManager.instance.SetupCameraSettings();
        UIManager.instance.SetupGame();
        PlayerController.instance.TriggerAnimation("idle"); 
        PlayerController.instance.ChangeXPos(0);

        GameObject refObject = platformParent.transform.GetChild(0).gameObject;

        float posZ = refObject.transform.position.z + refObject.GetComponent<Platform>().CalculatePlatformSize();
        Vector3 pos = new Vector3(refObject.transform.position.x, refObject.transform.position.y, posZ);

        GameObject platform = Instantiate(Resources.Load<GameObject>("Platform"), pos, Quaternion.identity, platformParent.transform);

        refObject.GetComponent<Platform>().enabled = false;
    }
}
