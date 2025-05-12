using UnityEngine;

public enum PlacementState
{
    Fixed,
    Valid,
    Invalid
}

public class PlacementHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color validColor;
    [SerializeField] private Color invalidColor;
    [SerializeField] private GameObject towerRange;
    private Color fixedColor;
    [HideInInspector] public bool hasValidPlacement;
    [HideInInspector] public bool isFixed;
    private int obstacleNumber;

    private void Awake()
    {
        hasValidPlacement = true;
        isFixed = true;
        obstacleNumber = 0;
        if (spriteRenderer != null)
        {
            fixedColor = spriteRenderer.color;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFixed) { return; }
        obstacleNumber++;
        SetPlacementState(PlacementState.Invalid);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isFixed) { return; }
        obstacleNumber--;
        if (obstacleNumber <= 0)
        {
            SetPlacementState(PlacementState.Valid);
        }
    }

    public void SetPlacementState(PlacementState p_state)
    {
        if (p_state == PlacementState.Fixed)
        {
            isFixed = true;
            hasValidPlacement = true;
            towerRange.SetActive(true);
        }
        else if (p_state == PlacementState.Valid)
        {
            hasValidPlacement = true;
        }
        else
        {
            hasValidPlacement = false;
        }
        SetColor(p_state);
    }

    private void SetColor(PlacementState p_state)
    {
        switch (p_state)
        {
            case PlacementState.Fixed:
                if (spriteRenderer != null)
                    spriteRenderer.color = fixedColor;
                break;
            case PlacementState.Valid:
                if (spriteRenderer != null)
                    spriteRenderer.color = validColor;
                break;
            case PlacementState.Invalid:
                if (spriteRenderer != null)
                    spriteRenderer.color = invalidColor;
                break;
            default:
                if (spriteRenderer != null)
                    spriteRenderer.color = fixedColor;
                break;
        }
    }
}
