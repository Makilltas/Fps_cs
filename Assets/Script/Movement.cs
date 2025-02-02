using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform feet;
    public LayerMask groundMask;
    
    public float speed = 15f;
    public float gravity = -9.8f;

    private CharacterController controller;
    private bool isGrounded;
    private float y;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        var input = new Vector3();
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");

        var move = (transform.right * input.x + transform.forward * input.z) * speed * Time.deltaTime;

        y += gravity * Time.deltaTime * Time.deltaTime;
        move.y = y;

        controller.Move(move);
    }

    private void OnDrawGizmos()
    {
        if(feet != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(feet.position, 0.4f);
        }
    }
}
