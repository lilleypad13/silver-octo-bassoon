using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    #region Variables
    private PlayerStats playerStatsReference;


    public Text roundsText;
    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        playerStatsReference = PlayerStats.GetInstance();

        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(0.7f);

        while (round < playerStatsReference.rounds)
        {
            round++;
            roundsText.text = round.ToString();

            yield return new WaitForSeconds(0.05f);
        }
    }

    #endregion
}
