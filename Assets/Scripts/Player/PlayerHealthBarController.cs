using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealthBarController : MonoBehaviour {

    #region Private fields
    
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private Text _healthText;
    [SerializeField] private PlayerController _playerController;

    private float _healthByPercentage = 100f;
    private int _maxHealth;

    #endregion

    #region MonoBehaviour methods

    private void Start() {
        _maxHealth = _playerController.Health;
        _healthText.text = _maxHealth.ToString();
    }

    #endregion

    #region Internal methods

    internal void UpdateHealthBar(int damage) {
        if(_healthByPercentage <= 0f)
            return;
        
        var damageByPercentage = (((float) damage * 100) / _maxHealth);
        _healthByPercentage -= ((float) damageByPercentage);

        if(_healthByPercentage < 0f)
            _healthByPercentage = 0f;
            
        _healthBarImage.DOFillAmount(_healthByPercentage/ 100f, 1.5f)
            .SetLink(gameObject)
            .SetUpdate(true)
            .Play();
    }

    internal void UpdateHealthText(int health) {
        if(health < 0)
            health = 0;
            
        _healthText.text = health.ToString();
    }

    #endregion
}
