using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TowerPlacer placer;
    [SerializeField] private GameObject shopGO;
    private int playerDebt = 0;

    public void BeginTransaction(GameObject towerBoughtGO)
    {
        Tower towerBought = towerBoughtGO.GetComponentInChildren<Tower>(true);
        if (towerBought == null)
        {
            return;
        }
        if (!player.CheckPrice(towerBought))
        {
            return;
        }
        placer.SetTowerPrefab(towerBought.transform.parent.gameObject);
        playerDebt = towerBought.GetCost();
        shopGO.SetActive(false);
    }

    public void EndTransaction()
    {
        player.DoTransaction(playerDebt);
        playerDebt = 0;
    }

    public void CancelTransaction()
    {
        playerDebt = 0;
    }
}
