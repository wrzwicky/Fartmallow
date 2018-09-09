using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationExample : MonoBehaviour
{
    readonly int k_jumpKey = Animator.StringToHash("Jump");
    readonly int k_fartKey = Animator.StringToHash("Fart");
    readonly int k_speedKey = Animator.StringToHash("Speed");

    [SerializeField]
    float m_speed = 1f;

    Animator m_animator;
    Rigidbody m_rigidbody;

    float m_forward = 0f;

	// Use this for initialization
	void Awake ()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            m_forward = Mathf.Clamp01(m_forward + (Time.deltaTime * m_speed));
        }
        else
        {
            m_forward = Mathf.Clamp01(m_forward - (Time.deltaTime * m_speed));
        }

        m_rigidbody.velocity = transform.forward * m_forward;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_animator.SetTrigger(k_jumpKey);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            m_animator.SetTrigger(k_fartKey);
        }

        m_animator.SetFloat(k_speedKey, m_forward);
    }
}
