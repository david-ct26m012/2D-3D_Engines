using UnityEngine;
using System.Collections;

public class DancerShipDetection : MonoBehaviour
{
    [SerializeField] private Transform spaceship;
    [SerializeField] private float detectionRadius = 20f;
    private Animator[] dancers; //Defined as long as this Script is assigned to Dancers Object
    private bool waving;



    void Awake()
    {
        dancers = GetComponentsInChildren<Animator>();
    }



    //Co routine
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
            foreach (Animator dancer in dancers) dancer.SetBool("isWaving", near); //Update each dancer and set the isWaving Parameter to near; If the ship is close: near true => isWaving = true; If the ship is far: near false => isWaving = false;
            
            yield return wait;
        }
}


}
