using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnPoint;

    public void OnShoot(InputValue value)
    {

        if (value.isPressed)
        {
            Shoot();
        }
    }



    void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("Kein Bullet Prefab im Cannon Skript zugewiesen!");
            return;
        }

        Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position;
        Quaternion spawnRot = spawnPoint != null ? spawnPoint.rotation : transform.rotation;


        Instantiate(bulletPrefab, spawnPos, spawnRot);
    }



}

