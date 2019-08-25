using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    #region Variables
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;


    #endregion


    #region Unity Methods

    public int GetSellAmount()
    {
        return cost / 2;
    }



    #endregion
}
