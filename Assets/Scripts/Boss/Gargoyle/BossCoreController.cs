using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoreController : MonoBehaviour {

    #region Private attributes

    #endregion

    #region Internal attributes

    [SerializeField] internal float moveSpeed;

    //others variables
    [SerializeField] internal float delayToAction;
    [SerializeField] internal float takeOffSpeed;
    [SerializeField] internal float ladingSpeed;
    [SerializeField] internal float flyingDiveSpeed;
    [SerializeField] internal int clawDamage;
    [SerializeField] internal int flyingDiveDamage;

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

    #endregion

    #region Private methods

    private void GetComponents() {
        bossAirPatrolController = GetComponent<BossAirPatrolController>();
        bossColliderController = GetComponent<BossColliderController>();
        bossActionController = GetComponent<BossActionController>();
        bossAnimationController = GetComponent<BossAnimationController>();
        bossRigidbody2D = GetComponent<Rigidbody2D>();
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
