using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossHealthBarController : MonoBehaviour {
    #region Private fields
    
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private BossCoreController _bossCoreController;

    private float _healthByPercentage = 100f;
    private int _maxHealth;

    #endregion

    #region MonoBehaviour methods

    private void Start() {
        _maxHealth = _bossCoreController.health;
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

    #endregion
}
