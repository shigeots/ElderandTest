using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossColliderController : MonoBehaviour {

    #region Private fields
    
    private BossCoreController _bossCoreController;

    private const string TAG_GROUND = "Ground";
    private const string TAG_DAMAGE_ENEMY = "DamageTheEnemy";

    #endregion

    #region Internal attributes

    [SerializeField] internal bool isGrounded = false;

    #endregion

    #region MonoBehaviour methods

    private void Awake() {
        GetComponents();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag(TAG_GROUND)) {
            isGrounded = true;
        }

        if(other.gameObject.CompareTag(TAG_DAMAGE_ENEMY) && !_bossCoreController.isDead) {
            int damage = other.gameObject.GetComponent<IGetDamage>().GetDamage();
            _bossCoreController.bossTakeDamage.FlickerForDamage();
            _bossCoreController.bossTakeDamage.TakeDamage(damage);
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
