using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Component Menu", 0)]
public class Board : MonoBehaviour
{
    #region Tile Map Variables
    [Header("Tile Map Settings"), Space(5)]
    [SerializeField, Tooltip("Width value for the board."), Range(0, 15)] 
    private int width = 0;
    [SerializeField, Tooltip("Height value for the board."), Range(0, 15)] 
    private int height = 0;

    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private List<GameObject> gems = new();

    private Vector2Int[,] tileGrid = null;
    private readonly List<GameObject> prefabCopies = new();
    #endregion

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tileGrid = new Vector2Int[width, height];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Creates a grid
    public void CreateTileGrid() {
        for (int x = 1; x < width; x++) {
            for (int y = 1; y < height; y++) {

                Vector3 worldPos = new Vector3(x, y, 0);

                GameObject tileCopy = Instantiate(tilePrefab, worldPos, Quaternion.identity);
                GameObject gemCopy = Instantiate(gems[Random.Range(0, gems.Count)], worldPos, Quaternion.identity);

                tileGrid[x, y] = new Vector2Int(x, y);

                tileCopy.transform.parent = this.transform;
                tileCopy.name = $"Tile[{x}, {y}]";

                gemCopy.transform.parent = tileCopy.transform;

                prefabCopies.Add(tileCopy);
                prefabCopies.Add(gemCopy);
            }
        }
    }

    public void DeleteTileGrid() {
        prefabCopies.Clear();
    }

    void SpawnGemAtTilePosition(Vector3 pos) {
        
    }
}
