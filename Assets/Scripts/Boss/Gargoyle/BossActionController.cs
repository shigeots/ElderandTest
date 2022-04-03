using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActionController : MonoBehaviour {

    #region Private fields

    [SerializeField] private GameObject _clawAttackCollider;
    [SerializeField] private CapsuleCollider2D _airDiveAttackCollider;
    [SerializeField] private GameObject _horizontaFireballPrefab;
    [SerializeField] private GameObject _diagonalFireballPrefab;
    [SerializeField] private GameObject _fireballCastEffectPrefab;
    [SerializeField] private GameObject _backDustEffectPrefab;
    [SerializeField] private Transform _horizontaFireballSpawnPoint;
    [SerializeField] private Transform _diagonalFireballSpawnPoint;
    [SerializeField] private Transform _backDustEffectSpawnPoint;

    [SerializeField] private BossAction lastAction = BossAction.None;

    private BossCoreController _bossCoreController;

    #endregion

    #region MonoBehaviour methods

    private void Awake() {
        GetComponents();
    }

    #endregion

    #region Internal methods

    internal void StartDecideActionCoroutine() {
        StartCoroutine(DecideActionCoroutine());
    }

    #endregion

    #region Private methods

    private void GetComponents() {
        _bossCoreController = GetComponent<BossCoreController>();
    }

    private void DecideAction() {
        if(_bossCoreController.bossState == BossState.Grounded) {
            GroundAction();
            return;
        }
        if(_bossCoreController.bossState == BossState.Air) {
            AirAction();
            return;
        }
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
    }

    private void VerifyAirLastAction(BossAction actionToCheck) {
        if(lastAction == actionToCheck) {
            Debug.Log("Repeat");
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
        _bossCoreController.bossAnimationController.PlayFireballFromGroundAnimation();
    }

    private void DoFly() {
        Debug.Log("Fly");
        lastAction = BossAction.Fly;
        _bossCoreController.bossState = BossState.Air;
        _bossCoreController.bossAirPatrolController.GoUpToSky();
        ShowBackDustEffect();
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
        _bossCoreController.bossAnimationController.PlayFireballFromAirAnimation();
    }

    private void DoAirDive() {
        Debug.Log("Air dive");
        lastAction = BossAction.AirDiving;
        _bossCoreController.bossAirPatrolController.AirDive();
    }

    private void DoLand() {
        Debug.Log("Land");
        lastAction = BossAction.Land;
        _bossCoreController.bossState = BossState.Grounded;
        _bossCoreController.bossAirPatrolController.GoDownToTheGround();
    }

    private void ShootHorizontalFireball() {
        GameObject fireball = Instantiate(_horizontaFireballPrefab, _horizontaFireballSpawnPoint.position, _horizontaFireballSpawnPoint.rotation);
        GameObject fireballCast = Instantiate(_fireballCastEffectPrefab, _horizontaFireballSpawnPoint.position, _horizontaFireballSpawnPoint.rotation);
        fireball.GetComponent<FireballController>().Damage = _bossCoreController.fireballDamage;
        Rigidbody2D fireballRigidbody2D = fireball.GetComponent<Rigidbody2D>();

        if(transform.localScale.x > 0) {
            fireballRigidbody2D.AddForce(_horizontaFireballSpawnPoint.right * _bossCoreController.fireballSpeed, ForceMode2D.Impulse);
            return;
        }
        if(transform.localScale.x < 0) {
            fireball.transform.localScale = new Vector2(fireball.transform.localScale.x * -1, fireball.transform.localScale.y);
            fireballRigidbody2D.AddForce(-_horizontaFireballSpawnPoint.right * _bossCoreController.fireballSpeed, ForceMode2D.Impulse);
            return;
        }
    }

    private void ShootDiagonalFireball() {
        GameObject fireball = Instantiate(_diagonalFireballPrefab, _diagonalFireballSpawnPoint.position, _diagonalFireballSpawnPoint.rotation);
        GameObject fireballCast = Instantiate(_fireballCastEffectPrefab, _diagonalFireballSpawnPoint.position, _diagonalFireballSpawnPoint.rotation);

        fireball.GetComponent<FireballController>().Damage = _bossCoreController.fireballDamage;
        Rigidbody2D fireballRigidbody2D = fireball.GetComponent<Rigidbody2D>();

        if(transform.localScale.x > 0) {
            fireballRigidbody2D.AddForce(_diagonalFireballSpawnPoint.right * _bossCoreController.fireballSpeed, ForceMode2D.Impulse);
            return;
        }
        if(transform.localScale.x < 0) {
            fireball.transform.localScale = new Vector2(fireball.transform.localScale.x * -1, fireball.transform.localScale.y);
            fireballRigidbody2D.AddForce(-_diagonalFireballSpawnPoint.right * _bossCoreController.fireballSpeed, ForceMode2D.Impulse);
            return;
        }
    }

    private void ShowBackDustEffect() {
        GameObject backDustEffect = Instantiate(_backDustEffectPrefab, _backDustEffectSpawnPoint.position, _backDustEffectSpawnPoint.rotation);

        if(transform.localScale.x < 0) {
            backDustEffect.transform.localScale = new Vector2(backDustEffect.transform.localScale.x * -1, backDustEffect.transform.localScale.y);
            return;
        }
    }

    private void ActiveClawAttackCollider() {
        _clawAttackCollider.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void DeactiveClawAttackCollider() {
        _clawAttackCollider.GetComponent<CircleCollider2D>().enabled = false;
    }

    #endregion

    #region Internal methods

    internal void EnableAirDiveAttackCollider() {
        _airDiveAttackCollider.enabled = true;
    }

    internal void DisableAirDiveAttackCollider() {
        _airDiveAttackCollider.enabled = false;
    }

    #endregion

    #region Coroutine

    IEnumerator DecideActionCoroutine() {
        yield return new WaitForSeconds(_bossCoreController.delayToAction);

        if(_bossCoreController.isDead)
            yield break;
            
        DecideAction();
    }

    #endregion
}
