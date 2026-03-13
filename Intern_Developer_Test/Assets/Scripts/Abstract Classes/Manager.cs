using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    public virtual void Initialize() {
        GameStates.Instance.OnStateChanged += OnStateChanged;
        GameStates.Instance.OnStateExited += OnStateChanged;
    }

    protected virtual void OnStateChanged(GameStates.States newState) => Debug.Log(gameObject.name + " changed state: " + newState);
}
