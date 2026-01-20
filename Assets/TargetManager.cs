using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float directionChangeDistance = 3f;

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;

    [Header("Z Limits")]
    public float minZ = 1f;
    public float maxZ = 5f;

    [Header("Score Manager")]
    public ScoreManager scoreManager; // Assign your ScoreManager in Inspector

    private Vector3 moveDirection;
    private Vector3 lastDirectionChangePosition;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        Respawn();
    }

    void Update()
    {
        //Move target
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        //Clamp Z within limits
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            Mathf.Clamp(transform.position.z, minZ, maxZ)
        );

        //Keep target inside camera view
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
        bool bounced = false;

        if (viewportPos.x < 0f || viewportPos.x > 1f)
        {
            moveDirection.x = -moveDirection.x;
            bounced = true;
        }

        if (viewportPos.y < 0f || viewportPos.y > 1f)
        {
            moveDirection.y = -moveDirection.y;
            bounced = true;
        }

        if (viewportPos.z < 0f)
        {
            moveDirection.z = -moveDirection.z;
            bounced = true;
        }

        if (bounced)
        {
            lastDirectionChangePosition = transform.position;
        }

        // Pick new random direction after finishing movement
        if (Vector3.Distance(transform.position, lastDirectionChangePosition) >= directionChangeDistance)
        {
            PickNewDirection();
            lastDirectionChangePosition = transform.position;
        }
    }

    void PickNewDirection()
    {
        moveDirection = Random.onUnitSphere;

        //Prevent immediate Z out-of-bounds
        if ((transform.position.z <= minZ && moveDirection.z < 0) ||
            (transform.position.z >= maxZ && moveDirection.z > 0))
        {
            moveDirection.z = -moveDirection.z;
        }
    }

    void OnMouseDown()
    {
        if (scoreManager != null)
        {
            scoreManager.AddScoreFromTargetZ(transform.position.z);
        }

        Respawn();
    }

    void Respawn()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        // Pick random spawn point
        Transform chosenSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        //Clamp Z on spawn
        Vector3 spawnPos = chosenSpawn.position;
        spawnPos.z = Mathf.Clamp(spawnPos.z, minZ, maxZ);
        transform.position = spawnPos;

        PickNewDirection();
        lastDirectionChangePosition = transform.position;
    }
}
