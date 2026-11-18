using UnityEngine;


public class ProjectileAmmo : MonoBehaviour
{
    void OnDestroy()
    {
        if (Magazine.Instance != null)
            Magazine.Instance.Restore();
    }
}
