using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_startingPosition;
    [SerializeField]
    private GameObject m_player;
    [SerializeField]
    private float m_movementSpeed;
    private Vector3 m_velocity;

    private void Awake()
    {
        m_velocity = new Vector3();
        transform.position = m_startingPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(m_player);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += 
            m_velocity + (m_player.transform.position - transform.position).normalized * m_movementSpeed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x, transform.position.y, m_startingPosition.z);
    }
}
