using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamaraController : MonoBehaviour {

    #region Private fields

    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;

    private CinemachineVirtualCamera _cinemachineVirtualCamera; 
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    #endregion

    #region MonoBehaviour methods

    private void Awake() {
        GetComponents();
    }

    #endregion

    #region Private methods

    private void GetComponents() {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        _cinemachineBasicMultiChannelPerlin = 
                        _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void SelectPlayerToFollow(GameObject player) {
        _cinemachineVirtualCamera.Follow = player.transform;
    }

    private void ShakeCamera() {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _amplitude;
        _cinemachineBasicMultiChannelPerlin.m_FrequencyGain = _frequency;
    }

    private void StopShakeCamera() {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        _cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f;
    }

    #endregion

    #region Internal methods

    internal void StartShakeCameraCoroutine() {
        StartCoroutine(ShakeCameraCoroutine());
    }

    #endregion

    #region Coroutine

    IEnumerator ShakeCameraCoroutine() {

        ShakeCamera();
        yield return new WaitForSeconds(0.5f);
        StopShakeCamera();

    }

    #endregion
}
