using UnityEngine;

public class MouseRaycastHit : MonoBehaviour
{
    // A LayerMask can be used to selectively filter which colliders are considered
    public LayerMask hitLayers;

    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera through the current mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; // Variable to store information about the hit

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayers))
            {
                // If a collider is hit, this block of code executes
                Debug.Log("Hit object: " + hit.collider.gameObject.name);

                // You can access other information about the hit, such as:
                // The exact point in world space where the ray hit the collider
                Debug.Log("Hit point: " + hit.point);

                // The collider that was hit
                // hit.collider 

                // The transform of the hit object
                // hit.transform

                // You can also call functions on the hit object's scripts:
                // hit.collider.GetComponent<SomeScriptName>()?.SomeFunction();
            }
            else
            {
                // If nothing is hit by the raycast
                Debug.Log("Nothing hit");
            }
        }
    }
}
