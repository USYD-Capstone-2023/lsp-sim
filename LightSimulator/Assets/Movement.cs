using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //position of camera to object (Z position)
    private float CameraZDistance;

    //Main camera object
    private Camera mainCamera;

    private void OnMouseDown()
    {

        // print(this);
        // print(Input.mousePosition);

    }

    private void OnMouseDrag()
    {

        // print("MOUSE BEING DRAGGED");
        // print(Input.mousePosition);


        //finding the position of mouse from camera
        Vector3 ScreenPosition = new Vector3((int)(Input.mousePosition.x/5) *5, (int)(Input.mousePosition.y/5)*5, (CameraZDistance));
        // print(ScreenPosition);

        //setting object position to mouse position
        transform.position = mainCamera.ScreenToWorldPoint(ScreenPosition);

    }

    private void OnCollisionEnter(Collision collision){

        // print(collision.gameObject.name);

        if (collision.gameObject.name == "Bin"){

            //destorying self only if bin collsions
            Destroy(gameObject);

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
        //setting camera
        mainCamera = Camera.main;

        //fiindind z position from mouse to camera.
        CameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
