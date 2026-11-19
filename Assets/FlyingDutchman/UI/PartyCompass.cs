using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyCompass : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform spaceship;      // SpaceShip
    [SerializeField] private Transform party;          // PlatformMover/Dancers
    [SerializeField] private RectTransform arrow;
    [SerializeField] private Image ring;
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private TMP_Text heightText;

    [Header("Look")]
    [SerializeField] private Color farColor = new Color(0.2f, 0.9f, 1f);   // cyan
    [SerializeField] private Color nearColor = new Color(1f, 0.2f, 0.8f);  // pink
    [SerializeField] private float maxDistance = 500f;   // ab hier ganz Cyan
    [SerializeField] private float partyRadius = 40f;    // ab hier pulsiert der Ring

    private void Update()
    {
        // 1. Wo ist die Party aus Sicht des Schiffs?
        Vector3 local = spaceship.InverseTransformPoint(party.position);
        float distance = local.magnitude;

        // 2. Pfeil drehen: Winkel in der Ebene vorne/rechts
        float angle = Mathf.Atan2(local.x, local.z) * Mathf.Rad2Deg;
        arrow.localRotation = Quaternion.Euler(0f, 0f, -angle);

        // 3. Texte
        distanceText.text = $"{distance:0} m";
        heightText.text = local.y > 10f ? "▲ oben" : local.y < -10f ? "▼ unten" : "";

        // 4. Farbe: je näher, desto pinker
        float t = Mathf.InverseLerp(maxDistance, partyRadius, distance);
        Color color = Color.Lerp(farColor, nearColor, t);
        arrow.GetComponent<Image>().color = color;
        distanceText.color = color;

        // 5. Ring pulsiert, wenn man da ist
        float pulse = distance < partyRadius ? 1f + Mathf.Sin(Time.time * 8f) * 0.08f : 1f;
        ring.rectTransform.localScale = Vector3.one * pulse;
        ring.color = color;
    }
}