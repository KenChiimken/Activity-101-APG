using UnityEngine;

[ExecuteAlways]
public class SpawnGrid : MonoBehaviour
{
    [Header("Grid Settings")]
    public float spacing = 2f;

    [Header("Spawn Point Settings")]
    public string spawnPointName = "SpawnPoint";
    public bool showGizmos = true;

    [Header("Random Y Settings")]
    public float minY = 1f;
    public float maxY = 5f;

    public void Generate()
    {
        Clear();

        Renderer r = GetComponent<Renderer>();
        if (!r)
        {
            Debug.LogError("Plane must have a Renderer.");
            return;
        }

        Vector3 size = r.bounds.size;
        Vector3 center = r.bounds.center;

        int xCount = Mathf.FloorToInt(size.x / spacing);
        int zCount = Mathf.FloorToInt(size.z / spacing);

        for (int x = 0; x < xCount; x++)
        {
            for (int z = 0; z < zCount; z++)
            {
                //Random Y between minY and maxY
                float randomY = Random.Range(minY, maxY);

                Vector3 pos = new Vector3(
                    center.x - size.x / 2 + x * spacing,
                    randomY,
                    center.z - size.z / 2 + z * spacing
                );

                GameObject sp = new GameObject(spawnPointName);
                sp.transform.position = pos;
                sp.transform.parent = transform;
                sp.tag = "SpawnPoint";
            }
        }
    }

    public void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    void OnDrawGizmos()
    {
        if (!showGizmos) return;

        Gizmos.color = Color.green;
        foreach (Transform child in transform)
        {
            Gizmos.DrawSphere(child.position, 0.15f);
        }
    }
}
