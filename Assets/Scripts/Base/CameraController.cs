using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    private bool isLocked = false;
    #endregion


    #region Unity Methods

    private void Update()
    {
        //if (LevelManager.GameIsOver)
        //{
        //    this.enabled = false;
        //    return;
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isLocked = !isLocked;
        }

        if(isLocked == false)
        {
            MoveCamera();
            ZoomCamera();
        }
    }


    private void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        // Want to add some large multiplication factor becasue scrollSpeed is normally a very small number
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }


    private void MoveCamera()
    {
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
    }


    #endregion
}
