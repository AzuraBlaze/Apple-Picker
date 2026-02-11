using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public ScoreCounter scoreCounter;

    void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    void Update()
    {
        Vector3 mousePosition2D = Input.mousePosition;
        mousePosition2D.z = -Camera.main.transform.position.z;

        Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint(mousePosition2D);

        Vector3 position = this.transform.position;
        position.x = mousePosition3D.x;
        this.transform.position = position;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collidedWith = collision.gameObject;
        
        if (collidedWith.CompareTag("Apple"))
        {
            Destroy(collidedWith);
            scoreCounter.score += 100;
            HighScore.TRY_SET_HIGH_SCORE(scoreCounter.score);
        }
        else if (collidedWith.CompareTag("Branch"))
        {
            // Branch caught = Game Over
            Destroy(collidedWith);
            
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            apScript.BranchCaught();
        }
    }
}