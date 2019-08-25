using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    #region Variables
    public Text moneyText;

    private PlayerStats playerStatsReference;
    #endregion

    #region Unity Methods

    private void Start()
    {
        playerStatsReference = PlayerStats.GetInstance();
    }

    private void Update()
    {
        moneyText.text = "$" + playerStatsReference.playerMoney.ToString();
    }



    #endregion
}
