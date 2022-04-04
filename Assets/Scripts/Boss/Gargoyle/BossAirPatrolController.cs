using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAirPatrolController : MonoBehaviour {

    #region Private fields

    [SerializeField] private Transform _borderPatrolRightSide;
    [SerializeField] private Transform _borderPatrolLeftSide;
    
    private bool _reachedTheLimitPatrolSide = false;
    private bool _mustKeepGoingUp = false;
    private bool _mustKeepGoingDown = false;
    private bool _initialAirDive = false;
    private bool _finalAirDive = false;
    private Vector2 _direction = new Vector2(0f, 0f);

    private Transform _borderPatrolTargetSide;
    private BossCoreController _bossCoreController;

    #endregion

    #region MonoBehaviour methods

    private void Awake() {
        GetComponents();
    }

    private void Start() {
        RemoveChildToBorderPatrolSide();
        SetInitialPatrolTargetSide();
    }

    private void Update() {
        if(_bossCoreController.isDead)
            return;

        CheckPatrolTargetSide();
        CheckGroundWhenGoDown();
        CheckMaximumHeightWhenGoUp();
        CheckGroundWhenAirDive();
        CheckMaximumHeightWhenAirDive();
    }

    private void FixedUpdate() {
        if(_bossCoreController.isDead)
            return;
            
        if(_bossCoreController.bossState == BossState.Air && _bossCoreController.mustPatrol)
            MoveThroughTheAir();
        
        if(_mustKeepGoingDown)
            GoDownToTheGround();
        
        if(_mustKeepGoingUp)
            GoUpToSky();
        
        if(_initialAirDive)
            StartAirDive();

        if(_finalAirDive)
            FinishAirDive();
    }

    #endregion

    #region Private methods

    private void SetInitialPatrolTargetSide() {
        _borderPatrolTargetSide = _borderPatrolRightSide;
    }

    private void GetComponents() {
        _bossCoreController = GetComponent<BossCoreController>();
    }

    private void RemoveChildToBorderPatrolSide() {
        _borderPatrolRightSide.parent = null;
        _borderPatrolLeftSide.parent = null;
    }

    private void StartAirDive() {
        if(_borderPatrolTargetSide == _borderPatrolRightSide) {
            _direction = new Vector2(1f, -1f);
        } else {
            _direction = new Vector2(-1f, -1f);
        }

        _bossCoreController.bossRigidbody2D.MovePosition(_bossCoreController.bossRigidbody2D.position + _direction * _bossCoreController.flyingDiveSpeed * Time.fixedDeltaTime);
        _bossCoreController.bossActionController.EnableAirDiveAttackCollider();
    }

    private void FinishAirDive() {
        if(_borderPatrolTargetSide == _borderPatrolRightSide) {
            _direction = new Vector2(1f, 1f).normalized;
        } else {
            _direction = new Vector2(-1f, 1f).normalized;
        }
        _bossCoreController.bossRigidbody2D.MovePosition(_bossCoreController.bossRigidbody2D.position + _direction * _bossCoreController.flyingDiveSpeed * Time.fixedDeltaTime);
    }

    private void MoveThroughTheAir() {
        if(_borderPatrolTargetSide == _borderPatrolRightSide) {
            _direction = new Vector2(1f, 0f).normalized;
        } else {
            _direction = new Vector2(-1f, 0f).normalized;
        }

         _bossCoreController.bossRigidbody2D.MovePosition(_bossCoreController.bossRigidbody2D.position + _direction * _bossCoreController.moveSpeed * Time.fixedDeltaTime);
    }

    private void CheckPatrolTargetSide() {
        if(Vector3.Distance(transform.position , new Vector3(_borderPatrolTargetSide.position.x, transform.position.y, transform.position.z)) <= 0.1f
                && !_reachedTheLimitPatrolSide) {
            
            _reachedTheLimitPatrolSide = true;
            StartCoroutine(TurnAroundCoroutine());
        }
    }

    private void ChangePatrolTargetSide() {
        _borderPatrolTargetSide = (_borderPatrolTargetSide == _borderPatrolRightSide) ? _borderPatrolLeftSide : _borderPatrolRightSide;
    }

    private void CheckGroundWhenGoDown() {
        if(_bossCoreController.bossColliderController.isGrounded && _mustKeepGoingDown) {
            _mustKeepGoingDown = false;
            _bossCoreController.bossAnimationController.PlayIdleOnGroundAnimation();
            _bossCoreController.bossActionController.StartDecideActionCoroutine();
        }
    }

    private void CheckMaximumHeightWhenGoUp() {
        if(Vector3.Distance(transform.position , new Vector3(transform.position.x, _borderPatrolTargetSide.position.y, transform.position.z)) <= 0.1f
                && _mustKeepGoingUp) {
            _bossCoreController.mustPatrol = true;
            _mustKeepGoingUp = false;
        }
    }

    private void CheckGroundWhenAirDive() {
        if(_bossCoreController.bossColliderController.isGrounded && _initialAirDive) {
            _initialAirDive = false;
            _finalAirDive = true;
            _bossCoreController.bossActionController.ShowHeavyDustEffect();
            _bossCoreController.bossActionController.Quake();
        }
    }

    private void CheckMaximumHeightWhenAirDive() {
        if(Vector3.Distance(transform.position , new Vector3(transform.position.x, _borderPatrolTargetSide.position.y, transform.position.z)) <= 0.1f
                && _finalAirDive) {
            _finalAirDive = false;
            _bossCoreController.mustPatrol = true;
            _bossCoreController.bossActionController.DisableAirDiveAttackCollider();
        }
    }

    #endregion

    #region Internal methods

    internal void AirDive() {
        _bossCoreController.mustPatrol = false;
        _initialAirDive = true;
    }

    internal void GoUpToSky() {
        _bossCoreController.mustPatrol = false;
        _mustKeepGoingUp = true;
        _direction = new Vector2(0f, 1f);
        _bossCoreController.bossRigidbody2D.MovePosition(_bossCoreController.bossRigidbody2D.position + _direction * _bossCoreController.takeOffSpeed * Time.fixedDeltaTime);
        _bossCoreController.bossAnimationController.PlayFlyAnimation();
    }

    internal void GoDownToTheGround() {
        _bossCoreController.mustPatrol = false;
        _mustKeepGoingDown = true;
        _direction = new Vector2(0f, -1f);
        _bossCoreController.bossRigidbody2D.MovePosition(_bossCoreController.bossRigidbody2D.position + _direction * _bossCoreController.ladingSpeed * Time.fixedDeltaTime);
    }

    #endregion

    #region Coroutine

    IEnumerator TurnAroundCoroutine() {
        yield return new WaitForSeconds(0.4f);
        ChangePatrolTargetSide();
        _bossCoreController.Flip();
        _reachedTheLimitPatrolSide = false;
        _bossCoreController.bossActionController.StartDecideActionCoroutine();
    }

    #endregion
}
