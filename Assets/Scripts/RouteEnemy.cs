using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteEnemy : MonoBehaviour
{
    public float speed;
    public GameObject[] points;
    private int index;
    private float radiusRange = 2f;
    public float startWaitTime;
    private float waitTime;
    private bool go;


    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        index = 0;
        go = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector3.Distance(transform.position, points[index].transform.position);

        if (go)
        {
            if(distance > radiusRange)
            {
                //waitTime -= Time.deltaTime;
                Move();
            }
            else //if(waitTime <= 0)
            {
                //waitTime = startWaitTime;
                index = Random.Range(0, points.Length);
               
            }
        }

        //transform.position = Vector3.MoveTowards(transform.position, points[index].position, speed * Time.deltaTime);
        /*
        if(Vector3.Distance(transform.position, points[index].position) == 0f)
        {
            if(waitTime <= 0)
            {
                index = Random.Range(0, points.Length);
                Debug.Log(index);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        */
    }
    void Move()
    {
        //gameObject.
        transform.LookAt(points[index].transform.position);
        transform.position = Vector3.MoveTowards(transform.position, points[index].transform.position, speed * Time.deltaTime);
    }
}
