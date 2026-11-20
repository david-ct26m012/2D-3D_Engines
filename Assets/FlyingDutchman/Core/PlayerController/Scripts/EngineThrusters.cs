using UnityEngine;

public class EngineThrusters : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    // Partikel
    [SerializeField] private ParticleSystem[] thrusters;   // Thruster Partikel assignen
    [SerializeField] private float idleSpeed = 3f;          // Partikel-Tempo im Leerlauf
    [SerializeField] private float maxSpeed = 30f;          // Vollgas: schnell = langer Schweif
    [SerializeField] private float maxRate = 150f;
    [SerializeField] private float response = 6f;          // wie schnell es hochfährt

    // Lichter
    [SerializeField] private Light[] engineLights;          // Thruster Lichter assignen
    [SerializeField] private float idleRate = 15f;          // leichtes Glühen im Leerlauf
    [SerializeField] private float idleLightIntensity = 0.5f;
    [SerializeField] private float maxLightIntensity = 4f;


    private float currentThrottle;

    

    private void Awake()
    {
        // Fallback: Script liegt auf SpaceShip, PlayerController auch
        if (player == null) player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        // weich nachziehen statt hart an/aus
        currentThrottle = Mathf.Lerp(currentThrottle, player.Throttle, response * Time.deltaTime);
        float rate = Mathf.Lerp(idleRate, maxRate, currentThrottle);
        float speed = Mathf.Lerp(idleSpeed, maxSpeed, currentThrottle);

        foreach (var thruster in thrusters)
        {
            var emission = thruster.emission;   // Module sind Structs: erst holen, dann setzen
            emission.rateOverTime = rate;

            var main = thruster.main;
            main.startSpeedMultiplier = speed;  // schneller nach hinten = längerer Schweif
        }

        foreach (var light in engineLights)
            light.intensity = Mathf.Lerp(idleLightIntensity, maxLightIntensity, currentThrottle);
    }
}