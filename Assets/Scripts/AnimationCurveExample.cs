using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurveExample : MonoBehaviour {

    [SerializeField]
    AnimationCurve m_curve;

    float m_timer = 0f;
    float m_endTime = 0f;

    //Rigidbody m_rigidBody;


	// Use this for initialization
	void Start ()
    {
        //m_rigidBody = GetComponent<Rigidbody>();

        m_endTime = m_curve.keys[m_curve.length - 1].time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_endTime)
            m_timer = 0f;

        float speed = m_curve.Evaluate(m_timer);

        //m_rigidBody.velocity = transform.forward * speed;
	}
}
