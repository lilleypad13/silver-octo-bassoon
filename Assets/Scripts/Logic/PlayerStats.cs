using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Variables
    static PlayerStats playerStatsInstance;

    private int _playerMoney;
    public int playerMoney { get { return _playerMoney; } }

    private int _playerLives;
    public int playerLives { get { return _playerLives; } }

    private int _rounds;
    public int rounds { get { return _rounds; } }

    #endregion


    #region Unity Methods

    private void Start()
    {
        if (playerStatsInstance != null)
        {
            Debug.Log("More than one Player Stats Instance in scene");
            return;
        }
        playerStatsInstance = this;

        // Initializes playerLives at value greater than 0 so game does not immediately end
        _playerLives = 1;

        _rounds = 0;
    }

    public static PlayerStats GetInstance()
    {
        return playerStatsInstance;
    }

    public void SetMoney(int money)
    {
        _playerMoney = money;
    }

    public void AddMoney(int moneyToAdd)
    {
        _playerMoney += moneyToAdd;
    }

    public void SetLives(int numberOfLives)
    {
        _playerLives = numberOfLives;
    }

    public void LoseLives(int numberOfLivesLost)
    {
        _playerLives -= numberOfLivesLost;
    }

    public void SetRounds(int numberOfRounds)
    {
        _rounds = numberOfRounds;
    }

    public void ResetRounds()
    {
        _rounds = 0;
    }

    public void AddRound()
    {
        _rounds++;
    }

    #endregion
}
