using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    #region Variables

    // to hold the enemy prefab to spawn for this wave
    public GameObject enemy;

    // to determine the number of enemies to spawn in this wave
    public int count;

    // how fast each individual enemy in the wave spawns
    public float rate;



    #endregion


}
