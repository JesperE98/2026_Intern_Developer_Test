using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class GameStates : MonoBehaviour {

    public enum States {
        None = 0,
        MainMenu = 1,
        Gameplay = 2,
        GameOver = 3,
    };

    public static GameStates Instance { get; private set; }

    public static event Action<States> OnStateChanged;
    public static event Action<States> OnStateExited;

    [SerializeField] private States startState;
    private States currentState = States.None;

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(Instance);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void Start() {
        OnSwitchState(startState);
    }

    public void OnSwitchState(States newState) {

        if(newState == currentState || newState == States.None) {
            Debug.LogWarning("Skipping execution since new state was the same as current state. CurrentState: " + currentState + "  newState: " + newState);
            return;
        }

        OnExitState(currentState);
        currentState = newState;
        OnEnterState(newState);
    }

    void OnEnterState(States newState) {
        OnStateChanged.Invoke(newState);
    }

    void OnExitState(States outState) {
        OnStateExited.Invoke(outState);
    }

    public States GetCurrentState() => currentState;
}
