using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private Image crosshairImage;

    [Header("Raycast Settings")]
    //public string selectableTag = "Selectable";
    public float maxRaycastDistance = 100f;
    public LayerMask raycastLayer;

    [Header("UI Elements")]
    public Color defaultColor = Color.white;
    public Color triggeredColor = Color.cyan;

    private bool isTargetInRange = false;

    private void Awake()
    {
        crosshairImage = GetComponent<Image>();
    }

    private void Update()
    {
        // Create a ray from the camera through the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // Create a raycast hit variable to store information about the hit
        RaycastHit hit;

        // Check if the ray hits an object on the specified layer within the max distance
        if (Physics.Raycast(ray, out hit, maxRaycastDistance, raycastLayer))
        {
            //// Check if the hit object has the "Selectable" tag
            //if (hit.collider.CompareTag(selectableTag))
            //{
            //    Debug.Log("WE GOT A HIT!!!");
            //    // The crosshair is over a selectable object within range
            //    isTargetInRange = true;
            //}
            //else
            //{
            //    // The crosshair is not over a selectable object within range
            //    isTargetInRange = false;
            //}

            isTargetInRange = true;
        }
        else
        {
            // The ray didn't hit any object within range
            isTargetInRange = false;
        }

        // Update the crosshair appearance based on the target in range status
        UpdateCrosshairUI();
    }

    private void UpdateCrosshairUI()
    {
        // You can modify the crosshair appearance based on whether a selectable target is in range.
        // For example, you can change its color or size.

        if (crosshairImage != null)
        {
            if (isTargetInRange)
            {
                // Set the crosshair to the active state (e.g., change color or size)
                crosshairImage.color = triggeredColor;
            }
            else
            {
                // Set the crosshair to the inactive state (e.g., default color or size)
                crosshairImage.color = defaultColor;
            }
        }
    }
}
