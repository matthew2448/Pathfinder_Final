using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]

    public bool movement = true;
    public CharacterController characterController;
    public float speed = 0.0f;

    private Vector3 inputVector;
    private Rigidbody playerBody;
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //float horzMove = Input.GetAxis("Horizontal");
        //float vertMove = Input.GetAxis("Vertical");

        //Vector3 move = transform.forward * vertMove + transform.right * horzMove;
        //characterController.Move(speed * Time.deltaTime * move);

        inputVector = new Vector3(Input.GetAxisRaw("Horizontal") * 5, playerBody.velocity.y, Input.GetAxisRaw("Vertical") * 5);
        //transform.LookAt(transform.position + new Vector3(inputVector.x, 0, inputVector.z));
        playerBody.velocity = inputVector;
    }
}
