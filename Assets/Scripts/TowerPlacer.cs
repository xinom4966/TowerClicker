using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TowerPlacer : MonoBehaviour
{
    private GameObject _towerPrefab;
    private GameObject _toBuild;
    private Camera _camera;
    private Vector3 _mousePos;
    [SerializeField] private UnityEvent _onTowerPlaced;

    private void Awake()
    {
        _towerPrefab = null;
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_towerPrefab != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(_toBuild);
                _toBuild = null;
                _towerPrefab = null;
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                _toBuild.SetActive(false);
            }
            else
            {
                _toBuild.SetActive(true);
            }

            _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _toBuild.transform.position = new Vector3(_mousePos.x, _mousePos.y, 0);
            if (Input.GetMouseButtonDown(0))
            {
                PlacementHandler handler = _toBuild.GetComponent<PlacementHandler>();
                if (handler.hasValidPlacement)
                {
                    handler.SetPlacementState(PlacementState.Fixed);

                    _towerPrefab = null;
                    _toBuild = null;
                    _onTowerPlaced.Invoke();
                }
            }
        }
    }

    public void SetTowerPrefab(GameObject p_prefab)
    {
        _towerPrefab = p_prefab;
        PrepareTower();
    }

    protected virtual void PrepareTower()
    {
        if (_toBuild)
        {
            Destroy(_toBuild);
        }
        _toBuild = Instantiate(_towerPrefab);
        _toBuild.SetActive(false);

        PlacementHandler handler = _toBuild.GetComponent<PlacementHandler>();
        handler.isFixed = false;
        handler.SetPlacementState(PlacementState.Valid);
    }
}
