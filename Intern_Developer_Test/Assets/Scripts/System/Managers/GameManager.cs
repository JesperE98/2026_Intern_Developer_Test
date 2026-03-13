using UnityEngine;
using UnityEngine.InputSystem;

public sealed class GameManager : Manager
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private Board board;
    [SerializeField]
    private float playerScore = 0;
    [SerializeField]
    private float playerHighScore = 0;

    public float Score {
        get => playerScore;
        set { 
            playerScore = value;
        } 
    }

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(this);
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
    }
    private void OnEnable() {
        GameStates.OnStateChanged += OnStateChanged;
        GameStates.OnStateExited += OnStateChanged;
    }

    private void OnDisable() {
        GameStates.OnStateChanged -= OnStateChanged;
        GameStates.OnStateExited -= OnStateChanged;
    }
    void Start()
    {

    }

    void Update()
    {
        if (Keyboard.current.digit1Key.isPressed) {
            GameStates.Instance.OnSwitchState(GameStates.States.MainMenu);
        }
        if (Keyboard.current.digit2Key.isPressed) {
            GameStates.Instance.OnSwitchState(GameStates.States.LevelStarted);
        }
        if (Keyboard.current.digit3Key.isPressed) {
            GameStates.Instance.OnSwitchState(GameStates.States.LevelFailed);
        }
    }

    protected override void OnStateChanged(GameStates.States newState) {
        base.OnStateChanged(newState);

        switch (newState) {

            case GameStates.States.MainMenu:
                board.DeleteTileGrid();
                break;

            case GameStates.States.LevelStarted:
                board.CreateTileGrid();
                board.CheckForMatchesAtStart();
                break;

            case GameStates.States.LevelFailed:

                break;

            case GameStates.States.LevelCompleted:

                break;
        }
    }

    void UpdatePlayerScore(float score) {
        playerScore += score;
    }

    void UpdatePlayerHighScore(float score) {
        if(score > playerHighScore) {
            playerHighScore = score;
        }
    }
}
