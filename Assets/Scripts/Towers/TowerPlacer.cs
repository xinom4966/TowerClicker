using UnityEngine;
using UnityEngine.Events;

public class TowerPlacer : MonoBehaviour
{
    private GameObject towerPrefab;
    private GameObject toBuild;
    private Camera placerCamera;
    private Vector3 mousePos;
    private GameObject goldFeedback;
    [SerializeField] private UnityEvent onTowerPlaced;
    [SerializeField] private UnityEvent onPlacementCanceled;
    [SerializeField] private GameObject goldFeedbackPrefab;

    private void Awake()
    {
        towerPrefab = null;
        placerCamera = Camera.main;
    }

    private void Update()
    {
        if (towerPrefab != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(toBuild);
                toBuild = null;
                towerPrefab = null;
                onPlacementCanceled.Invoke();
                return;
            }

            mousePos = placerCamera.ScreenToWorldPoint(Input.mousePosition);
            toBuild.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
            if (Input.GetMouseButtonDown(0))
            {
                PlacementHandler handler = toBuild.GetComponent<PlacementHandler>();
                if (handler.hasValidPlacement)
                {
                    handler.SetPlacementState(PlacementState.Fixed);

                    towerPrefab = null;
                    toBuild = null;
                    onTowerPlaced.Invoke();
                    goldFeedback = Instantiate(goldFeedbackPrefab);
                    goldFeedback.GetComponentInChildren<GoldFeedBack>().SetDatas(Camera.main.WorldToScreenPoint(handler.transform.position), handler.GetComponentInChildren<Tower>().GetCost());
                }
            }
        }
    }

    public void SetTowerPrefab(GameObject p_prefab)
    {
        towerPrefab = p_prefab;
        PrepareTower();
    }

    protected virtual void PrepareTower()
    {
        if (toBuild)
        {
            Destroy(toBuild);
        }
        toBuild = Instantiate(towerPrefab);
        toBuild.SetActive(false);

        PlacementHandler handler = toBuild.GetComponent<PlacementHandler>();
        handler.isFixed = false;
        handler.SetPlacementState(PlacementState.Valid);
        toBuild.SetActive(true);
    }
}
