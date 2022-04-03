using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackColliderController : MonoBehaviour, IGetDamage {

    [SerializeField] private PlayerController _playerController;

    public int GetDamage() {
        return _playerController.AttackDamage;
    }
}
