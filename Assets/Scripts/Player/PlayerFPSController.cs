using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFPSController : MonoBehaviour
{
    public Action OnSpacePress;
    public Action OnQPress;

    public Camera PlayerCamera;
    public Rigidbody PlayerRigidbody;
    public float MovementSpeed = 10;
    public float MouseSpeed = 10;

    private void Awake()
    {
        PlayerCamera = PlayerCamera ? PlayerCamera : GetComponentInChildren<Camera>();
        PlayerRigidbody = PlayerRigidbody ? PlayerRigidbody : GetComponentInChildren<Rigidbody>();
        PlayerRigidbody.constraints = ~(RigidbodyConstraints.FreezeAll & RigidbodyConstraints.FreezePositionY);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        //Movement. Use RawInput to prevent movementsliding
        float verticleInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 movementDelta = MovementSpeed * Time.deltaTime * Vector3.Normalize(Vector3.forward * verticleInput + Vector3.right * horizontalInput);

        transform.Translate(movementDelta);
       
        //Rotation
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");
        //Rotate body horizontally
        transform.Rotate(0, MouseX * MouseSpeed, 0);

        //Rotate camera vertically around axis x, clamp camera angle from -80 to 80

        float newAngleX = PlayerCamera.transform.eulerAngles.x - Mathf.Clamp(MouseY * MouseSpeed, -160, 160);

        while (newAngleX > 180)
        {
            newAngleX -= 360;
        }

        PlayerCamera.transform.rotation = Quaternion.Euler(Mathf.Clamp(newAngleX, -80, 80), PlayerCamera.transform.eulerAngles.y, PlayerCamera.transform.eulerAngles.z);

        if (Input.GetKeyUp("space"))
        {
            OnSpacePress?.Invoke();
        }

        if (Input.GetKeyUp("q"))
        {
            OnQPress?.Invoke();
        }
    }
}
