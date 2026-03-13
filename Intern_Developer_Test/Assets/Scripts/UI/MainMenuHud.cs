using UnityEngine;
using TMPro;
using UnityEngine.UI;

public sealed class MainMenuHud : Hud
{
    [Header("UI Elements"), Space(5)]
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TMP_Text highscoreText;

    private void OnEnable() {
        playButton.onClick.AddListener(Play);
        quitButton.onClick.AddListener(Quit);
    }

    private void OnDisable() {
        playButton.onClick.RemoveListener(Play);
        quitButton.onClick.RemoveListener(Quit);
    }

    public override void Initialize() {
        base.Initialize();

        gameObject.SetActive(true);
        highscoreText.text = "Highscore: " + GameManager.Instance.Highscore.ToString();
    }

    public override void Deinitialize() {
        base.Deinitialize();

        gameObject.SetActive(false);
    }

    private void Play() {
        GameStates.Instance.OnSwitchState(GameStates.States.LevelStarted);
    }

    private void Quit() {
        Application.Quit();
    }
}
