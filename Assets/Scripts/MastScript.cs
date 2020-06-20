using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastScript : MonoBehaviour
{
    public Rigidbody m_Flag;
    private bool m_TookFlag = false;
    private float m_FlagVelocity = 5.0f;

    private void Start()
    {
        m_Flag = GameObject.Find("Flag").GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (m_TookFlag)
        {
            MoveFlag();
        }
    }

    private void MoveFlag()
    {
        if (m_Flag.position.y > -1.9f)
        {
            Vector3 position = new Vector3(0.0f, m_FlagVelocity * Time.fixedDeltaTime, 0.0f);
            m_Flag.MovePosition(m_Flag.position - position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerTag")
        {
            m_TookFlag = true;
        }
    }
}
