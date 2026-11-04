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
    [SerializeField] private float rollSpeed = 2f;

    private float ignoreLookUntil;
    private const float LookInputIgnoreDuration = 0.1f;
    


// ------------------------------------

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        IgnoreLookInputBriefly();
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
        if (Time.unscaledTime < ignoreLookUntil)
        {
            return;
        }

        pendingLook += value.Get<Vector2>();
    }

    public void OnEscape(InputValue value)
    {
        pendingLook = Vector2.zero;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        pendingLook = Vector2.zero;
        if (hasFocus)
        {
            IgnoreLookInputBriefly();
        }
    }

    private void IgnoreLookInputBriefly()
    {
        ignoreLookUntil = Time.unscaledTime + LookInputIgnoreDuration;
    }



    private void FixedUpdate()
    {

        Rigidbody.AddRelativeForce(movementInput * speed, ForceMode.Acceleration);

        Vector2 look = pendingLook;
        pendingLook = Vector2.zero;

        Quaternion turn = Quaternion.Euler (-look.y * mouseSensitivity,look.x * mouseSensitivity,0f);
        Rigidbody.MoveRotation(Rigidbody.rotation * turn);


        Rigidbody.AddRelativeTorque(Vector3.forward * -rollInput * rollSpeed, ForceMode.Acceleration);
    }






/*
    private void FixedUpdate()
    {
       // transform.position += movementInput * speed * Time.deltaTime;
       rigidbody.AddRelativeForce(movementInput, ForceMode.Acceleration);
    }
    */
}
