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
    private float rayLength;

    [SerializeField]
    private float speedAdjustment;

    [SerializeField]
    LayerMask wallLayerMask;

    private float animTimer;
    private float animEndTime;

    // Use this for initialization
    void Start ()
    {
        wormRigidBody = GetComponent<Rigidbody>();

        animEndTime = wormAnimation.keys[wormAnimation.length - 1].time;
    }

    private void FixedUpdate()
    {
        WallDetect();


    }

    // Update is called once per frame
    void Update ()
    {
        animTimer += Time.deltaTime;

        if (animTimer > animEndTime)
            animTimer = 0f;

        Move();
	}

    private void WallDetect()
    {
        if (Physics.Raycast(transform.position, -transform.right, rayLength, wallLayerMask)
            && Physics.Raycast(transform.position, transform.right, rayLength, wallLayerMask))
        {
            transform.Rotate(Vector3.up, Random.Range(120, -120));
        }
        else
        if (Physics.Raycast(transform.position, transform.forward, rayLength, wallLayerMask))
        {
            transform.Rotate(Vector3.up, Random.Range(120, -120));
        }
    }

    private void Move()
    {
        float speed = wormAnimation.Evaluate(animTimer);

        wormRigidBody.velocity = transform.forward * (speed * speedAdjustment);
    }
}
