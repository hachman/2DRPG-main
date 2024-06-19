using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollowwithborder : MonoBehaviour
{
    public Transform target;             // The target that the camera will follow (e.g., the player)
    public float smoothTime = 0.3f;      // The time it takes to smooth the camera movement
    public Tilemap tilemap;              // The tilemap to determine the bounds (if using Tilemap)
    public Transform map;                // The map object to determine the bounds (if not using Tilemap)

    private Vector3 velocity = Vector3.zero; // The current velocity of the camera (used by SmoothDamp)
    private Vector2 minPosition;         // The minimum x and y positions the camera can move to
    private Vector2 maxPosition;         // The maximum x and y positions the camera can move to

    void Start()
    {
        // Calculate the bounds based on the map size
        CalculateCameraBounds();
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // Define the target position based on the target's position
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Smoothly move the camera towards the target position
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Clamp the camera position to ensure it stays within the specified bounds
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minPosition.x, maxPosition.x);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minPosition.y, maxPosition.y);

        // Update the camera's position
        transform.position = smoothedPosition;
    }

    void CalculateCameraBounds()
    {
        if (tilemap != null)
        {
            // If using Tilemap, calculate bounds from the Tilemap
            Bounds bounds = tilemap.localBounds;
            SetCameraBounds(bounds);
        }
        else if (map != null)
        {
            // If using a regular map, calculate bounds from the Renderer
            Renderer mapRenderer = map.GetComponent<Renderer>();
            if (mapRenderer != null)
            {
                Bounds bounds = mapRenderer.bounds;
                SetCameraBounds(bounds);
            }
        }
    }

    void SetCameraBounds(Bounds bounds)
    {
        float camHeight = Camera.main.orthographicSize * 2;
        float camWidth = camHeight * Camera.main.aspect;

        minPosition = new Vector2(bounds.min.x + camWidth / 2, bounds.min.y + camHeight / 2);
        maxPosition = new Vector2(bounds.max.x - camWidth / 2, bounds.max.y - camHeight / 2);
    }
}

