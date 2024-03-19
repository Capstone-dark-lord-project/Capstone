using UnityEngine;

public class DragDrop3D : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private float zCoordinate;
    private Vector3 startPosition;
    private bool dragging = false;
    private GameObject collidedObject = null; // To keep track of the current collided object

    void Awake()
    {
        mainCamera = Camera.main;
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = transform.position;
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
            // Check if collided with another card, if so, potentially combine them
            if (collidedObject != null)
            {
                // Notify the PlayerManager or a specific manager for combining logic
                FindObjectOfType<PlayerManager>().CombineCards(gameObject, collidedObject);
                collidedObject = null; // Reset for next drag operation
            }
            else
            {
                // If no combination is possible, return to the original position
                transform.position = startPosition;
            }
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
        transform.position = GetMouseWorldPos() + offset + Vector3.up * 0.1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ResourceCardTag")) // Make sure your cards have this tag set in the editor
        {
            Debug.Log($"COLLIDED");
            collidedObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == collidedObject)
        {
            collidedObject = null; // No longer colliding with this object
        }
    }
}
