using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    #region Variables
    private PlayerStats playerStatsReference;

    public Text livesText;

    #endregion

    #region Unity Methods

    private void Start()
    {
        playerStatsReference = PlayerStats.GetInstance();
    }

    void Update()
    {
        livesText.text = playerStatsReference.playerLives.ToString() + " LIVES";
    }



    #endregion
}
