using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour
{
    public Transform target;
    public Transform yTrans;
    public Transform xTrans;
    public Transform zTrans;

    private float xRotation = 45;
    private float zoom;

    private void Start()
    {
        zoom = zTrans.localPosition.z;
    }

    private void Update()
    {
        CameraFollow();
        YRotate();
        Zoom();
    }

    private float desiredRot;
    public float rotSpeed = 250;
    public float damping = 10;

    private void YRotate()
    {
        if (Input.GetKey(KeyCode.A))
            desiredRot -= rotSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.D))
            desiredRot += rotSpeed * Time.deltaTime;

        var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, desiredRot, transform.eulerAngles.z);
        yTrans.transform.rotation = Quaternion.Lerp(yTrans.transform.rotation, desiredRotQ, Time.deltaTime * damping);
    }



    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward scroll
            {
                if (xRotation < 84)
                    xRotation += 5f;
                if (zoom > -1.2f)
                    zoom -= 0.2f;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0) // back scroll
            {
                if (xRotation > 15)
                    xRotation -= 5f;
                if (zoom < 1.8f)
                    zoom += 0.2f;
            }
            xTrans.localRotation = Quaternion.Euler(xRotation, 0, 0);
            zTrans.localPosition = new Vector3(0, 0, zoom);
        }
    }


    private void CameraFollow()
    {
        this.transform.position = target.position;
    }
}