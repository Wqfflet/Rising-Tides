using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int width, height;
    [SerializeField] private float tileHeight, tileWidth;
    [SerializeField] private GameObject tilePrefab;
    public List<Tile> tilesClicked = new List<Tile>();
    public int maxShips;

    public Tile[,] gridTiles;

    public int ships = 0;
    public List<Tile> computerTiles = new List<Tile>();

    public int tries = 0;
    public int maxTries;
    private void Awake()
    {
        gridTiles = new Tile[width, height];
        
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
                if (tileClass != null)
                {
                    tileClass.Setup(new Vector2Int(x, y));
                    gridTiles[x, y] = tileClass;
                }
            }
        }
    }

    private void Start()
    {
        placeRandomShape();
    }

    private void placeRandomShape()
    {
        int[,] Lshape = new int[,] {
            {1, 0, 0},
            {1, 0, 0},
            {1, 1, 1},
        };

        int[,] SLshape = new int[,] {
            {1, 0, 0},
            {1, 1, 0},
            {0, 0, 0},
        };

        int[,] Ishape = new int[,] {
            {0, 1, 0},
            {0, 1, 0},
            {0, 1, 0},
        };

        int[,] Ushape = new int[,] {
            {1, 0, 1},
            {1, 0, 1},
            {1, 1, 1},
        };

        int[,] Sshape = new int[,] {
            {1, 0, 0},
            {1, 1, 0},
            {0, 1, 0},
        };

        int[,] Zshape = new int[,] {
            {0, 0, 1},
            {0, 1, 1},
            {0, 1, 0},
        };

        int[,] Blockshape = new int[,] {
            {0, 0, 0},
            {0, 1, 1},
            {0, 1, 1},
        };

        List<int[,]> shapeList = new List<int[,]>() 
        {   Lshape, SLshape, Ishape, Ushape, 
            Sshape, Zshape, Blockshape  };


        while (ships < maxShips && tries < maxTries)
        {
            tries++;

            int randomIndex = Random.Range(0, shapeList.Count);
            int[,] chosenShape = shapeList[randomIndex];

            int shapeWidth = chosenShape.GetLength(0);
            int shapeHeight = chosenShape.GetLength(1);

            int randSpawnX = Random.Range(0, width - shapeWidth + 1);
            int randSpawnY = Random.Range(0, height - shapeHeight + 1);

            bool validSpawn = true;
            List<Tile> tempTiles = new List<Tile>();

            for (int x = 0; x < shapeWidth; x++)
            {
                for (int y = 0; y < shapeHeight; y++)
                {
                    if (chosenShape[x,y] == 1)
                    {
                        int targetX = randSpawnX + x;
                        int targetY = randSpawnY + y;

                        if (targetX < width && targetY < height)
                        {
                            Tile tile = gridTiles[targetX, targetY];
                            if (tile == null || computerTiles.Contains(tile))
                            {
                                validSpawn = false;
                                break;
                            }
                            tempTiles.Add(tile);
                        } else
                        {
                            validSpawn = false;
                            break;
                        }
                    }
                }
                if (validSpawn == false) {
                    break;
                }
            }

            if (validSpawn == true)
            {
                foreach (Tile tile in tempTiles)
                {
                    tile.HighlightTile();
                    computerTiles.Add(tile);
                }
                ships++;
            }
        }
    }
}
