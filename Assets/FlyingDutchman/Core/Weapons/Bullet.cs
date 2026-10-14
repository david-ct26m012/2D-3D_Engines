using System;
using System.Collections;
using UnityEngine;


public class Bullet: MonoBehaviour
{

    public static event Action<string> OnHit;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 5f;
    
    
    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }


    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;

        OnHit?.Invoke($"Projektil hat '{other.gameObject.name}' getroffen!");

        Destroy(gameObject);
    }


    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);

        OnHit?.Invoke($"Projektil nach {lifetime} Sekunden zerstört.");

        Destroy(gameObject);
    }

}
