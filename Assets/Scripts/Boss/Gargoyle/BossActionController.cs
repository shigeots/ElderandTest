using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionController : MonoBehaviour {
    #region Private attributes

    [SerializeField] private BossAction lastAction = BossAction.None;

    private BossCoreController _bossCoreController;

    #endregion

    #region MonoBehaviour methods

    private void Start() {
        GetComponents();
    }

    #endregion

    #region Internal methods

    internal void DecideAction() {
        if(_bossCoreController.bossState == BossState.Grounded) {
            GroundAction();
            return;
        }
        if(_bossCoreController.bossState == BossState.Air) {
            AirAction();
            return;
        }
    }

    #endregion

    #region Private methods

    private void GetComponents() {
        _bossCoreController = GetComponent<BossCoreController>();
    }

    private void GroundAction() {
        if(_bossCoreController.playerIsClose) {
            VerifyGroundLastAction(BossAction.Claw);
            return;
        }

        int probability = UnityEngine.Random.Range(1,101);

        if(!_bossCoreController.playerIsClose && probability > 20) {
            VerifyGroundLastAction(BossAction.FireballFromGround);
            Debug.Log(probability.ToString());

            return;
        }

        if(!_bossCoreController.playerIsClose && probability <= 20) {
            VerifyGroundLastAction(BossAction.Fly);
            Debug.Log(probability.ToString());
            return;
        }
    }

    private void AirAction() {
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
            DoClaw();
            return;
        }
        if(actionToCheck == BossAction.FireballFromGround && lastAction != BossAction.FireballFromGround) {
            DoFireballFromGround();
            return;
        }

        if(actionToCheck == BossAction.Claw && lastAction == BossAction.Claw) {
            DoFireballOrFly();
            return;
        }

        if(actionToCheck == BossAction.FireballFromGround && lastAction == BossAction.FireballFromGround) {
            DoClawOrFly();
            return;
        }

        if(actionToCheck == BossAction.Fly) {
            DoFly();
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
            DoFireballFromAir();
            return;
        }

        if(actionToCheck == BossAction.AirDiving) {
            DoAirDive();
            return;
        }

        if(actionToCheck == BossAction.Land) {
            DoLand();
            return;
        }
    }

    private void DoClaw() {
        Debug.Log("Claw");
        lastAction = BossAction.Claw;
        _bossCoreController.bossAnimationController.PlayClawAttackAnimation();
    }

    private void DoFireballFromGround() {
        Debug.Log("Fireball from ground");
        lastAction = BossAction.FireballFromGround;
    }

    private void DoFly() {
        Debug.Log("Fly");
        lastAction = BossAction.Fly;
    }

    private void DoFireballOrFly() {
        int probability = UnityEngine.Random.Range(1,101);

        if(probability <= 50) {
            DoFireballFromGround();
            return;
        }

        if(probability > 50) {
            DoFly();
            return;
        }
    }

    private void DoClawOrFly() {
        int probability = UnityEngine.Random.Range(1,101);

        if(probability <= 50) {
            DoClaw();
            return;
        }

        if(probability > 50) {
            DoFly();
            return;
        }
    }

    private void DoFireballFromAir() {
        Debug.Log("Fireball from air");
        lastAction = BossAction.FireballFromAir;
    }

    private void DoAirDive() {
        Debug.Log("Air dive");
        lastAction = BossAction.AirDiving;
    }

    private void DoLand() {
        Debug.Log("Land");
        lastAction = BossAction.Land;
    }

    #endregion
}