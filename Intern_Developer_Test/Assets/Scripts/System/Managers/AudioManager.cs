using UnityEngine;

public sealed class AudioManager : Manager
{
    public static AudioManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void OnDisable() {
        GameStates.Instance.OnStateChanged -= OnStateChanged;
        GameStates.Instance.OnStateExited -= OnStateChanged;
    }

    void Start()
    {
        
    }
    public override void Initialize() {
        base.Initialize();

        GameStates.Instance.OnStateChanged += OnStateChanged;
        GameStates.Instance.OnStateExited += OnStateChanged;
    }

    protected override void OnStateChanged(GameStates.States newState) {
        base.OnStateChanged(newState);

        switch (newState) {

            case GameStates.States.MainMenu:
                break;

            case GameStates.States.LevelStarted:
                break;

            case GameStates.States.LevelFailed:

                break;

            case GameStates.States.LevelCompleted:

                break;
        }
    }
}
