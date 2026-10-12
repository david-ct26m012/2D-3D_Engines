using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody Rigidbody;


    //Controls
    private Vector3 movementInput;
    [SerializeField] private float speed = 10f;

    private Vector2 pendingLook;
    [SerializeField] private float mouseSensitivity = 0.1f;

    private float rollInput;
    [SerializeField] private float rollSpeed = 0.5f;


    


// ------------------------------------

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector3>();
        Debug.Log(movementInput);
    }

    public void OnRoll(InputValue value)
    {
        rollInput = value.Get<float>();
    }

    public void OnLook(InputValue value)
    {
        pendingLook += value.Get<Vector2>();
    }

    public void OnEscape(InputValue value)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }



    private void FixedUpdate()
    {

        Rigidbody.AddRelativeForce(movementInput * speed, ForceMode.Acceleration);

        Vector2 look = pendingLook;
        pendingLook = Vector2.zero;

        Quaternion turn = Quaternion.Euler (-look.y * mouseSensitivity,look.x * mouseSensitivity,0f);
        Rigidbody.MoveRotation(Rigidbody.rotation * turn);


        Quaternion turnRoll = Quaternion.Euler(0f, 0f, -rollInput * rollSpeed);
        Rigidbody.MoveRotation(Rigidbody.rotation * turnRoll);
    }






/*
    private void FixedUpdate()
    {
       // transform.position += movementInput * speed * Time.deltaTime;
       rigidbody.AddRelativeForce(movementInput, ForceMode.Acceleration);
    }
    */
}
