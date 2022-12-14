using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    #region Variables
    public CinemachineVirtualCamera vCam;
    public CinemachineVirtualCamera orbitalCam;
    CinemachineTransposer transposer;
    #endregion

    #region Singleton
    public static CameraManager instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    private void Start()
    {
        SetupCameraSettings();
    }

    private void Update()
    {
        RotateCameraAroundPlayer();
    }

    public void SetFollowOffset(Vector3 offset)
    {
        transposer.m_FollowOffset = offset;
    }

    public void SetCinemachineRotation(Vector3 rotation)
    {
        vCam.transform.DORotate(rotation, 0.25f);
    }

    public void RotateCameraAroundPlayer()
    {
        if (!orbitalCam.enabled) return;

        CinemachineOrbitalTransposer orbitalTranspozer = orbitalCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        orbitalTranspozer.m_XAxis.Value += Time.deltaTime * orbitalTranspozer.m_XAxis.m_MaxSpeed;
    }

    public void FinishLevel()
    {
        vCam.gameObject.SetActive(false);
        orbitalCam.gameObject.SetActive(true);
    }

    public void FailLevel()
    {
        vCam.gameObject.SetActive(false);
    }

    public void SetupCameraSettings()
    {
        orbitalCam.gameObject.SetActive(false);
        vCam.gameObject.SetActive(true);

        transposer = vCam.GetCinemachineComponent<CinemachineTransposer>();

        SetCinemachineRotation(new Vector3(27.5f, 0, 0));
        SetFollowOffset(new Vector3(0, 5, -5));
    }
}
