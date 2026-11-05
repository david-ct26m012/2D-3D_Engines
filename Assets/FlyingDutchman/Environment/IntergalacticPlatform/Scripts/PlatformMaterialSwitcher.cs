using UnityEngine;

public class PlatformMaterialSwitcher : MonoBehaviour
{
    [Tooltip("Renderer der Plattform (SM_Veh_Barge_01). NICHT die Taenzer oder Lichter!")]
    public Renderer[] platformRenderers;

    [Tooltip("Material am tiefsten Punkt (blaue Lichter, Plattform faehrt danach nach oben)")]
    public Material lowestMaterial;   // PolygonScifiWorlds_01_A

    [Tooltip("Material am hoechsten Punkt (rote Lichter, Plattform faehrt danach nach unten)")]
    public Material highestMaterial;  // PolygonScifiWorlds_04_C

    [Tooltip("Welcher Material-Slot getauscht wird (meist 0)")]
    public int materialIndex = 0;

    void Awake()
    {
        if (platformRenderers == null || platformRenderers.Length == 0)
            Debug.LogWarning("PlatformMaterialSwitcher: Bitte die Renderer von SM_Veh_Barge_01 im Inspector zuweisen.");
    }

    // Animation Event am tiefsten Keyframe
    public void SetLowestMaterial()
    {
        ApplyMaterial(lowestMaterial);
        Debug.Log("Platform reached lowest point -> " + lowestMaterial.name);
    }

    // Animation Event am hoechsten Keyframe
    public void SetHighestMaterial()
    {
        ApplyMaterial(highestMaterial);
        Debug.Log("Platform reached highest point -> " + highestMaterial.name);
    }

    void ApplyMaterial(Material mat)
    {
        if (mat == null) return;

        foreach (Renderer r in platformRenderers)
        {
            Material[] mats = r.sharedMaterials;          // Kopie des Arrays holen
            if (materialIndex < 0 || materialIndex >= mats.Length) continue;
            mats[materialIndex] = mat;
            r.sharedMaterials = mats;                      // zurueckschreiben, sonst passiert nichts
        }
    }
}
