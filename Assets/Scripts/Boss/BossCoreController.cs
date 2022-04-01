using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoreController : MonoBehaviour {

    #region Private attributes

    private bool _lookRight = true;

    #endregion

    #region Internal attributes

    [SerializeField] internal float moveSpeed;

    internal BossAirPatrolController bossAirPatrolController;
    internal Rigidbody2D bossRigidbody2D;

    #endregion

    #region MonoBehaviour methods

    private void Start() {
        GetComponents();
    }

    private void Update() {
        
    }

    #endregion

    #region Private methods

    private void GetComponents() {
        bossAirPatrolController = GetComponent<BossAirPatrolController>();
        bossRigidbody2D = GetComponent<Rigidbody2D>();
    }

    #endregion

    internal void Flip() {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
}
