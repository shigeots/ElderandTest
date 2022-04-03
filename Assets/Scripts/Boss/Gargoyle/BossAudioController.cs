using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudioController : MonoBehaviour
{
    #region Private fields

    [SerializeField] AudioClip _damageAudioClip;
    
    private BossCoreController _bossCoreController;
    private AudioSource _audioSource;

    #endregion

    #region Monobehaviour methods

    private void Awake() {
        GetComponents();
    }

    #endregion

    #region Private methods

    private void GetComponents() {
        _bossCoreController = GetComponent<BossCoreController>();
        _audioSource = GetComponent<AudioSource>();
    }

    #endregion

    #region Internal methods

    internal void PlayDamageSound() {
        _audioSource.Stop();
        _audioSource.clip = _damageAudioClip;
        _audioSource.Play();
    }

    #endregion


}
