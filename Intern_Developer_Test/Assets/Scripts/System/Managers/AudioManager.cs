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
        
    }

    protected override void OnStateChanged(GameStates.States newState) {
        base.OnStateChanged(newState);

        switch (newState) {

            case GameStates.States.MainMenu:
                break;

            case GameStates.States.Gameplay:
                break;

            case GameStates.States.GameOver:

                break;
        }
    }
}
