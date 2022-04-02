using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoreController : MonoBehaviour {

    #region Internal attributes

    [Header("Movement speed and actions")]
    [SerializeField, Range(1, 5), Tooltip("Boss movement speed. Minimum value 1 and maximum 5.")]
    internal float moveSpeed;
    [SerializeField, Range(0, 5), Tooltip("Delay time for each action. Minimum value 0 and maximum 5.")]
    internal float delayToAction;
    [SerializeField, Range(1, 5), Tooltip("Boss take off speed. Minimum value 1 and maximum 5.")]
    internal float takeOffSpeed;
    [SerializeField, Range(1, 5), Tooltip("Boss landing speed. Minimum value 1 and maximum 5.")]
    internal float ladingSpeed;
    [SerializeField, Range(1, 5), Tooltip("Air dive attack speed. Minimum value 1 and maximum 5.")]
    internal float flyingDiveSpeed;
    [SerializeField, Range(1, 20), Tooltip("Fireball projectile speed. Minimum value 1 and maximum 20.")]
    internal float fireballSpeed;

    [Header("Damage he can deal with his attacks")]
    [SerializeField, Min(1), Tooltip("The damage of the fireball attack that can be dealt to the player. The value cannot be less than 1")]
    internal int fireballDamage;
    [SerializeField, Min(1), Tooltip("The damage of the claw attack that can be dealt to the player. The value cannot be less than 1")]
    internal int clawDamage;
    [SerializeField, Min(1), Tooltip("The damage of the air dive attack that can be dealt to the player. The value cannot be less than 1")]
    internal int flyingDiveDamage;

    internal BossState bossState = BossState.Air;
    internal bool playerIsClose = false;
    internal bool mustPatrol = true;

    internal BossAirPatrolController bossAirPatrolController;
    internal BossColliderController bossColliderController;
    internal BossActionController bossActionController;
    internal BossAnimationController bossAnimationController;
    internal BossFrontCheckController bossFrontCheckController;
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

    #endregion
}
