using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f, throwForce = 20f, lerpSpeed = 10f;
    [SerializeField] Transform objectHolder;

    public float rotate_speed = 0.1f;

    Rigidbody grabbedRB;

    void Update()
    {
        if (grabbedRB)
        {
            grabbedRB.MovePosition(Vector3.Lerp(grabbedRB.position, objectHolder.transform.position, Time.deltaTime * lerpSpeed));

            float yAngle = grabbedRB.transform.eulerAngles.y;
            float horizontalMovement = -Input.GetAxisRaw("RotateHorizontal");
            float verticalMovement = Input.GetAxisRaw("RotateVertical");
            Vector3 myInputs = new Vector3(horizontalMovement, verticalMovement, 0);

            print("Rotation Angle : " + myInputs);
            grabbedRB.transform.rotation = Quaternion.Euler(grabbedRB.transform.eulerAngles.x - verticalMovement, grabbedRB.transform.eulerAngles.y + horizontalMovement, 0);
            //grabbedRB.transform.Rotate(Vector3.up * horizontalMovement);
            //grabbedRB.transform.Rotate(Vector3.right * verticalMovement);
            //grabbedRB.transform.Translate(myTurnedInputs * rotate_speed * Time.deltaTime);


            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("LaunchAndroid") || Input.GetButtonDown("Launch"))
            {
                grabbedRB.isKinematic = false;
                grabbedRB.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
                grabbedRB = null;
            }
        }

        if (Input.GetButtonDown("Fire") || Input.GetButtonDown("FireAndroid"))
        {
            if (grabbedRB)
            {
                grabbedRB.isKinematic = false;
                grabbedRB = null;
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if (Physics.Raycast(ray, out hit, maxGrabDistance))
                {
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    if (grabbedRB)
                    {
                        grabbedRB.isKinematic = true;
                    }
                }
            }
        }
    }
}