using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceLogic : MonoBehaviour
{
    #region Variables
    private WaveSpawner waveSpawnerReference;

    public Text timerText;

    #endregion



    #region Unity Methods

    private void Start()
    {
        waveSpawnerReference = WaveSpawner.GetInstance();
    }

    public void PassTimerText()
    {
        waveSpawnerReference.waveCountdownText = timerText;
    }


    #endregion
}
