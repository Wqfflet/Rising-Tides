using UnityEngine;

public class Tile : MonoBehaviour
{
    
    [SerializeField] private Color color;

    [SerializeField] private Color color2;

    [SerializeField] private Color color3;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public string tileID;
    public Vector2Int gridPosition;
    public bool hasShip = false;
    public GameObject currentShip = null;

    public void Setup(Vector2Int pos) 
    {
        gridPosition = pos;
        tileID = $"{pos.x:D2}{pos.y:D2}";
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.color = color;
    }
    private void OnMouseEnter()
    {
        spriteRenderer.color = color2;
    }

    private void OnMouseExit()
    {
        if (!GridManager.Instance.tilesClicked.Contains(this))
        {
            spriteRenderer.color = color;
        } else {
        spriteRenderer.color = color3;
        }
    }

    private void OnMouseDown()
    {
        if (!GridManager.Instance.tilesClicked.Contains(this))
        {
            if (GridManager.Instance.tilesClicked.Count < GridManager.Instance.maxSelected)
            {
                spriteRenderer.color = color3;
                GridManager.Instance.tilesClicked.Add(this);
            }   
        } else {
        spriteRenderer.color = color;
        GridManager.Instance.tilesClicked.Remove(this);
        }
    }
}
