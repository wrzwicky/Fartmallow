using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 10;
    public Vector3 jumpDir = new Vector3(0, 10, 0);

    CharacterController myCC;
    Rigidbody myRB;

	// Use this for initialization
	void Start () {
        myCC = GetComponent<CharacterController>();
        myRB = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        // basic movement
        float rt = Input.GetAxis("Horizontal");
        float up = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(rt, 0, up);
        move = Quaternion.Euler(0, -45, 0) * move; //hack so UP points away from camera
        move *= speed;

        Vector3 vel = myRB.velocity;
        vel.x = move.x;
        vel.z = move.z;
        myRB.velocity = vel;

        // jump
        if (Input.GetButtonDown("Jump"))
        {
            if(myRB.velocity.y == 0)
                myRB.AddForce(jumpDir, ForceMode.Impulse);
        }
    }
}
