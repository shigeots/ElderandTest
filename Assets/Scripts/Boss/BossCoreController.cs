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
    [SerializeField] internal BossAction lastAction = BossAction.None;
    [SerializeField] internal bool playerIsClose = false;

    internal BossAirPatrolController bossAirPatrolController;
    internal BossColliderController bossColliderController;
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
        bossRigidbody2D = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region Internal methods

    internal void Flip() {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    [ContextMenu("Decidir accion")]
    internal void DecideAction() {
        if(bossState == BossState.Grounded) {
            GroundAction();
            return;
        }
        if(bossState == BossState.Air) {
            AirAction();
            return;
        }
    }

    internal void GroundAction() {
        if(playerIsClose) {
            VerifyGroundLastAction(BossAction.Claw);
            return;
        }

        int probability = UnityEngine.Random.Range(1,101);

        if(!playerIsClose && probability > 20) {
            VerifyGroundLastAction(BossAction.FireballFromGround);
            Debug.Log(probability.ToString());

            return;
        }

        if(!playerIsClose && probability <= 20) {
            VerifyGroundLastAction(BossAction.Fly);
            Debug.Log(probability.ToString());
            return;
        }

    }

    internal void AirAction() {
        int probability = UnityEngine.Random.Range(1,101);

        if(probability <= 40) {
            Debug.Log(probability.ToString());
            VerifyAirLastAction(BossAction.FireballFromAir);
            return;
        }

        if(probability > 40 && probability <= 80) {
            Debug.Log(probability.ToString());
            VerifyAirLastAction(BossAction.AirDiving);
            return;
        }

        if(probability > 80) {
            Debug.Log(probability.ToString());
            VerifyAirLastAction(BossAction.Land);
            return;
        }
    }

    private void VerifyGroundLastAction(BossAction actionToCheck) {
        if(actionToCheck == BossAction.Claw && lastAction != BossAction.Claw) {
            Debug.Log("Claw");
            lastAction = BossAction.Claw;
            return;
        }
        if(actionToCheck == BossAction.FireballFromGround && lastAction != BossAction.FireballFromGround) {
            Debug.Log("Fireball");
            lastAction = BossAction.FireballFromGround;
            return;
        }

        if(actionToCheck == BossAction.Claw && lastAction == BossAction.Claw) {
            Debug.Log("Fireball or fly");
            lastAction = BossAction.FireballFromGround;
            return;
        }

        if(actionToCheck == BossAction.FireballFromGround && lastAction == BossAction.FireballFromGround) {
            Debug.Log("Claw or fly");
            lastAction = BossAction.Claw;
            return;
        }

        if(actionToCheck == BossAction.Fly) {
            Debug.Log("Fly");
            lastAction = BossAction.Fly;
            return;
        }
        Debug.Log("bug");
    }

    private void VerifyAirLastAction(BossAction actionToCheck) {
        if(lastAction == actionToCheck) {
            Debug.Log("repetir");
            AirAction();
            return;
        }

        if(actionToCheck == BossAction.FireballFromAir) {
            Debug.Log("Fireball");
            lastAction = BossAction.FireballFromAir;
            return;
        }

        if(actionToCheck == BossAction.AirDiving) {
            Debug.Log("Diving");
            lastAction = BossAction.AirDiving;
            return;
        }

        if(actionToCheck == BossAction.Land) {
            Debug.Log("Aterrizar");
            lastAction = BossAction.Land;
            return;
        }
    }

    #endregion
}
