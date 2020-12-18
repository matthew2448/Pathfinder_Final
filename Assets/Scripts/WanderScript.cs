using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderScript : MonoBehaviour
{
    public float speed = 3f;
    public float roateSpeed = 100f;

    private bool walking = false;
    private bool turnRight = false, turnLeft = false;
    private bool wander = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator wanderEnemy()
    {
        int rot = Random.Range(1, 3);
        int rotWait = Random.Range(1, 4);
        int rotLorR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        wander = true;

        yield return new WaitForSeconds(walkWait); walking = true;
        yield return new WaitForSeconds(walkTime); walking = false;
        yield return new WaitForSeconds(rotWait);
        if(rotLorR == 1)
        {
            turnRight = true;
            yield return new WaitForSeconds(rot); turnRight = false;
        }
        if (rotLorR == 2)
        {
            turnLeft = true;
            yield return new WaitForSeconds(rot); turnLeft = false;
        }
        wander = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!wander)
        {
            StartCoroutine(wanderEnemy());
        }
        if (turnRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * speed);
        }
        if (turnLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -speed);
        }
        if (walking == true)
        {
            transform.position += transform.forward * Time.deltaTime;
        }
    }
}
