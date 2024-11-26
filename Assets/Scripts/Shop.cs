using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TowerPlacer _placer;
    private int _playerDebt = 0;

    public void BeginTransaction(GameObject towerBoughtGO)
    {
        Tower towerBought = towerBoughtGO.GetComponent<Tower>();
        if (!_player.CheckPrice(towerBought))
        {
            return;
        }
        _placer.SetTowerPrefab(towerBought.gameObject);
        _playerDebt = towerBought.GetCost();
    }

    public void EndTransaction()
    {
        _player.DoTransaction(_playerDebt);
    }
}
