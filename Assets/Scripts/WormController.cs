using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve wormAnimation;
    [SerializeField]
    private Rigidbody wormRigidBody;
    [SerializeField]
    private float rayLength, animTimer, animEndTime, speedAdjustment;
    [SerializeField]
    LayerMask wallLayerMask;

	// Use this for initialization
	void Start ()
    {
        wormRigidBody = GetComponent<Rigidbody>();

        animEndTime = wormAnimation.keys[wormAnimation.length - 1].time;
    }

    private void FixedUpdate()
    {
        WallDetect();

        animTimer += Time.deltaTime;

        if (animTimer > animEndTime)
        {
            animTimer = 0f;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        Move();
	}

    private void WallDetect()
    {
        RaycastHit forwardHit;
        RaycastHit leftHit;
        RaycastHit rightHit;

        if (Physics.Raycast(transform.position, -transform.right, out leftHit, wallLayerMask) && Physics.Raycast(transform.position, transform.right, out rightHit, wallLayerMask))
        {
            transform.Rotate(Vector3.up , Random.Range(120, -120));
        }

        else if (Physics.Raycast(transform.position, transform.forward, out forwardHit, wallLayerMask));
        {
            transform.Rotate(Vector3.up, Random.Range (120, -120));
        }
    }

    private void Move()
    {
        float speed = wormAnimation.Evaluate(animTimer) * speedAdjustment;

        wormRigidBody.velocity = transform.forward * speed;
    }
}
