using UnityEngine;

public class PickupSpawner : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField]
    private Pickup[] pickupPrefabs;

    [SerializeField]
    private Vector3 areaSize = new Vector3(5f, 5f, 5f);    

    [SerializeField]
    private int maxPickups = 10;


    // We always want to spawn once during start
    private void Start()
    {
        // Spawn maxPickups
        for (int i = 0; i < maxPickups; i++)
        {
            TrySpawnRandomPickup();
        }
        
    }

    private void TrySpawnRandomPickup()
    {
        // We are getting random pickups to spawn
        Pickup prefab = pickupPrefabs[Random.Range(0, pickupPrefabs.Length)];

        if (prefab == null)
        {
            Debug.LogError("Can't spawn pickup. Check pickups to spawn added in editor");
            return;
        }

        Vector3 position = GetRandomPointInArea();

        Pickup newPickup = Instantiate(prefab, position, Quaternion.identity);
        
    }

    
    

    private Vector3 GetRandomPointInArea()
    {
        Vector3 localOffset = new Vector3(
            Random.Range(-0.5f, 0.5f) * areaSize.x,
            Random.Range(-0.5f, 0.5f) * areaSize.y,
            Random.Range(-0.5f, 0.5f) * areaSize.z);

        return localOffset + transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.azure;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, areaSize);
    }
}
