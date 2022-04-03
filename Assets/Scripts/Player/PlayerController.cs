using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Private fields

    [SerializeField] private int _health;
    [SerializeField] private float _speedOfMovement = 5f;
    [SerializeField] private int _attackDamage;
    [SerializeField] private GameObject _attackCollider;
    [SerializeField] private PlayerHealthBarController _playerHealthBarController;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float _xAxis;
    private bool _lookRight = true;
    private bool _canNextAttack = true;

    private const string TAG_DAMAGE_PLAYER = "DamageThePlayer";

    #endregion

    #region Properties

    public int AttackDamage { get => _attackDamage; set => _attackDamage = value; }
    public int Health { get => _health; }

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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag(TAG_DAMAGE_PLAYER) && _health >= 0) {
            int damage = other.gameObject.GetComponent<IGetDamage>().GetDamage();
            _health -= damage;
            _playerHealthBarController.UpdateHealthBar(damage);
            _playerHealthBarController.UpdateHealthText(_health);
        }
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
