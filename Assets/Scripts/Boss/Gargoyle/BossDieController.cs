using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDieController : MonoBehaviour
{
    #region Private fields

    [SerializeField] private LayerMask _bossDeadLayer;
    [SerializeField] private CapsuleCollider2D _bossCapsuleCollider2D;

    private BossCoreController _bossCoreController;

    #endregion

    #region MonoBehaviour methods

    private void Awake() {
        GetComponents();
    }

    #endregion

    #region Private methods

    private void GetComponents() {
        _bossCoreController = GetComponent<BossCoreController>();
    }

    private void CallWinGameEvent() {
        EventObserver.WinGameEvent();
    }

    #endregion

    #region Internal methods

    internal void Die() {
        if(_bossCoreController.isDead)
            return;

        _bossCoreController.isDead = true;
        _bossCoreController.bossAnimationController.PlayDieAnimation();
        _bossCoreController.bossRigidbody2D.isKinematic = false;
        _bossCapsuleCollider2D.isTrigger = false;
        _bossCoreController.bossActionController.DisableAirDiveAttackCollider();
        _bossCoreController.bossActionController.DeactiveClawAttackCollider();
        int layerValue = _bossDeadLayer.value;
        gameObject.layer =  LayerMask.NameToLayer("Boss/Dead");
        _bossCoreController.spriteRenderer.sortingOrder = -1;
    }

    #endregion
}
