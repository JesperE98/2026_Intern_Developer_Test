using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    protected virtual void OnStateChanged(GameStates.States newState) => Debug.Log(gameObject.name + " changed state: " + newState);
}
