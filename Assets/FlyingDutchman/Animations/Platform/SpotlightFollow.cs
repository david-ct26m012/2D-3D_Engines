using UnityEngine;

public class SpotlightFollow: MonoBehaviour
{
    public Transform dancer;
    
    //Zielt 1 m über die füße
    public float height = 1f;


    //Late Update, weil es nach den Tanzanimtionen laufen soll
    void LateUpdate()
    {
        transform.LookAt(dancer.position + Vector3.up * height);
    }
}
