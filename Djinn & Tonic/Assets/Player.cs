using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_movementSpeed;
    [SerializeField]
    private float m_jumpSpeed;
    [SerializeField]
    private Vector3 m_velocity;
    [SerializeField]
    private bool m_smoothMovement = false;
    [SerializeField]
    private float m_smoothMovementSlowdown;

    private bool m_onGround;
    private Rigidbody m_rigidBody;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        Assert.IsNotNull(m_rigidBody);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            m_onGround = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_smoothMovement)
        {
            m_velocity.x = 0.0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_velocity += Vector3.right * m_movementSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_velocity -= Vector3.right * m_movementSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space) && m_onGround)
        {
            m_rigidBody.AddForce(Vector3.up * m_jumpSpeed, ForceMode.Impulse);
            m_onGround = false;
        }

        transform.position += m_velocity * Time.deltaTime;

        if(m_smoothMovement)
        {
            m_velocity *= m_smoothMovementSlowdown;
            if (Mathf.Abs(m_velocity.x) <= 0.2f)
            {
                m_velocity.x = 0.0f;
            }
        }
    }
}