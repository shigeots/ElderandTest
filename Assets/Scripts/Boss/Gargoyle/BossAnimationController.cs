using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour {

    #region Private attributes
    
    private BossCoreController _bossCoreController;
    private Animator _animator;

    public string _currentAnimation;

    private const string FLY_ANIMATION = "Fly";
    private const string CLAW_ATTACK_ANIMATION = "ClawAttack";

    #endregion

    #region MonoBehaviour methods

    private void Start() {
        GetComponents();
    }

    #endregion

    #region Private methods

    private void GetComponents() {
        _bossCoreController = GetComponent<BossCoreController>();
        _animator = GetComponent<Animator>();
    }

    private void ChangeAnimationState(string newAnimation) {
        if(_currentAnimation == newAnimation) return;

        _animator.Play(newAnimation);
        _currentAnimation = newAnimation;
    }

    #endregion

    internal void PlayFlyAnimation() {
        ChangeAnimationState(FLY_ANIMATION);
    }

    internal void PlayClawAttackAnimation() {
        ChangeAnimationState(CLAW_ATTACK_ANIMATION);
    }

}
