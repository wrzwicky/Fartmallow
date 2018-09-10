using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform m_target;

    Camera m_camera;
    Vector3 m_offset;

	// Use this for initialization
	void Awake ()
    {
        m_camera = GetComponent<Camera>();
        m_offset = this.transform.position - m_target.position ;

    }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = m_target.position + m_offset;
        	
	}
}
