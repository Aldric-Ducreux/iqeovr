using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    public GameObject playerCam;
    public float m_speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 movement = new Vector3(Input.GetAxis("MoveHorizontal"), 0.0f, Input.GetAxis("MoveVertical"));
        //playerCam.transform.position = playerCam.transform.position + movement * Time.deltaTime * m_speed;

        //float horizontalAxis = Input.GetAxis("MoveHorizontal");
        //float verticalAxis = Input.GetAxis("MoveVertical");


        //var forward = playerCam.transform.forward;
        //var right = playerCam.transform.right;

        ////this is the direction in the world space we want to move:
        //var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;

        ////now we can apply the movement:
        //playerCam.transform.Translate(desiredMoveDirection * m_speed * Time.deltaTime);

        float facing = Camera.main.transform.eulerAngles.y; // Getting the angle the camera is facing

        
        float horizontalMovement = - Input.GetAxisRaw("MoveHorizontal");
        float verticalMovement = Input.GetAxisRaw("MoveVertical");

        Vector3 myInputs = new Vector3(horizontalMovement, 0, verticalMovement);

        // we rotate them around Y, assuming your inputs are in X and Z in the myInputs vector
        
        Vector3 myTurnedInputs = Quaternion.Euler(0, facing, 0) * myInputs;
        //print("Final Vector : " + myTurnedInputs);
        playerCam.transform.Translate(myTurnedInputs * m_speed * Time.deltaTime);
    }
}


