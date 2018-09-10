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
    [Tooltip("The sound of the fart.")]
    public AudioSource fartSound;

    readonly int k_jumpKey = Animator.StringToHash("Jump");
    readonly int k_fartKey = Animator.StringToHash("Fart");
    readonly int k_speedKey = Animator.StringToHash("Speed");

    private Rigidbody myRB;
    private GameObject myBody;
    private Animator myAnimator;
    // Location must be carefully tweaked to avoid putting worms directly under player.
    private ParticleSystem myFartCloud;
    // Hold last movement dir so model finishes rotating properly when not moving.
    private Vector3 lastLookDir;

    private bool m_hasKey = false;

    public float CurrentSize {  get { return currentSize; } }

    public bool HasKey {  get { return m_hasKey; } }

    void Start () {
    }

    void Awake()
    {
        myRB = GetComponent<Rigidbody>();
        myBody = gameObject.transform.Find("Body").gameObject;
        myAnimator = GetComponentInChildren<Animator>();
        myFartCloud = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update () {
        // basic movement
        float rt = Input.GetAxis("Horizontal");
        float up = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(rt, 0, up);
        move = Quaternion.Euler(0, -90, 0) * move; //hack so UP points away from camera
        move *= speed;

        Vector3 vel = myRB.velocity;
        vel.x = move.x;
        vel.z = move.z;
        myRB.velocity = vel;

        myAnimator.SetFloat(k_speedKey, vel.magnitude);

        // jump
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                myRB.AddForce(jumpDir, ForceMode.Impulse);
                myAnimator.SetTrigger(k_jumpKey);
            }
        }

        // fart
        if(Input.GetButtonDown("Fire1"))
        {
            DoFart();
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

    //private void OnCollisionEnter(Collision other)
    //{
    //    //Debug.Log("y-velocity: " + myRB.velocity.y);
    //    IsFood food = other.gameObject.GetComponent<IsFood>();
    //    // I guess 'down' is y>0
    //    if (food && myRB.velocity.y > 0.01)
    //    {
    //        currentCalories += food.calories;
    //        food.BeEaten();
    //        BeSize();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        IsFood food = other.gameObject.GetComponent<IsFood>();
        // I guess 'down' is y>0
        if (food &&  transform.position.y > 0.1f)//  myRB.velocity.y > 0.01)
        {
            currentCalories += food.calories;
            food.BeEaten();
            BeSize();
        }

        Key key = other.gameObject.GetComponent<Key>();
        if (key)
            m_hasKey = true;

        Door door = other.gameObject.GetComponent<Door>();
        if (door && m_hasKey)
            door.Open();

    }

    public void Feed()
    {
        currentCalories += 1;
        BeSize();
    }

    public void GiftKey()
    {
        m_hasKey = true;
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

    // perform a standard fart if possible
    public void DoFart()
    {
        if (currentCalories >= 1)
        {
            myRB.velocity = Vector3.zero;

            // start animation; it will call DoFart at appropriate time
            myAnimator.SetTrigger(k_fartKey);
            // start sound now under the assumption it covers the entire animation
            fartSound.Play();
        }
    }

    public void DoFartEffects()
    {
        if (currentCalories >= 1)
        {
            myFartCloud.Play();

            currentCalories--;
            BeSize();

            if (fartWorm)
            {
                GameObject worm = Instantiate(fartWorm, myFartCloud.transform.position,
                    Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0));

                //// random toss dir
                //float pitch = UnityEngine.Random.Range(180+30, 180+60);
                //float yaw = UnityEngine.Random.Range(-30, 30);
                //float power = UnityEngine.Random.Range(10, 20);

                //Vector3 launchDir = Quaternion.AngleAxis(yaw, myBody.transform.up)
                //    * Quaternion.AngleAxis(pitch, myBody.transform.right)
                //    * myBody.transform.forward;

                //worm.GetComponent<Rigidbody>().AddForce(launchDir * power, ForceMode.Impulse);
            }
        }
    }

}
