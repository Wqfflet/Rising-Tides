using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private Color color = Color.white;
    [SerializeField] private Color subcolor = Color.black;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool isSelected = false;    
    private void Awake() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null) {
            spriteRenderer.color = color;
        }
    }
    private void OnMouseDown() {
        Debug.Log("OnMouseDown triggered on tile: " + gameObject.name);

        if (GridManager.Instance != null) {
            GridManager.Instance.tileSelection(this);
        }
    }

    public void SetSelected(bool selected) {
        isSelected = selected;
        if (spriteRenderer != null) {
            Color targetColor = isSelected ? subcolor : color;
            spriteRenderer.color = targetColor;
        }
    }

}
