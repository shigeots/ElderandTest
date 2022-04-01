using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoreController : MonoBehaviour {

    #region Private attributes

    #endregion

    #region Internal attributes

    [SerializeField] internal float moveSpeed;
    [SerializeField] internal float delayToAction;
    [SerializeField] internal float takeOffSpeed;
    [SerializeField] internal float ladingSpeed;
    [SerializeField] internal float flyingDiveSpeed;
    [SerializeField] internal float fireballSpeed;
    [SerializeField] internal int fireballDamage;
    [SerializeField] internal int clawDamage;
    [SerializeField] internal int flyingDiveDamage;

    [SerializeField] private Transform _frontCheck;
    [SerializeField] private Vector2 _boxCastSize;
    [SerializeField] private LayerMask _playerLayer;

    [SerializeField] internal BossState bossState = BossState.Air;
    [SerializeField] internal bool playerIsClose = false;

    internal BossAirPatrolController bossAirPatrolController;
    internal BossColliderController bossColliderController;
    internal BossActionController bossActionController;
    internal BossAnimationController bossAnimationController;
    internal Rigidbody2D bossRigidbody2D;

    #endregion

    #region MonoBehaviour methods

    private void Start() {
        GetComponents();
    }

    private void Update() {
        CheckPlayerIsFront();
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(255, 0, 0, 0.75F);
        Gizmos.DrawWireCube(_frontCheck.position, _boxCastSize);
    }

    #endregion

    #region Private methods

    private void GetComponents() {
        bossAirPatrolController = GetComponent<BossAirPatrolController>();
        bossColliderController = GetComponent<BossColliderController>();
        bossActionController = GetComponent<BossActionController>();
        bossAnimationController = GetComponent<BossAnimationController>();
        bossRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void CheckPlayerIsFront() {
        var boxCastHit2D = Physics2D.BoxCast(_frontCheck.position, _boxCastSize, 0f, Vector2.zero, .1f, _playerLayer);

        if(boxCastHit2D) {
            playerIsClose = true;
        } else {
            playerIsClose = false;
        }
    }

    #endregion

    #region Internal methods

    internal void Flip() {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    [ContextMenu("ACTION")]
    internal void CallDecideAction() {
        bossActionController.DecideAction();
    }

    #endregion
}
