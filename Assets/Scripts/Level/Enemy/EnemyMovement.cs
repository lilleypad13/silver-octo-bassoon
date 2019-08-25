using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    #region Variables
    private PlayerStats playerStatsReference;
    private WaveSpawner waveSpawnerReference;

    private Transform target;
    private int wavepointIndex = 0;

    private Enemy enemy;

    #endregion

    #region Unity Methods

    private void Start()
    {
        playerStatsReference = PlayerStats.GetInstance();
        waveSpawnerReference = WaveSpawner.GetInstance();

        enemy = GetComponent<Enemy>();

        target = Waypoints.points[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        // Find better way to reset speed instead of doing it every frame
        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        playerStatsReference.LoseLives(1);
        waveSpawnerReference.enemiesAlive--;
        Destroy(gameObject);
    }

    #endregion
}
