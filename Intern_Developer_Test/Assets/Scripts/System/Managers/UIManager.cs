using UnityEngine;

public sealed class UIManager : Manager
{
    public static UIManager Instance { get; private set; }
    
    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void OnEnable() {
        GameStates.OnStateChanged += OnStateChanged;
        GameStates.OnStateExited += OnStateChanged;
    }

    private void OnDisable() {
        GameStates.OnStateChanged -= OnStateChanged;
        GameStates.OnStateExited -= OnStateChanged;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
