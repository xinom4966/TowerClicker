using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TowerPlacer : MonoBehaviour
{
    private GameObject _towerPrefab;
    private GameObject _toBuild;
    private Camera _camera;
    private Vector3 _mousePos;
    private GameObject _goldFeedback;
    [SerializeField] private UnityEvent _onTowerPlaced;
    [SerializeField] private UnityEvent _onPlacementCanceled;
    [SerializeField] private GameObject _goldFeedbackPrefab;

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
                _onPlacementCanceled.Invoke();
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
                    _goldFeedback = Instantiate(_goldFeedbackPrefab);
                    _goldFeedback.GetComponentInChildren<GoldFeedBack>().SetDatas(Camera.main.WorldToScreenPoint(handler.transform.position), handler.GetComponentInChildren<Tower>().GetCost());
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
