using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    #region Variables
    private WaveSpawner waveSpawnerReference;
    private float countdown;

    public Text waveCountdownText;
    #endregion

    #region Unity Methods

    private void Start()
    {
        waveSpawnerReference = WaveSpawner.GetInstance();
    }

    private void Update()
    {
        // Keeps countdown from ever being less than 0 (negative)
        countdown = Mathf.Clamp(waveSpawnerReference.countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }


    #endregion
}
