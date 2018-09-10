using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFood : MonoBehaviour {
    public float calories = 1;

    WormController m_worm;

    private void Awake()
    {
        m_worm = GetComponentInParent<WormController>();
    }


    public void BeEaten()
    {
        Destroy(m_worm.gameObject);
    }
}
