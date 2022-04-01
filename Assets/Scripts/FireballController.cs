using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {

    #region Private attributes

    [SerializeField] private int _damage = 0;

    private const string TAG_GROUND = "Ground";

    #endregion

    #region Properties

    public int Damage { get => _damage; set => _damage = value; }

    #endregion

    #region MonoBehaviour methods

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(TAG_GROUND)) {
            DestroyFireball();
        }
    }

    #endregion

    #region Private methods

    private void DestroyFireball() {
        Destroy(gameObject);
    }

    #endregion

}
