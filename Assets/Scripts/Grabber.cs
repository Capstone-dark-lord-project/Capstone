using UnityEngine;

public class DragDrop3D : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private float zCoordinate;
    private Vector3 startPosition;
    private bool dragging = false;

    void Awake()
    {
        mainCamera = Camera.main; // Cache the main camera for performance.
        startPosition = transform.position; // Store the original start position.
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    zCoordinate = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
                    offset = gameObject.transform.position - GetMouseWorldPos();
                    dragging = true;
                }
            }
        }

        if (dragging)
        {
            DragObject();
        }

        if (Input.GetMouseButtonUp(0) && dragging)
        {
            dragging = false;
            // Return the object to its start position when the mouse button is released.
            transform.position = startPosition;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private void DragObject()
    {
        transform.position = GetMouseWorldPos() + offset;
    }
}
