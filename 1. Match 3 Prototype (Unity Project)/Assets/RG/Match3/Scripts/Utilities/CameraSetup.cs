using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    void Start()
    {
        float xPos = GameManager.instance.currentGrid.GridSizeX;
        float yPos = GameManager.instance.currentGrid.GridSizeY;

        Vector3 cameraPosToStart = new Vector3(xPos / 2, yPos / 2, transform.position.z);

        transform.position = cameraPosToStart;
    }
}
