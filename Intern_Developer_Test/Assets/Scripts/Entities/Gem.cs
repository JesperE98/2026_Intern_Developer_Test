using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class Gem : MonoBehaviour
{
    [Header("Attributes"), Space(5)]
    [SerializeField]
    private SO_Gem gemData;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = gemData.Sprite;
        spriteRenderer.color = gemData.color;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
