using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTakeDamage : MonoBehaviour
{
    #region Private fields

    [SerializeField] private float _flickerDuration;
    [SerializeField] private Material _damageFlickerMaterial;

    private BossCoreController _bossCoreController;
    private Material _originalMaterial;
    private Coroutine _flickerCoroutine;

    #endregion

    #region MonoBehaviour methods

    private void Awake() {
        GetComponents();
    }

    private void Start() {
        _originalMaterial = _bossCoreController.spriteRenderer.material;
    }

    #endregion

    #region Private methods

    private void GetComponents() {
        _bossCoreController = GetComponent<BossCoreController>();
    }

    #endregion

    #region Internal methods

    internal void FlickerForDamage() {
        if (_flickerCoroutine != null)
            StopCoroutine(_flickerCoroutine);

        _flickerCoroutine = StartCoroutine(FlickerCoroutine());
    }

    #endregion

    #region Coroutine

    private IEnumerator FlickerCoroutine() {
        _bossCoreController.spriteRenderer.material = _damageFlickerMaterial;

        yield return new WaitForSeconds(_flickerDuration);

        _bossCoreController.spriteRenderer.material = _originalMaterial;

        _flickerCoroutine = null;
    }

    #endregion
}
