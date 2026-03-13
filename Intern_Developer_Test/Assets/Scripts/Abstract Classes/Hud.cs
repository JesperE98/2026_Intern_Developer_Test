using UnityEngine;

public abstract class Hud : MonoBehaviour
{
    public virtual void Initialize() { Debug.Log(gameObject.name + " INITIALIZED!"); }
    public virtual void Deinitialize() { Debug.Log(gameObject.name + " DEINITIALIZED!"); }

}
