using System.Collections;
using UnityEngine;

// Environment transformer. Sets some heavyweight materials after a delay
// Allows world generation without heavy rendering lag
// You can remove this component and replace materials at start instead
public class Environment : MonoBehaviour
{
    public float delay;

    public float lightRange;
    public float lightIntensityMin;
    public float lightIntensityMax;
    public Material clusterMaterial;
    public Material starsSkybox;

    void Start()
    {
        StartCoroutine(MakeItShine());
    }

    IEnumerator MakeItShine() {
        // Wait for most particles to settle
        yield return new WaitForSeconds(delay);

        foreach (var light in FindObjectsOfType<Light>()) {
            light.range = lightRange;
            light.color = Random.ColorHSV(0f, 1f, 0.0f, 0.1f, lightIntensityMin, lightIntensityMax);
        }

        foreach (var particle in FindObjectsOfType<DLAParticle>()) {
            particle.GetComponent<Renderer>().material = clusterMaterial;
            particle.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 0.1f, 0.0f, 0.3f, 0.4f, 0.5f, 0.33f, 0.33f);
        }

        RenderSettings.skybox = starsSkybox;
    }
}
