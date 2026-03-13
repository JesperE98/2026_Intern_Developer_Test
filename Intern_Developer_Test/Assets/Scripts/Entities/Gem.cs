using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class Gem : MonoBehaviour
{
    public static event Action<Gem> OnSelectGem;

    [Header("Attributes"), Space(5)]
    [SerializeField]
    private SO_Gem data;
    public Vector2Int Position;

    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    public SO_Gem Data { get; private set;  }

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = data.Sprite;
        spriteRenderer.color = data.color;
        spriteRenderer.sortingOrder = 1;

        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;

        Data = data;
    }

    void Start()
    {
        this.transform.position = new Vector3(Position.x, Position.y, -0.1f);
    }

    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame) {

            Vector3 mousePos = Mouse.current.position.ReadValue();

            mousePos.z = Mathf.Abs(Camera.main.transform.position.z);

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);

            RaycastHit2D hit = Physics2D.Raycast(worldPos2D, Vector2.zero);

            if(hit.collider != null && hit.collider.gameObject == gameObject) {
                OnPressed();
            }
        }
    }

    private void OnPressed() {
        OnSelectGem?.Invoke(this);
    }
}
