using UnityEngine;

// "Drunkard Walker"
// Parforms a Drunkard's Walk multiple times
// Spawns objects on each step and after each walk
public class DWDrunkard : MonoBehaviour
{
    public int walksTargetMin;
    public int walksTargetMax;
    public int walkStepsMin;
    public int walkStepsMax;
    public float speed;
    public GameObject[] spawnsPerStep;
    public GameObject[] spawnsPerWalk;

    void Start()
    {
        int walksTarget = Random.Range(walksTargetMin, walksTargetMax);
        for (int walksNum = 0; walksNum < walksTarget; walksNum++) {
            int walkSteps = Random.Range(walkStepsMin, walkStepsMax);
            
            for (int step = 0; step < walkSteps; step++) {
                Vector3 brownianDir = Random.insideUnitSphere;
                transform.position += brownianDir*speed;
                
                if (step == walkSteps-1) break;
                foreach (var spawn in spawnsPerStep) {
                    Instantiate(
                        spawn,
                        transform.position,
                        Quaternion.identity
                    );
                }
            }

            foreach (var spawn in spawnsPerWalk) {
                Instantiate(
                    spawn,
                    transform.position,
                    Quaternion.identity
                );
            }
        }
    }
}
