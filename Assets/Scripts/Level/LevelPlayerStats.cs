using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlayerStats : MonoBehaviour
{
    #region Variables
    public int startMoney;
    public int startLives;

    private PlayerStats playerStatsReference;
    #endregion


    #region Unity Methods

    private void Start()
    {
        playerStatsReference = PlayerStats.GetInstance();

        playerStatsReference.SetMoney(startMoney);
        playerStatsReference.SetLives(startLives);


        playerStatsReference.ResetRounds();
    }



    #endregion
}
