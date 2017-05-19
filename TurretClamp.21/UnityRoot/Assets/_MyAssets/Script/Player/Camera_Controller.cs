﻿using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour
{
    public Transform target;
    public float rotationYOffSet;

    private void Update()
    {
        CameraFollow();
        CameraRotate(); 
    }

    private void CameraRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rotationYOffSet -= 1;
            this.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, transform.rotation.y + rotationYOffSet, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationYOffSet += 1;
            this.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, transform.rotation.y + rotationYOffSet, 0);
        }
    }
    private void CameraFollow()
    {
        this.transform.position = target.position;   
    }
}
