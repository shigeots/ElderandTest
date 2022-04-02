using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossColliderController : MonoBehaviour {

    #region Private attributes
    
    private BossCoreController _bossCoreController;

    private const string TAG_GROUND = "Ground";
    private const string TAG_DAMAGE_ENEMY = "DamageTheEnemy";

    #endregion

    #region Internal attributes

    [SerializeField] internal bool isGrounded = false;

    #endregion

    #region MonoBehaviour methods

    private void Start() {
        GetComponents();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag(TAG_GROUND)) {
            isGrounded = true;
        }

        if(other.gameObject.CompareTag(TAG_DAMAGE_ENEMY)) {
            Debug.Log("Recibir daño");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag(TAG_GROUND)) {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag(TAG_GROUND)) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag(TAG_GROUND)) {
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
