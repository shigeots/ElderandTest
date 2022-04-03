using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClawAttackCollider : BossAttackCollider, ISetDamage, IGetDamage {

    #region MonoBehaviour methods

    private void Start() {
        SetDamage();
    }

    #endregion

    #region Public methods

    public void SetDamage() {
        _attackDamage = _bossCoreController.clawDamage;
    }

    public int GetDamage() {
        return _attackDamage;
    }

    #endregion
}
