using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    [SerializeField]
    private Rigidbody m_rigb;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 movment = Input.GetAxisRaw("Horizontal") * transform.right 
            + Input.GetAxisRaw("Vertical") * transform.forward;

        movment.Normalize();
        movment *= 8.0f;

        movment.y = m_rigb.velocity.y;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            movment.y = 10.0f;
        }
        else if (!IsGrounded())
        {
            movment.y -= 19.62f * Time.deltaTime;
        }

        transform.Rotate(transform.up, Time.deltaTime * 72.0f * Input.GetAxisRaw("Mouse X"));

        m_rigb.velocity = movment;
    }

    bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector3.up * 1.1f, Color.red);
        return Physics.Raycast(transform.position, -Vector3.up, 1.1f);
    }
}
