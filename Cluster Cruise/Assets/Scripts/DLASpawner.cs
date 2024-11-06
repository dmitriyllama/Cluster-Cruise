using System.Collections;
using UnityEngine;

// Spawner for Diffusion-Limited Aggregation particles (DLAParticle)
public class DLASpawner : MonoBehaviour
{
    public float spawnRange;
    public int particleTargetMin;
    public int particleTargetMax;
    public GameObject particlePrefab;

    private int _particleTarget;

    private void Start()
    {
        _particleTarget = Random.Range(particleTargetMin, particleTargetMax+1);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int particleNum = 0; particleNum < _particleTarget; particleNum++) {
            DLAParticle dlaParticle = Instantiate(
                particlePrefab,
                transform.position + Random.insideUnitSphere * spawnRange,
                Quaternion.identity,
                transform
            )
            .GetComponent<DLAParticle>();
            dlaParticle.target = transform.position;
        }

        yield return null;
    }
}
