using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacer : MonoBehaviour
{
    protected GameObject _towerPrefab;
    protected GameObject _toBuild;
    protected Camera _camera;
    protected Vector3 _mousePos;

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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _toBuild.transform.Rotate(Vector3.forward, 90);
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
                }
            }
        }
    }

    public void SetTowerPrefab(GameObject p_prefab)
    {
        _towerPrefab = p_prefab;
        PrepareTower();
        EventSystem.current.SetSelectedGameObject(null);
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
