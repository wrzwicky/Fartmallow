using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField]
    float m_sizeRequirement = 2f;

    [SerializeField]
    float m_speed = 1f;

    [SerializeField]
    Vector3 m_destinationPosition;

    float m_timer = 0f;
    bool m_pushed = false;

    Vector3 m_originalPosition;

    // Use this for initialization
    void Start()
    {
        m_originalPosition = this.transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_pushed)
        {
            m_timer = Mathf.Clamp01(m_timer + (Time.deltaTime * m_speed));
            this.transform.localPosition = Vector3.Lerp(m_originalPosition, m_destinationPosition, m_timer);
        }
    }

    public void Push()
    {
        m_pushed = true;
    }


    private void OnCollisionEnter(Collision other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if(player)
        {
            if (Mathf.Approximately(player.CurrentSize, m_sizeRequirement) || player.currentSize > m_sizeRequirement)
            {
                m_pushed = true;
            }
        }
    }

}
