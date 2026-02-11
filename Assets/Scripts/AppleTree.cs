using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject applePrefab;
    public GameObject branchPrefab;
    public float speed = 1f;
    public float leftAndRightEdge = 10f;
    public float changeDirectionChance = 0.1f;
    public float appleDropDelay = 1f;
    public float branchDropChance = 0.1f;

    void Start()
    {
        Invoke("DropApple", 2f);
    }

    void DropApple()
    {
        if (branchPrefab != null && Random.value < branchDropChance)
        {
            GameObject branch = Instantiate<GameObject>(branchPrefab);
            branch.transform.position = transform.position;
        }
        else
        {
            GameObject apple = Instantiate<GameObject>(applePrefab);
            apple.transform.position = transform.position;
        }
        
        Invoke("DropApple", appleDropDelay);
    }

    void Update()
    {    
        Vector3 position = transform.position;
        position.x += speed * Time.deltaTime;
        transform.position = position;

        if (position.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if (position.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed);
        }
        else if (Random.value < changeDirectionChance)
        {
            speed *= -1f;
        }
    }

    void FixedUpdate()
    {
        if (Random.value < changeDirectionChance)
        {
            speed *= -1f;
        }
    }
}