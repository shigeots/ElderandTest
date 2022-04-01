using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAirPatrolController : MonoBehaviour {

    #region Private attributes

    [SerializeField] private Transform _borderPatrolRightSide;
    [SerializeField] private Transform _borderPatrolLeftSide;

    private Transform _borderPatrolTargetSide;
    private BossCoreController _bossCoreController;

    #endregion

    #region MonoBehaviour methods

    private void Start() {
        _borderPatrolTargetSide = _borderPatrolRightSide;
        GetComponents();
        RemoveChildToBorderPatrolSide();
    }

    private void Update() {
        CheckPatrolTargetSide();
    }

    private void FixedUpdate() {
        MoveThroughTheAir();
    }

    #endregion

    #region Private methods

    private void GetComponents() {
        _bossCoreController = GetComponent<BossCoreController>();
    }

    private void RemoveChildToBorderPatrolSide() {
        _borderPatrolRightSide.parent = null;
        _borderPatrolLeftSide.parent = null;
    }

    private void MoveThroughTheAir() {
            //Vector3 _direction = (_borderPatrolTargetSide.transform.position - transform.position).normalized;
            Vector3 _distance = (_borderPatrolTargetSide.transform.position - transform.position).normalized;
            Vector3 _direction = new Vector3(_distance.x, 0f, 0f);
            
            _bossCoreController.bossRigidbody2D.MovePosition(transform.position + _direction * _bossCoreController.moveSpeed * Time.fixedDeltaTime);
    }

    private void CheckPatrolTargetSide() {
        if(Vector3.Distance(transform.position , new Vector3(_borderPatrolTargetSide.position.x, transform.position.y, transform.position.z)) <= 0.2f) {
            ChangePatrolTargetSide();
            _bossCoreController.Flip();
        }
    }

    private void ChangePatrolTargetSide() {
        _borderPatrolTargetSide = (_borderPatrolTargetSide == _borderPatrolRightSide) ? _borderPatrolLeftSide : _borderPatrolRightSide;
        
    }

    #endregion
}
