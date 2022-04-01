using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossColliderController : MonoBehaviour {

    #region Private attributes
    
    private BossCoreController _bossCoreController;

    private const string TAG_GROUND = "Ground";

    #endregion

    [SerializeField] internal bool isGrounded = false;

    #region MonoBehaviour methods

    private void Start() {
        GetComponents();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(TAG_GROUND)) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag(TAG_GROUND)) {
            isGrounded = false;
        }
    }

    #endregion

    #region Private methods

    private void GetComponents() {
        _bossCoreController = GetComponent<BossCoreController>();
    }

    #endregion
}
