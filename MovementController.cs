﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float lookSensitivity = 3f;

    [SerializeField]
    GameObject fpsCamera;

    

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float CameraUpAndDownRotation = 0f;
    private float CurrentCameraUpAndDownRotation = 0f; 


    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.visible = false;

        

    }



    // Update is called once per frame
    void Update()
    {
        


        //Calculate movement velocity as a 3D vector
        float _xMovement = Input.GetAxis("Horizontal");
        float _zMovement = Input.GetAxis("Vertical");

        Vector3 _movementHorizontal = transform.right * _xMovement;
        Vector3 _movementVertical = transform.forward * _zMovement;



        //Final Movement vector
        Vector3 _movementVelocity = (_movementHorizontal + _movementVertical).normalized * speed;


        //Apply movement
        Move(_movementVelocity);
        if (Cursor.visible == true)
        {
            //transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            return;
        }

        //Calculate rotation as a 3D vector for turning around
        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _rotationVector = new Vector3(0,_yRotation,0)*lookSensitivity;


        //Apply rotation
        Rotate(_rotationVector);

        //Calculate look up and down camera rotation
        float _cameraUpDownRotation = Input.GetAxis("Mouse Y")*lookSensitivity;


        //Apply camera rotation
        RotateCamera(_cameraUpDownRotation);



        
    }

    protected void LateUpdate()
    {
        //transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y,0 );
    }


    void Move(Vector3 movementVelocity)
    {
        velocity = movementVelocity;
    }

    void Rotate(Vector3 rotationVector)
    {
        rotation = rotationVector;
    }


    void RotateCamera(float cameraUpAndDownRotation)
    {
        CameraUpAndDownRotation = cameraUpAndDownRotation;
    }


    //Run every physics iteration
    private void FixedUpdate()
    {
        if (velocity!=Vector3.zero)
        {
            rb.MovePosition(rb.position+velocity*Time.fixedDeltaTime);
        }
        rb.MoveRotation(rb.rotation*Quaternion.Euler(rotation));
        if (fpsCamera!=null)
        {
            
            CurrentCameraUpAndDownRotation -= CameraUpAndDownRotation;
            CurrentCameraUpAndDownRotation = Mathf.Clamp(CurrentCameraUpAndDownRotation, -85, 85);


            fpsCamera.transform.localEulerAngles = new Vector3(CurrentCameraUpAndDownRotation,0,0);
            //transform.localEulerAngles = new Vector3(CurrentCameraUpAndDownRotation,transform.eulerAngles.y,transform.eulerAngles.z);
        }
    }





}
