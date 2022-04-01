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
    private const string DIE_ANIMATION = "Die";
    private const string FIREBALL_FROM_GROUND_ANIMATION = "FireballFromGround";
    private const string FIREBALL_FROM_AIR_ANIMATION = "FireballFromAir";
    private const string IDLE_ON_GROUND_ANIMATION = "IdleOnGround";

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

    #region Internal methods

    internal void PlayFlyAnimation() {
        ChangeAnimationState(FLY_ANIMATION);
    }

    internal void PlayClawAttackAnimation() {
        ChangeAnimationState(CLAW_ATTACK_ANIMATION);
    }

    internal void PlayFireballFromGroundAnimation() {
        ChangeAnimationState(FIREBALL_FROM_GROUND_ANIMATION);
    }

    internal void PlayFireballFromAirAnimation() {
        ChangeAnimationState(FIREBALL_FROM_AIR_ANIMATION);
    }

    internal void PlayIdleOnGroundAnimation() {
        ChangeAnimationState(IDLE_ON_GROUND_ANIMATION);
    }

    internal void PlayDieAnimation() {
        ChangeAnimationState(DIE_ANIMATION);
    }

    #endregion

}
