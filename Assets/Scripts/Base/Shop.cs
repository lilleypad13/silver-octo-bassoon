using UnityEngine;

public class Shop : MonoBehaviour
{
    #region Variables
    public TurretBluePrint standardTurret;
    public TurretBluePrint missileLauncher;
    public TurretBluePrint laserBeamer;

    private BuildManager buildManagerInstance;


    #endregion


    #region Unity Methods

    private void Start()
    {
        buildManagerInstance = BuildManager.GetInstance();
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret Selected.");
        buildManagerInstance.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected.");
        buildManagerInstance.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Beamer Selected.");
        buildManagerInstance.SelectTurretToBuild(laserBeamer);
    }

    #endregion
}
