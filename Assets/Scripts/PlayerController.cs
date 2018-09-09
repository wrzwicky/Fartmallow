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
        //myCC.SimpleMove(move * speed);
        myRB.AddForce(move * speed);

        // jump
        if (Input.GetButtonDown("Jump"))
        {
            if(myRB.velocity.y == 0)
                myRB.AddForce(jumpDir, ForceMode.Impulse);
        }
    }
}
