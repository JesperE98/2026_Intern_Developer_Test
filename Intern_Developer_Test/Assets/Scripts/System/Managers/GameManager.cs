using UnityEngine;

public sealed class GameManager : Manager
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private Board board;

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
                board.DeleteTileGrid();
                break;

            case GameStates.States.Gameplay:
                board.CreateTileGrid();
                break;

            case GameStates.States.GameOver:

                break;
        }
    }
}
