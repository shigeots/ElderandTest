using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackCollider : MonoBehaviour {

    #region Protected fields

    [SerializeField] protected BossCoreController _bossCoreController;
    [SerializeField] protected int _attackDamage;

    #endregion
}
