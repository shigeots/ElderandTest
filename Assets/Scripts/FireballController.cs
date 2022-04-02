using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {

    #region Private attributes

    [SerializeField] private GameObject _explosionParticle;

    private int _damage = 0;

    private const string TAG_GROUND = "Ground";
    private const string TAG_PLAYER = "Player";

    #endregion

    #region Properties

    public int Damage { get => _damage; set => _damage = value; }

    #endregion

    #region MonoBehaviour methods

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(TAG_GROUND) || other.gameObject.CompareTag(TAG_PLAYER) ) {
            InstantiateExplosion();
            DestroyFireball();
        }
    }

    #endregion

    #region Private methods

    private void DestroyFireball() {
        Destroy(gameObject);
    }

    private void InstantiateExplosion() {
        Instantiate(_explosionParticle, transform.position, transform.rotation);
    }

    #endregion

}
