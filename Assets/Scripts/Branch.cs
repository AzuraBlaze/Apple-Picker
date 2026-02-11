using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public static float bottomY = -20f;

    void Update()
    {
        if (transform.position.y < bottomY)
        {
            // Branch fell through, just destroy it, no penalty
            Destroy(this.gameObject);
        }
    }
}