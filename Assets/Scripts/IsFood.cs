using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFood : MonoBehaviour {
    public float calories = 1;

    public void BeEaten()
    {
        Destroy(gameObject);
    }
}
