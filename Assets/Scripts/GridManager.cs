using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int width, height;
    [SerializeField] private float tileHeight, tileWidth;
    [SerializeField] private GameObject tilePrefab;
    public List<Tile> tilesClicked = new List<Tile>();
    public int maxSelected;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xPos = x * tileWidth - (width * tileWidth / 2 - tileWidth / 2);
                float yPos = y * tileHeight - (height * tileHeight / 2 - tileHeight); 

                Vector3 spawnPos = new Vector3(xPos, yPos, 0);
                GameObject newTile = Instantiate(tilePrefab, spawnPos, Quaternion.identity, transform);

                newTile.name = $"Tile_{x}{y}";

                Tile tileClass = newTile.GetComponent<Tile>();
                if (tileClass = null)
                {
                    tileClass.Setup(new Vector2Int(x, y));
                }
            }
        }
    }
}
