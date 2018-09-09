using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Tooltip("Player movement speed.")]
    public float speed = 10;
    [Tooltip("Player jump speed and direction.")]
    public Vector3 jumpDir = new Vector3(0, 10, 0);

    CharacterController myCC;
    Rigidbody myRB;
    private Vector3 lastLookDir;

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

        // rotate to face dir of motion
        Vector3 dir = myRB.velocity;
        dir.y = 0;
        if(dir.sqrMagnitude > 0)
        {
            lastLookDir = dir;
        }
        gameObject.transform.Find("Body").rotation =
            Quaternion.Slerp(gameObject.transform.Find("Body").rotation, Quaternion.LookRotation(lastLookDir), 0.20f);
    }
}
