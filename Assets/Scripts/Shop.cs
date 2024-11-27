using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TowerPlacer _placer;
    [SerializeField] private GameObject _shopGO;
    private int _playerDebt = 0;

    public void BeginTransaction(GameObject towerBoughtGO)
    {
        Tower towerBought = towerBoughtGO.GetComponentInChildren<Tower>(true);
        if (towerBought == null)
        {
            return;
        }
        if (!_player.CheckPrice(towerBought))
        {
            return;
        }
        _placer.SetTowerPrefab(towerBought.transform.parent.gameObject);
        _playerDebt = towerBought.GetCost();
        _shopGO.SetActive(false);
    }

    public void EndTransaction()
    {
        _player.DoTransaction(_playerDebt);
    }
}
