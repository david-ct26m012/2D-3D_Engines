using UnityEngine;

public class Movement : MonoBehaviour

{
    public float speed = 5;
    private SphereController controller;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<SphereController>(/*PrintMessage*/);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Printmessage()
    {
        Debug.Log("Message");
    }

}



