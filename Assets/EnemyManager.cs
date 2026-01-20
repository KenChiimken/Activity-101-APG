using System.Security.Claims;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float speed = 2f;
    public float directionChangeDistance = 2f;
    public float screenPadding = 1f;

    private Camera mainCamera;
    private Vector3 moveDirection;
    private Vector3 lastDirectionChange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        PickNewDirection();
        lastDirectionChange = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ClampToCameraView();

        transform.position += moveDirection * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, lastDirectionChange) >= directionChangeDistance)
        {
            PickNewDirection();
            lastDirectionChange = transform.position;
        }
    }

    void PickNewDirection()
    {
        moveDirection = Random.onUnitSphere;
    }

    void ClampToCameraView()
    {
        Vector3 viewportPos = mainCamera.WorldToScreenPoint(transform.position);
        bool outOfView = viewportPos.x < 0f || viewportPos.x > 1f || viewportPos.y < 0f || viewportPos.y > 1f || viewportPos.z < mainCamera.nearClipPlane;

        if (outOfView)
        {
            //viewportPos.x = Mathf.Clamp(viewportPos.x)
        }
    }
}
