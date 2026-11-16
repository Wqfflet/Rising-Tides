using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {
    public static GridManager Instance;
    [SerializeField] private int width, height;
    [SerializeField] private Tile tilePrefab;

    [SerializeField] private List<Tile> selectedTiles = new List<Tile>();
    [SerializeField] private List<Tile> computerSelectedTiles = new List<Tile>();
    [SerializeField] private List<Tile> allSpawnedTiles = new List<Tile>();
    [SerializeField] private int maxTiles = 5;

    [SerializeField] private bool gridExists = false;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    private void Update() {
        if (Keyboard.current == null) return;

        if (Keyboard.current.downArrowKey.wasPressedThisFrame) {
            if (!gridExists)
            {
                GenerateGrid();
                gridExists = true;
            }
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame) {
            if (gridExists) {
                ComputerChooseTiles();
            }
        }
    }
    private void GenerateGrid() {

        float xOffset = -width / 2f;
        float yOffset = -height / 2f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 spawnPos = new Vector3(xOffset + x + 0.5f, yOffset + y + 0.5f, 0);

                Tile spawnedTile = Instantiate(tilePrefab, spawnPos, Quaternion.identity);
                spawnedTile.name = $"Tile x{x}y{y}";
            }
        }
    }

    public void tileSelection(Tile tile) {
        if (selectedTiles.Contains(tile)) {
            selectedTiles.Remove(tile);
            tile.SetSelected(false);
        } else {
            if (selectedTiles.Count < maxTiles) {
                selectedTiles.Add(tile);
                tile.SetSelected(true);

                Debug.Log($"Selected: {selectedTiles.Count}/{maxTiles}");
            } else {
                Debug.Log("You can only select 5 tiles.");
            }
        }
    }

// almost completely from the grid guy youtube tutorial, if you dont understand go 
// back to that one because this makes 0 sense
    private void ComputerChooseTiles() {
        computerSelectedTiles.Clear();
        List<Tile> availableTiles = new List<Tile>(allSpawnedTiles);

        int tilesToPick = Mathf.Min(maxTiles, availableTiles.Count);

        for (int i = 0; i < tilesToPick; i++) {
            int randomIndex = Random.Range(0, availableTiles.Count);
            Tile chosenTile = availableTiles[randomIndex];
            
            computerSelectedTiles.Add(chosenTile);
            availableTiles.RemoveAt(randomIndex);
        }

        Debug.Log("- Computer -");
        foreach (var tile in computerSelectedTiles) {
            Debug.Log($"Computer chose: {tile.name}");
        }
    }
}
