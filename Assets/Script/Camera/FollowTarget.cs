using UnityEngine;

// This script can be used to match the positional values of a
// target transform. It's meant to be attached to the main camera
// so that the camera follows the player (or whatever the target is).
public class FollowTarget : MonoBehaviour
{
    [Header("Transform to Follow")]
    [SerializeField] private Transform targetTransform;

    [Header("Configuration")]
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;
    [SerializeField] private Vector2 offset = Vector2.zero;

    /*
    [Header("Limitation")]
    [SerializeField] private float minX = 0;
    [SerializeField] private float minY = 0;
    [SerializeField] private float maxX = 0;
    [SerializeField] private float maxY = 0;
    */

    private Transform originalTargetTransform;

    private void Start() 
    {
        originalTargetTransform = targetTransform;
    }

    private void LateUpdate() 
    {
        // if we don't have a target transform, don't update
        if (targetTransform == null) 
        {
            return;
        }

        float newPosX = this.transform.position.x;
        float newPosY = this.transform.position.y;
        if (followX) 
        {
            newPosX = targetTransform.position.x + offset.x;
        }
        if (followY) 
        {
            newPosY = targetTransform.position.y + offset.y;
        }
        this.transform.position = new Vector3(newPosX, newPosY, this.transform.position.z);

    }

}
/*
 using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [Header("Transform to Follow")]
    [SerializeField] private Transform targetTransform;

    [Header("Configuration")]
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;
    [SerializeField] private Vector2 offset = Vector2.zero;

    private Transform originalTargetTransform;
    private float raycastDistance = 0.1f; // Adjust as needed

    private void Start()
    {
        originalTargetTransform = targetTransform;
    }

    private void LateUpdate()
    {
        if (targetTransform == null)
        {
            return;
        }

        Vector3 newPosition = transform.position;

        if (followX)
        {
            RaycastHit hit;
            if (Physics.Raycast(targetTransform.position, Vector3.right, out hit, raycastDistance))
            {
                newPosition.x = hit.point.x - offset.x;
            }
            else
            {
                newPosition.x = targetTransform.position.x + offset.x;
            }
        }

        if (followY)
        {
            RaycastHit hit;
            if (Physics.Raycast(targetTransform.position, Vector3.up, out hit, raycastDistance))
            {
                newPosition.y = hit.point.y - offset.y;
            }
            else
            {
                newPosition.y = targetTransform.position.y + offset.y;
            }
        }

        transform.position = newPosition;
    }
}
 */
