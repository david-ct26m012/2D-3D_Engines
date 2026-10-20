//using System.Diagnostics;
//using System.Numerics;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SphereController : MonoBehaviour
{
    [Header("Speed")] 
    [SerializeField]
    private float speed = 0.1f;

    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private GameObject enemyPrefab;

    public UnityEvent MyEvent; //Event definieren um es später zu triggern



    private void Awake()
    {
        Debug.Log("Awake");
        MyEvent?.Invoke();
    }


    private void Start()
    {
        Debug.Log("Start");
        MyEvent?.Invoke();
        //GameObject Enemy = Instantiate(enemyPrefab);
        //MyEvent.RemoveListener;

    }

    private void OnEnable()
    {
        Debug.Log("Enable");
    }

    private void OnDisable()
    {
        Debug.Log("Disable");
    }



    // per frame
    private void Update()
    {
        Debug.Log("Update");

        transform.position += new Vector3(0, 0, speed * Time.deltaTime); // Frame unabhängige Bewegung
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);

    }
/*
    // per frame z.B. wenn eine Camera sich an eine neue Position anpassen soll
    private void LateUpdate()
    {
        Debug.Log("LateUpdate");
    }

        // 0.02s Kann in Physik Einstellungen angepasst werden
    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
    }
*/
    private void OnDestroy()
    {
        Debug.Log("OnDestroy");

        StartCoroutine(MyRoutine(10)); //Timer triggern
    }   


    //timer
    private IEnumerator MyRoutine(float time)
    {
        Debug.Log("");

        float timer = time;

        while (timer > 0) //Timer
        {
            timer -= Time.deltaTime;
            yield return null; //warte bis zum nächsten frame
        }

        
    }



}
