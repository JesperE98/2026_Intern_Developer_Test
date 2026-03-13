using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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
    private List<Gem> gemPrefabList = new();
    #endregion

    [Header("Gem Attributes")]
    [SerializeField, Range(0, 1)]
    private float gemSwapDuration = 0.5f;
    [SerializeField, Range(0, 1)]
    private float gemFallDuration = 0.5f;

    private Vector2Int[,] tileGrid = null;
    private readonly List<Object> prefabCopies = new();
    private Gem[,] allGems;
    private GameObject[,] allTiles;
    private Gem selectedGem;


    private void OnEnable() {
        Gem.OnSelectGem += SelectGem;
    }
    private void OnDisable() {
        Gem.OnSelectGem -= SelectGem;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tileGrid = new Vector2Int[width, height];
        allGems = new Gem[width, height];
        allTiles = new GameObject[width, height];
    }

    public void CheckForMatches() {
        List<Gem> startMatches = GetAllMatchesOnBoard();

        if (startMatches.Count >= 3) {
            StartCoroutine(ProcessStartMatches(startMatches));
        }
    }

    private void SelectGem(Gem gem) {
        // If selected gem is null meaning no gem has been clicked on yet
        if(selectedGem == null) {
            selectedGem = gem;
            selectedGem.transform.localScale = new Vector3(1.2f, 1.2f, 1);
            Debug.Log("First Gem selected: " + selectedGem.name + " - " + selectedGem.Position);
        }
        else {
            // Check if the player pressed the same gem
            if(selectedGem == gem) {
                selectedGem.transform.localScale = Vector3.one;
                selectedGem = null;
                return;
            }

            // Goes to this block if the first gem is already selected
            if(IsAdjacent(selectedGem.Position, gem.Position)) {
                StartCoroutine(SwapGems(selectedGem, gem));
            }
            else {
                selectedGem.transform.localScale = Vector3.one;
                selectedGem = gem;
                selectedGem.transform.localScale = new Vector3(1.2f, 1.2f, 1);
                Debug.Log("Not Neighbours! New gem chosen");
            }
        }
    }

    // Checks if the difference in X + difference in Y are exactly 1 (means neighbour)
    private bool IsAdjacent(Vector2Int p1, Vector2Int p2) {

        // Checks if the gems position is inside our grid
        if(p1.x < 0 || p1.x > width || p1.y < 0 || p1.y > height ||
            p2.x < 0 || p2.x > width || p2.y < 0 || p2.y > height) {
            return false;
        }

        bool isHorizontalNeighbor = Mathf.Abs(p1.x - p2.x) == 1 && p1.y == p2.y;
        bool isVerticalNeighbor = Mathf.Abs(p1.y - p2.y) == 1 && p1.x == p2.x;

        return isHorizontalNeighbor || isVerticalNeighbor;
    }

    private List<Gem> GetMatchList(Gem gem) {
        List<Gem> horizontalMatches = new List<Gem> { gem };
        horizontalMatches.AddRange(GetGemsInDirection(gem, Vector2Int.left));
        horizontalMatches.AddRange(GetGemsInDirection(gem, Vector2Int.right));

        List<Gem> verticalMatches = new List<Gem> { gem };
        verticalMatches.AddRange(GetGemsInDirection(gem, Vector2Int.up));
        verticalMatches.AddRange(GetGemsInDirection(gem, Vector2Int.down));

        List<Gem> totalMatches = new();

        if (horizontalMatches.Count >= 3) totalMatches.AddRange(horizontalMatches);
        if(verticalMatches.Count >= 3) totalMatches.AddRange(verticalMatches);

        return totalMatches;
    }

    private List<Gem> GetGemsInDirection(Gem startGem, Vector2Int direction) {
        List<Gem> matches = new();
        Vector2Int nextPos = startGem.Position + direction;

        while(nextPos.x >= 0 && nextPos.x < width && nextPos.y >= 0 && nextPos.y < height) {
            Gem nextGem = allGems[nextPos.x, nextPos.y];

            if(nextGem != null && nextGem.Data.Type == startGem.Data.Type) {
                matches.Add(nextGem);
                nextPos += direction;
            }
            else {
                break;
            }
        }

        return matches;
    }

    private List<Gem> GetAllMatchesOnBoard() {
        List<Gem> totalMatches = new();

        for(int x = 0; x < width; x++) {
            for(int y = 0;  y < height; y++) {
                Gem gem = allGems[x, y];

                if(gem != null) {
                    List<Gem> matches = GetMatchList(gem);

                    if(matches.Count >= 3) {
                        foreach(var match in matches) {
                            totalMatches.Add(match);
                        }
                    }
                }
            }
        }
        return new List<Gem>(totalMatches);
    }

    private void ClearMatches(List<Gem> matchSet) {
        foreach(Gem gem in matchSet) {
            if (gem == null) continue;

            int scoreValue = gem.Data.Score;
            GameManager.Instance.Score += scoreValue;

            allGems[gem.Position.x, gem.Position.y] = null;

            Destroy(gem.gameObject);
        }
    }

    #region State Methods
    // Creates a grid
    public void CreateTileGrid() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {

                Vector3 worldPos = new Vector3(x, y, 0);

                // -- Create a tile copy of tile prefab
                GameObject tileCopy = Instantiate(tilePrefab, worldPos, Quaternion.identity) as GameObject;
                tileCopy.transform.parent = this.transform;
                tileCopy.name = $"Tile[{x}, {y}]";

                allTiles[x, y] = tileCopy;

                // Create Gems copies of gem prefabs in gems array and store them in 2D array
                int gemToUse = Random.Range(0, gemPrefabList.Count);
                Gem gemCopy = Instantiate(gemPrefabList[gemToUse], worldPos, Quaternion.identity) as Gem;
                gemCopy.transform.parent = tileCopy.transform;
                gemCopy.Position = new Vector2Int(x, y);
                allGems[x, y] = gemCopy;

                prefabCopies.Add(tileCopy);
                prefabCopies.Add(gemCopy);
            }
        }

        CheckForMatches();
    }

    public void DeleteTileGrid() {

        foreach(Object obj in prefabCopies) {
            Destroy(obj);
        }

        prefabCopies.Clear();
    }
    #endregion

    #region Helper Methods
    private void PerformMatrixSwap(Gem g1, Gem g2) {
        // Update their positions in the allGems matric
        allGems[g1.Position.x, g1.Position.y] = g2;
        allGems[g2.Position.x, g2.Position.y] = g1;

        // Swap the gem positions
        Vector2Int tempPos = g1.Position;
        g1.Position = g2.Position;
        g2.Position = tempPos;
    }

    private int GetRandomGem() {
        int rand = Random.Range(0, gemPrefabList.Count);
        return rand;
    }

    #endregion

    #region IEnumerator Methods

    private IEnumerator ProcessStartMatches(List<Gem> matches) {
        ClearMatches(matches);
        yield return StartCoroutine(RefillBoard());
    }
    private IEnumerator SwapGems(Gem g1, Gem g2) {
        PerformMatrixSwap(g1, g2);
        yield return StartCoroutine(AnimateSwap(g1, g2));

        List<Gem> matchesG1 = GetMatchList(g1);
        List<Gem> matchesG2 = GetMatchList(g2);

        if(matchesG1.Count >= 3 || matchesG2.Count >= 3) {
            Debug.Log("FOUND A MATCH!");
            List<Gem> allMatches = new(matchesG1);
            allMatches.AddRange(matchesG2);

            selectedGem = null;

            ClearMatches(new List<Gem>(allMatches));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(RefillBoard());
        }
        else {
            Debug.Log("NO MATCH FOUND! SWAPPING BACK...");
            PerformMatrixSwap(g1, g2);

            // Animate back
            yield return StartCoroutine(AnimateSwap(g1, g2));
            selectedGem = null;
        }
    }

    private IEnumerator AnimateSwap(Gem g1, Gem g2) {

        Transform parent1 = g1.transform.parent;
        Transform parent2 = g2.transform.parent;

        Vector3 startPos1 = parent1.position;
        Vector3 startPos2 = parent2.position;

        float elapsed = 0f;

        while (elapsed < gemSwapDuration) {
            elapsed += Time.deltaTime;
            float t = elapsed / gemSwapDuration;

            g1.transform.position = Vector3.Lerp(startPos1, startPos2, t);
            g2.transform.position = Vector3.Lerp(startPos2, startPos1, t);

            yield return null;
        }

        g1.transform.SetParent(parent2);
        g2.transform.SetParent(parent1);

        g1.transform.localScale = Vector3.one;
        g2.transform.localScale = Vector3.one;

        selectedGem = null;

        yield return null;
    }

    private IEnumerator RefillBoard() {
        // First of, let already existing gems to fall down
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                // Check if we can find an empty tile
                if (allGems[x, y] == null) {
                    // If we found one, search up for the next gem
                    for(int k = y + 1; k < height; k++) {

                        if(allGems[x, k] != null) {
                            Gem fallingGem = allGems[x, k];
                            // Move the gems in the matrix
                            allGems[x, y] = fallingGem;
                            allGems[x, k] = null;

                            // Update the gems internal position
                            fallingGem.Position = new Vector2Int(x, y);
                            fallingGem.transform.SetParent(allTiles[x, y].transform);

                            // Start a coroutine for the fall animation
                            StartCoroutine(AnimateFall(fallingGem, allTiles[x, y].transform.position));
                            break;
                        }
                    }
                }
            }
        }

        yield return new WaitForSeconds(gemFallDuration);

        // Create new Gems at the top

        yield return StartCoroutine(SpawnNewGems());

        yield return new WaitForSeconds(gemFallDuration);
        CheckForMatches();
    }

    private IEnumerator SpawnNewGems() {
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                if (allGems[x, y] == null) {
                    Vector3 spawnPos = new Vector3(x, height, -0.1f);
                    int randGem = GetRandomGem();

                    Gem newGem = Instantiate(gemPrefabList[randGem], spawnPos, Quaternion.identity);

                    newGem.transform.SetParent(allTiles[x, y].transform);
                    newGem.Position = new Vector2Int(x, y);
                    allGems[x, y] = newGem;

                    StartCoroutine(AnimateFall(newGem, allTiles[x, y].transform.position));
                }
            }
        }
        yield return null;
    }

    private IEnumerator AnimateFall(Gem gem, Vector3 targetPos) {
        float elapsed = 0;

        Vector3 startPost = gem.transform.position;

        while(elapsed < gemFallDuration) {
            elapsed += Time.deltaTime;
            float t = elapsed / gemFallDuration;

            gem.transform.position = Vector3.Lerp(startPost, targetPos, t);
            yield return null;
        }

        gem.transform.position = targetPos;
    }
    #endregion
}
