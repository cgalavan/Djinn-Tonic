using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float m_movementSpeed;
    private Rigidbody m_rigidBody;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        Assert.IsNotNull(m_rigidBody);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody.velocity = new Vector3(m_movementSpeed, 0.0f, 0.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ChangeEnemyDirection"))
        {
            m_rigidBody.velocity = -m_rigidBody.velocity;    
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_rigidBody.velocity * m_movementSpeed * Time.deltaTime;
    }
}