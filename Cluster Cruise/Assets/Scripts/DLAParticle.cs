using System.Collections;
using UnityEngine;

// Diffusion-Limited Aggregation particle
public class DLAParticle : MonoBehaviour
{
    public Vector3 target;
    public float brownianSpeed;
    public float targetSpeed;
    public float collisionDistance;
    public bool isAggregated;

    private void Start()
    {
        if (isAggregated) return;
        StartCoroutine(MoveRandomly());
    }

    private IEnumerator MoveRandomly()
    {
        while (!isAggregated)
        {
            Vector3 brownianDir = Random.insideUnitSphere;
            Vector3 targetDir = (target - transform.position).normalized;
            
            transform.position += targetDir*targetSpeed + brownianDir*brownianSpeed;

            CheckAggregation();
            yield return null;
        }
    }

    private void CheckAggregation()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, collisionDistance);
        foreach (var other in colliders)
        {
            if (other.gameObject != gameObject && other.gameObject.GetComponent<DLAParticle>().isAggregated)
            {
                Aggregate(other.gameObject);
                break;
            }
        }
    }

    private void Aggregate(GameObject other)
    {
        isAggregated = true;
        other.GetComponent<DLAParticle>().isAggregated = true;

        StopCoroutine(MoveRandomly());
    }
}