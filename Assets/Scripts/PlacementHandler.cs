using UnityEngine;

public enum PlacementState
{
    Fixed,
    Valid,
    Invalid
}

public class PlacementHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _validColor;
    [SerializeField] private Color _invalidColor;
    [SerializeField] private GameObject _towerRange;
    private Color _fixedColor;
    [HideInInspector] public bool hasValidPlacement;
    [HideInInspector] public bool isFixed;
    private int _obstacleNumber;

    private void Awake()
    {
        hasValidPlacement = true;
        isFixed = true;
        _obstacleNumber = 0;
        if (_spriteRenderer != null)
        {
            _fixedColor = _spriteRenderer.color;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFixed) { return; }
        _obstacleNumber++;
        SetPlacementState(PlacementState.Invalid);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isFixed) { return; }
        _obstacleNumber--;
        if (_obstacleNumber <= 0)
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
            _towerRange.SetActive(true);
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
                if (_spriteRenderer != null)
                    _spriteRenderer.color = _fixedColor;
                break;
            case PlacementState.Valid:
                if (_spriteRenderer != null)
                    _spriteRenderer.color = _validColor;
                break;
            case PlacementState.Invalid:
                if (_spriteRenderer != null)
                    _spriteRenderer.color = _invalidColor;
                break;
            default:
                if (_spriteRenderer != null)
                    _spriteRenderer.color = _fixedColor;
                break;
        }
    }
}
