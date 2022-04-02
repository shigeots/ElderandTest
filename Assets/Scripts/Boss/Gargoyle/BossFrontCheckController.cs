using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFrontCheckController : MonoBehaviour {

    #region Private attributes

    [SerializeField] private Transform _frontCheck;
    [SerializeField] private Vector2 _boxCastSize;
    [SerializeField] private LayerMask _playerLayer;

    private BossCoreController _bossCoreController;

    #endregion

    #region MonoBehaviour methods

    private void Start() {
        GetComponents();
    }

    private void Update() {
        CheckPlayerIsFront();
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(255, 0, 0, 0.75F);
        Gizmos.DrawWireCube(_frontCheck.position, _boxCastSize);
    }

    #endregion

    #region Private methods

    private void GetComponents() {
        _bossCoreController = GetComponent<BossCoreController>();
    }

    private void CheckPlayerIsFront() {
        var boxCastHit2D = Physics2D.BoxCast(_frontCheck.position, _boxCastSize, 0f, Vector2.zero, .1f, _playerLayer);

        if(boxCastHit2D) {
            _bossCoreController.playerIsClose = true;
        } else {
            _bossCoreController.playerIsClose = false;
        }
    }

    #endregion
}
