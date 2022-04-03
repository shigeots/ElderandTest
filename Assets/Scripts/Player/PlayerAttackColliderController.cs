using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackColliderController : MonoBehaviour, IGetDamage {

    #region Private fields

    [SerializeField] private PlayerController _playerController;

    #endregion

    #region Public methods

    public int GetDamage() {
        return _playerController.AttackDamage;
    }

    #endregion
}
