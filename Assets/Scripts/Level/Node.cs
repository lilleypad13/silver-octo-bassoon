using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    #region Variables

    private PlayerStats playerStatsReference;

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    BuildManager buildManager;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    #endregion


    #region Unity Methods

    private void Start()
    {

        playerStatsReference = PlayerStats.GetInstance();

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.GetInstance();
        Debug.Log("Node has acquired buildManager instance: " + buildManager);
    }

    // Helper method for locating true position to place turret relative to node
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        //Build a turret
        //GameObject turretToBuild = buildManager.GetTurretToBuild();
        //turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);

        //buildManager.BuildTurretOn(this);

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBluePrint blueprint)
    {
        if (playerStatsReference.playerMoney < blueprint.cost)
        {
            Debug.Log("Not enough money to build that");
            return;
        }

        playerStatsReference.AddMoney(-blueprint.cost);

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBluePrint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret built");
    }

    public void UpgradeTurret()
    {
        if (playerStatsReference.playerMoney < turretBluePrint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that");
            return;
        }

        playerStatsReference.AddMoney(-turretBluePrint.upgradeCost);

        // Remove old turret
        Destroy(turret);

        // Building the new upgraded turret
        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Turret upgraded");
    }

    public void SellTurret()
    {
        playerStatsReference.AddMoney(turretBluePrint.GetSellAmount());

        // Spawn cool effect
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        // Actions to reset the node after a turret is removed
        Destroy(turret);
        turretBluePrint = null;
        isUpgraded = false;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }


    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    #endregion
}
