using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Private attributes

    [SerializeField] private int _health = 100;
    [SerializeField] private float _speedOfMovement = 5f;

    private Rigidbody2D _rigidbody2D;
    private float _xAxis;
    private bool _lookRight = true;

    #endregion

    #region MonoBehaviour methods

    private void Start() {
        GetComponents();
    }

    private void Update() {
        _xAxis = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate() {
        Move();
    }

    #endregion

    #region Private methods

    private void GetComponents() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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

    #endregion
}
