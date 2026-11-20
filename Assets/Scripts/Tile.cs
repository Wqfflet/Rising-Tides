using UnityEngine;

public class Tile : MonoBehaviour
{
    
    [SerializeField] private Color color;

    [SerializeField] private Color color2;

    [SerializeField] private Color color3;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public Vector2Int gridPosition;
    public bool hasShip = false;

    public void setPos(Vector2Int pos) 
    {
        gridPosition = pos;
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
        
    }
}
