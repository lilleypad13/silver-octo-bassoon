using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for determing "what to build next"
public class BuildManager : MonoBehaviour
{
    #region Variables
    static BuildManager instance;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.GetInstance().playerMoney >= turretToBuild.cost; } }

    public GameObject buildEffect;
    public GameObject sellEffect;

    private TurretBluePrint turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;
    #endregion


    #region Unity Methods

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    public static BuildManager GetInstance()
    {
        return instance;
    }

    private void OnDisable()
    {
        // TODO: Will most likely want to put something here to reset certain values
    }

    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }

    #endregion
}
