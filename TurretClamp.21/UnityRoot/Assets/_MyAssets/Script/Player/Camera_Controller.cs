using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour
{
    public Transform target;
    public Transform yTrans;
    public Transform xTrans;
    public Transform zTrans;


    private float yRotation;
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

    private void YRotate()
    {
        if (Input.GetKey(KeyCode.A))
            yRotation -= 1;
        else if (Input.GetKey(KeyCode.D))
            yRotation += 1;
        yTrans.rotation = Quaternion.Euler(0, transform.rotation.y + yRotation, 0);
    }
    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward scroll
            {
                if (xRotation < 84) 
                    xRotation += 5f;
                if(zoom > -3)
                zoom -= 0.25f;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0) // back scroll
            {
                if (xRotation > 5)
                    xRotation -= 5f;
                if (zoom < 1)
                    zoom += 0.25f;
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
