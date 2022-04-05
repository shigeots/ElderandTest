using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class EndGameScreenController : MonoBehaviour {

    #region Private fields

    [SerializeField] Text _winText;
    [SerializeField] Text _loseText;
    [SerializeField] private RectTransform _endGamePanel;

    private Canvas _endGameScreenCanvas;

    #endregion

    #region MonoBehaviour methods

    private void Awake() {
        SubscribeMethodsToEvents();
    }

    private void OnDestroy() {
        UnsubscribeMethodsToEvents();
    }

    #endregion

    #region Private methods

    private void ResumeTime() {
            Time.timeScale = 1;
        }

    private void StopTime() {
            Time.timeScale = 0;
    }

    private void ShowEndGamePanel() {
        StopTime();

        _endGamePanel.DOAnchorPos(new Vector2(0,0), 1.5f)
                .SetEase(Ease.OutBack)
                .SetLink(gameObject)
                .SetUpdate(true);
    }

    public void SubscribeMethodsToEvents() {
        EventObserver.WinGameEvent += WinGame;
        EventObserver.LoseGameEvent += LoseGame;
    }

    public void UnsubscribeMethodsToEvents() {
        EventObserver.WinGameEvent -= WinGame;
        EventObserver.LoseGameEvent -= LoseGame;
    }

    #endregion

    #region Internal methods

    internal void WinGame() {
        _winText.enabled = true;
        ShowEndGamePanel();
    }

    internal void LoseGame() {
        _loseText.enabled = true;
        ShowEndGamePanel();
    }

    #endregion

    #region Public methods

    public void OnClickPlayAgainButton() {
        ResumeTime();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void OnClickExitButton() {
        ResumeTime();
        Application.Quit();
    }

    #endregion
}
