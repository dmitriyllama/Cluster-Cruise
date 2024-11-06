using UnityEngine;

// Spawns objects randomly on a spherical surface
public class SphereSpawner : MonoBehaviour
{
    public float spawnRange;
    public float spawnNum;
    public GameObject[] spawns;

    void Start()
    {
        for (int i = 0; i < spawnNum; i++) {
            Vector3 dir = Random.onUnitSphere * spawnRange;
            foreach (var spawn in spawns) {
                Instantiate(
                    spawn,
                    transform.position + dir,
                    Quaternion.identity
                );
            }
        }
    }
}
