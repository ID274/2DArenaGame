using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform parent; // Reference to the parent object
    public float moveSpeed = 5f; // Speed of movement
    public float maxDistance = 5f; // Maximum allowed distance from the parent


    private void Awake()
    {
        parent = transform.parent;
    }
    void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 since we are working in 2D

        // Calculate the direction from the object to the mouse
        Vector3 directionToMouse = (mousePosition - transform.position).normalized;

        // Rotate the object to face the mouse
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Calculate the target position (move towards the mouse)
        Vector3 targetPosition = transform.position + directionToMouse * moveSpeed * Time.deltaTime;
        
        if (!Input.GetMouseButton(1))
        {
            // Check the distance to the parent
            if (Vector3.Distance(parent.position, targetPosition) > maxDistance)
            {
                // Limit the target position to the maximum distance from the parent
                targetPosition = parent.position + (targetPosition - parent.position).normalized * maxDistance;
            }
            if (Vector3.Distance(parent.position, targetPosition) < maxDistance)
            {
                // Limit the target position to the maximum distance from the parent
                targetPosition = parent.position + (targetPosition - parent.position).normalized * maxDistance;
            }

            // Move the object towards the target position
            transform.position = targetPosition;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (parent.position.x > transform.position.x)
        {
            Quaternion lookQuaternion = transform.rotation * Quaternion.AngleAxis(180, Vector3.right);
            transform.rotation = lookQuaternion;
        }
    }
}
