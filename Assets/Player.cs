using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_movementSpeed;
    [SerializeField]
    private float m_ladderMovementSpeed;
    [SerializeField]
    private float m_jumpSpeed;

    private bool m_onGround;
    private bool m_onLadder;
    private Rigidbody m_rigidBody;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        Assert.IsNotNull(m_rigidBody);

        m_onGround = false;
        m_onLadder = false;
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

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ladder"))
        {
            m_rigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            m_onLadder = true;
        }
        else if(other.gameObject.CompareTag("LevelEnd"))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Ladder"))
        {
            m_onLadder = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = 0.0f;
        if(m_onLadder)
        {
            m_rigidBody.velocity = new Vector3(0.0f, 0.0f, m_rigidBody.velocity.z);
            m_rigidBody.useGravity = false;
            movementSpeed = m_ladderMovementSpeed;
        }
        else
        {
            m_rigidBody.velocity = new Vector3(0.0f, m_rigidBody.velocity.y, m_rigidBody.velocity.z);
            m_rigidBody.useGravity = true;
            movementSpeed = m_movementSpeed;
        }
       
        m_rigidBody.useGravity = !m_onLadder;

        if (Input.GetKey(KeyCode.D))
        {
            m_rigidBody.velocity += Vector3.right * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_rigidBody.velocity -= Vector3.right * movementSpeed;
        }
        if(m_onLadder && Input.GetKey(KeyCode.W))
        {
            m_rigidBody.velocity += Vector3.up * m_ladderMovementSpeed;
        }
        if(m_onLadder && Input.GetKey(KeyCode.S))
        {
            m_rigidBody.velocity += Vector3.down * m_ladderMovementSpeed;
        }
        if (m_onGround && Input.GetKeyDown(KeyCode.Space))
        {
            m_rigidBody.AddForce(Vector3.up * m_jumpSpeed, ForceMode.Impulse);
            m_onGround = false;
        }

        transform.position += m_rigidBody.velocity * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        transform.rotation = Quaternion.identity;
    }
}