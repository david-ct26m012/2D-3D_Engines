using UnityEngine;
using System.Collections;

public class DancerShipDetection : MonoBehaviour
{
    [SerializeField] private Transform spaceship;
    [SerializeField] private float detectionRadius = 20f;
    [SerializeField] private float dancerTurnSpeed = 5f;
    private Animator[] dancers; //Defined as long as this Script is assigned to Dancers Object
    private Quaternion[] startRotations;
    private bool waving;



    void Awake()
    {
        dancers = GetComponentsInChildren<Animator>(); // Get all dancer attached to the dancers compontent

        startRotations = new Quaternion[dancers.Length];
        for (int i = 0; i < dancers.Length; i++)
            startRotations[i] = dancers[i].transform.localRotation;
    }


    //Co routine for waving
    private IEnumerator Start() 
    {
        var wait = new WaitForSeconds(0.2f);
        
        while (true)
        {
            bool near = Vector3.Distance(spaceship.position, transform.position) < detectionRadius; // Set near to true if the Spaceship distance is smaller than the set radius
            
            if (near == waving)
            {
                yield return wait; // Nothing changed; check again after the interval.
                continue;
            }

            waving = near; //Save new state
            
            foreach (Animator dancer in dancers) 
                dancer.SetBool("isWaving", near); //Update each dancer and set the isWaving Parameter to near; If the ship is close: near true => isWaving = true; If the ship is far: near false => isWaving = false;
            
            yield return wait;
        } 
    }

    void Update()
{
    
    
    //LookAt SpaceShip Logic:
    for (int i = 0; i < dancers.Length; i++)
    {
        Transform dancer = dancers[i].transform;
        Quaternion target;

            if (waving)
            {
                Vector3 toShip = spaceship.position - dancer.position;
                toShip.y = 0f;  //only turn horizontally
                
                if (toShip.sqrMagnitude < 0.01f) continue; // ship directly above: skip
                target = Quaternion.LookRotation(toShip);
            }

            else
            {
                target = dancer.parent.rotation * startRotations[i];  // original rotation
            }
        
            dancer.rotation = Quaternion.Slerp(dancer.rotation, target, dancerTurnSpeed * Time.deltaTime); //Spherical linear interpolation untilö the dancer looks at target
    }
}


}
