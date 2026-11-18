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

    public void Setup(Vector2Int pos) 
    {
        gridPosition = pos;
        tileID = $"{pos.x:D2}{pos.y:D2}";
    }
    private void Awake()
{
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.color = color;
}
    public void HighlightTile()
    {
    spriteRenderer.color = color3;
    }
    
    private void OnMouseEnter()
    {
        spriteRenderer.color = color2;
    }

    private void OnMouseExit()
    {
        if (!GridManager.Instance.computerTiles.Contains(this))
        {
            spriteRenderer.color = color;
        } else {
        spriteRenderer.color = color3;
        }
    }

    private void OnMouseDown()
    {
        if (gridPosition.x == 0 || gridPosition.x == 9 || gridPosition.y == 0 || gridPosition.y == 9)
        {
            return;
        }
        if (!GridManager.Instance.tilesClicked.Contains(this)) {
            spriteRenderer.color = color3;
            GridManager.Instance.tilesClicked.Add(this); 
        } else {
        spriteRenderer.color = color;
        GridManager.Instance.tilesClicked.Remove(this);
        }
    }
}
