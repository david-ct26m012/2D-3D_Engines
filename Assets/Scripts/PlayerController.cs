using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    private Movement movementinput;
    
    [SerializeField]
    private float speed;
    private Rigidbody rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputValue value)
    {
    Vector3 input = value.Get<Vector3>();
    Debug.Log(input);
    }
/*
    private void FixedUpdate()
    {
       // transform.position += movementInput * speed * Time.deltaTime;
       rigidbody.AddRelativeForce(movementInput, ForceMode.Acceleration);
    }
    */
}
