using UnityEngine;

public class MouseRaycastHit : MonoBehaviour
{
    public LayerMask hitLayers;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayers))
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                Debug.Log("Hit point: " + hit.point);
            }
            else
            {
                Debug.Log("Nothing hit");
            }
        }
    }
}
