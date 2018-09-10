using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Tooltip("Player movement speed.")]
    public float speed = 10;
    [Tooltip("Player jump speed and direction.")]
    public Vector3 jumpDir = new Vector3(0, 10, 0);
    [Tooltip("Current amount of food in tummy.")]
    public float currentCalories = 0;
    [Tooltip("Current size 1..3.")]
    public float currentSize = 1;
    [Tooltip("Critter that appears when player farts.")]
    public GameObject fartWorm;

    readonly int k_jumpKey = Animator.StringToHash("Jump");
    readonly int k_fartKey = Animator.StringToHash("Fart");
    readonly int k_speedKey = Animator.StringToHash("Speed");

    private Rigidbody myRB;
    private CapsuleCollider myCollider;
    private GameObject myBody;
    private Animator m_animator;
    private Rigidbody m_rigidbody;

    private Vector3 lastLookDir;


    // Use this for initialization
    void Start () {
        myRB = GetComponent<Rigidbody>();
        myCollider = GetComponentInChildren<CapsuleCollider>();
        myBody = gameObject.transform.Find("Body").gameObject;
    }

    void Awake()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
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

        m_animator.SetFloat(k_speedKey, vel.magnitude);

        // jump
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                myRB.AddForce(jumpDir, ForceMode.Impulse);
                m_animator.SetTrigger(k_jumpKey);
            }
        }

        // fart
        if(Input.GetButtonDown("Fire1"))
        {
            if (currentCalories >= 1)
            {
                m_animator.SetTrigger(k_fartKey);

                currentCalories--;
                BeSize();

                if (fartWorm)
                {
                    GameObject worm = Instantiate(fartWorm, transform.position+new Vector3(0,1,0), 
                        Quaternion.Euler(0,UnityEngine.Random.Range(0,360),0));
                    // random toss dir
                    float pitch = UnityEngine.Random.Range(30, 80);
                    float yaw = UnityEngine.Random.Range(0, 360);
                    float power = UnityEngine.Random.Range(9, 20);
                    Quaternion q = Quaternion.Euler(0,yaw,pitch);
                    Vector3 v = q * Vector3.forward;
                    worm.GetComponent<Rigidbody>().AddForce(v*power, ForceMode.Impulse);
                }
            }

        }

        // rotate to face dir of motion
        Vector3 dir = myRB.velocity;
        dir.y = 0;
        if(dir.sqrMagnitude > 0)
        {
            lastLookDir = dir;
        }
        // continually rotate toward last movement vector
        myBody.transform.rotation =
            Quaternion.Slerp(myBody.transform.rotation, Quaternion.LookRotation(lastLookDir), 0.20f);
    }

    private void OnCollisionEnter(Collision other)
    {
        IsFood food = other.gameObject.GetComponent<IsFood>();
        if (food)// && !IsGrounded())
        {
            currentCalories += food.calories;
            food.BeEaten();
            BeSize();
        }
    }

    // 'true' if player is standing on ground
    public bool IsGrounded()
    {
        // cheap hack, but doing anything more means figuring out what objects are 'ground'
        return Mathf.Abs(myRB.velocity.y) < 0.01;
    }

    // update Marshall's size based on current calories
    public void BeSize()
    {
        if (currentCalories < 1)
            currentSize = 1;
        else if (currentCalories >= 1 && currentCalories < 2)
            currentSize = 2;
        else
            currentSize = 3;
        myBody.transform.localScale = new Vector3(currentSize, 1, currentSize);
    }
}
