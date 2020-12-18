using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public PlayerController pc;
    //bool movement;
    public Rigidbody rb;
    public string newGameScene = "GameOver";
    static int level = 5;
    void OnCollisionEnter(Collision info)
    {
        //Debug.Log("Print");
        if (info.collider.tag == "WallEnemy" ||
            info.collider.tag == "MovingEnemy" ||
            info.collider.tag == "ChasingEnemy" ||
            info.collider.tag == "SeekerEnemy")
        {

            pc.movement = false;
            SceneManager.LoadScene(newGameScene);
        }

        if (info.collider.tag == "Wapper")
        {
            level++;
            if(level == 9)
                SceneManager.LoadScene("Credits");
            else
                SceneManager.LoadScene(level);
            Debug.Log(level);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Print");
    }


}