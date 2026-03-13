using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System;

public sealed class UIManager : Manager
{
    public static UIManager Instance { get; private set; }

    public event Action OnShowMainMenuHud;
    public event Action OnHideMainMenuHud;
    public event Action OnShowGameplayHud;
    public event Action OnHideGameplayHud;

    [Header("HUDS"), Space(5)]
    [SerializeField] private Hud Hud_MainMenu;
    [SerializeField] private Hud Hud_Gameplay;

    private List<Hud> huds;

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        if (!Hud_MainMenu) Hud_MainMenu = FindFirstObjectByType<MainMenuHud>();
        if(!Hud_Gameplay) Hud_Gameplay = FindFirstObjectByType<GameplayHud>();

        huds = new List<Hud>();
    }

    private void OnDisable() {
        GameStates.Instance.OnStateChanged -= OnStateChanged;
        GameStates.Instance.OnStateExited -= OnStateChanged;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        huds.Add(Hud_MainMenu);
        huds.Add(Hud_Gameplay);
    }
    public override void Initialize() {
        base.Initialize();
    }

    protected override void OnStateChanged(GameStates.States newState) {
        base.OnStateChanged(newState);
        switch (newState) {

            case GameStates.States.MainMenu:
                Hud_Gameplay.Deinitialize();
                Hud_MainMenu.Initialize();
                break;

            case GameStates.States.LevelStarted:
                Hud_MainMenu.Deinitialize();
                Hud_Gameplay.Initialize();
                break;

            case GameStates.States.LevelFailed:

                break;

            case GameStates.States.LevelCompleted:

                break;
        }
    }
}
