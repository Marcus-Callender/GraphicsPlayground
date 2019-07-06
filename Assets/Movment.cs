using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    [SerializeField]
    private Rigidbody m_rigb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            m_rigb.AddForce(new Vector3(0.0f, 100.0f, 0.0f));
        }

        Vector3 movment = new Vector3(Input.GetAxisRaw("Horizontal"), m_rigb.velocity.y, Input.GetAxisRaw("Vertical"));
        movment *= 8.0f;

        m_rigb.AddForce(movment);*/

        Vector3 movment = new Vector3(Input.GetAxisRaw("Horizontal"), m_rigb.velocity.y, Input.GetAxisRaw("Vertical"));
        movment *= 8.0f;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            movment.y = 10.0f;
        }
        else if (!IsGrounded())
        {
            movment.y -= 9.81f * Time.deltaTime;
        }

        m_rigb.velocity = movment;

        Debug.Log("movment: " + movment);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 1.1f);
    }
}
