using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Zoom : MonoBehaviour {

    float Dis = -1f;
    float rotation = 0;
    float viewField;

    private Camera cam;


    private void Start()
    {
        cam = GetComponent<Camera>();
        viewField = cam.fieldOfView;
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Transform trans = this.transform.parent.transform;
            if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward scroll
            {
                if(rotation < 39)
                {
                    viewField += 0.05f;
                    rotation += 1;
                    this.transform.Translate(Vector3.forward * -0.1f);
                }                
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0) // back scroll
            {
                if (rotation > -25)
                {
                    viewField -= 0.05f;
                    rotation -= 1;
                    this.transform.Translate(Vector3.forward * 0.1f);
                }                    
            }
            trans.rotation = Quaternion.Euler(rotation, 0, 0);
            //cam.fieldOfView = viewField;
        }
    }
}
