using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Private fields

    [SerializeField] private int _health = 100;
    [SerializeField] private float _speedOfMovement = 5f;
    [SerializeField] private GameObject _attackCollider;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float _xAxis;
    private bool _lookRight = true;
    private bool _canNextAttack = true;

    #endregion

    #region MonoBehaviour methods

    private void Awake() {
        GetComponents();
    }

    private void Update() {
        _xAxis = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.J) && _canNextAttack)
            Attack();
    }

    private void FixedUpdate() {
        Move();
    }

    #endregion

    #region Private methods

    private void GetComponents() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Move() {
        if(_xAxis != 0) {
            _rigidbody2D.velocity = new Vector2(_xAxis * _speedOfMovement, _rigidbody2D.velocity.y);
            Flip(_xAxis);
        }
        if(_xAxis == 0) {
            _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);            
        }
    }

    private void Flip(float inputMovement) {
        if((_lookRight && inputMovement < 0) || (!_lookRight && inputMovement > 0)) {
            _lookRight = !_lookRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void Attack() {
        _canNextAttack = false;
        _animator.Play("Attack");
    }

    private void ActiveAttackCollider() {
        _attackCollider.SetActive(true);
    }

    private void DeactiveAttackCollider() {
        _attackCollider.SetActive(false);
    }

    private void AvailableNextAttack() {
        _canNextAttack = true;
    }

    #endregion
}
