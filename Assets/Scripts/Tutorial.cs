using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [SerializeField]
    CanvasGroup m_canvas;

    [SerializeField]
    bool m_showAtStart = false;

    [SerializeField]
    float m_time = 5f;

    [SerializeField]
    bool m_useDelay = false;

    [SerializeField]
    float m_delayTime = 5f;

    float m_delayTimer = 0f;
    float? m_timer  = null;
    bool m_shown = false;

	// Use this for initialization
	void Start ()
    {
        m_canvas.alpha = 0f;
        m_delayTimer = m_delayTime;

        if (m_showAtStart)
            Show();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(m_useDelay)
        {
            m_delayTimer -= Time.deltaTime;
            if (m_delayTimer < 0f)
            {
                m_useDelay = false;
                Show();
            }
        }

	    if(m_timer.HasValue)
        {
            m_timer -= Time.deltaTime;

            if (m_timer < 0f)
            {
                m_timer = null;
                m_canvas.alpha = 0f;
            }
        }
        	
	}


    public void Show()
    {
        m_timer = m_time;
        m_shown = true;
        m_canvas.alpha = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponentInParent<PlayerController>();

        if (player && !m_shown)
        {
            Show();
        }
    }
}
