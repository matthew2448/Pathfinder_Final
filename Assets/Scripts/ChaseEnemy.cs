
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{
    public float speed;
    public GameObject[] points;
    private int index;
    private float radiusRange = 2f;
    public float startWaitTime;
    private float waitTime;
    private bool go;

    private GameObject p;
    Transform player;
    private float detectionRange = 10f;
    private float speedUp = 1.5f;

    Vector3 startPos;
    Vector3 destinationPos;
    bool goToPlayer = false;

    private float radius = 2.5f;
    float hit_sight_range;
    Vector3 sight_box;
    Collider own_collider;
    bool hitDetect;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        index = 0;
        go = true;

        p = GameObject.FindGameObjectWithTag("Player");
        player = p.transform;

        startPos = transform.position;

        hit_sight_range = 2.0f;
        sight_box = new Vector3(0.45f, 0.45f, 0.45f);
        own_collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (IsPlayerNear())
        {
            setTargetPos(player.position);

            if (!goToPlayer)
            {
                goToPlayer = true;
                speed = speed * 1.5f;
            }
        }
        else
        {
            if (goToPlayer)
            {
                goToPlayer = false;
                speed = speed / 1.5f;
            }

            patrol();
        }
        moveToPos();
        
        /*
        float distance = Vector3.Distance(transform.position, points[index].transform.position);
        Debug.Log(distance);
        if (go)
        {
            if (distance > radiusRange)
            {
                //waitTime -= Time.deltaTime;
                Move();
            }
            else //if(waitTime <= 0)
            {
                //waitTime = startWaitTime;
                index = Random.Range(0, points.Length);
                Debug.Log(index);
            }
        }
        */
    }

    private void patrol()
    {
        Vector3 currentPatrolPos = points[index].transform.position;

        if(destinationPos != currentPatrolPos)
        {
            setTargetPos(currentPatrolPos);
        }

        if(Vector3.Distance(transform.position, currentPatrolPos) < 2f)
        {
            index = Random.Range(0, points.Length);
            setTargetPos(points[index].transform.position);
        }
    }

    void Move()
    {
        //gameObject.

        transform.LookAt(points[index].transform.position);
        transform.position = Vector3.MoveTowards(transform.position, points[index].transform.position, speed * Time.deltaTime);
        

    }
    bool IsPlayerNear()
    {
        if(Vector3.Distance(transform.position, player.position) < 3f)
        {
            return true;
        }

        return false;
    }
    void setTargetPos(Vector3 setTarget)
    {
        destinationPos = setTarget;
    }
    public LayerMask mask;
    void moveToPos()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, destinationPos, .2f, mask);
        if (!hit)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPos, speed * Time.deltaTime);
        }
        
        
    }
    bool objectHit = false;
    public bool detectObjects()
    {
        hitDetect = Physics.BoxCast(
            own_collider.bounds.center, 
            sight_box, 
            transform.forward, 
            out hit, 
            transform.rotation, 
            hit_sight_range);

        if (hitDetect)
        {
            if(hit.transform.tag == "Wall")
            {
                objectHit = true;
                return true;

            }
        }
        else
        {
            objectHit = true;
        }

        return false;
    }
}
