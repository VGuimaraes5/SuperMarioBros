using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    public float m_SpeedMove;
    public float m_SpeedRun;
    public float m_SpeedRotation = 15.0f;
    private bool m_IsRunning;
    private Vector3 m_Movement;

    [Header("Ground")]
    public Vector3 m_GroundDistance = new Vector3(0.47f, 0.01f, 0.47f);
    public LayerMask m_GroundLayer;
    public Transform m_Feet;
    private bool m_IsGrounded;

    [Header("Jump")]
    public float m_JumpForce;
    private bool m_IsJumping;
    public float m_JumpTime = 0.33f;
    private float m_JumpElapsedTime;
    public AudioClip m_SmallJump;
    public AudioClip m_SuperJump;
    private bool m_playingJump;
    //private AudioSource audioSource;
    
    private Rigidbody m_Body;

    void Start()
    {
        m_Body = GetComponent<Rigidbody>();
        //audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        m_IsGrounded = Physics.CheckBox(m_Feet.position, m_GroundDistance, m_Feet.rotation, m_GroundLayer, QueryTriggerInteraction.Ignore);
        m_Movement.x = Input.GetAxis("Horizontal");
        m_IsRunning = Input.GetButton("Fire1");

        if(Input.GetButtonDown("Jump") && m_IsGrounded)
        {
            m_IsJumping = true;
            m_playingJump = false;
            m_JumpElapsedTime = 0.0f;
        }
    }


    private void FixedUpdate()
    {
        Jump();
        Move();
        Rotate();
    }


    private void Move()
    {
        /*float speed;
        if (m_IsRunning)
        {
            speed = m_SpeedRun;
        }
        else
        {
            speed = m_SpeedMove;
        }*/
        float speed = m_IsRunning ? m_SpeedRun : m_SpeedMove; //Operador ternario, usada somente para atribuição

        m_Body.MovePosition(m_Body.position + m_Movement * speed * Time.fixedDeltaTime);
    }


    private void Rotate()
    {
        if (m_Movement.sqrMagnitude > 0.001f)
        {
            var forwardRotation = Quaternion.Euler(0, -90, 0) * Quaternion.LookRotation(m_Movement);
            m_Body.MoveRotation(Quaternion.Slerp(m_Body.rotation, forwardRotation, m_SpeedRotation * Time.fixedDeltaTime));
        }
    }


    private void Jump()
    {
        if(m_IsJumping && m_JumpElapsedTime > (m_JumpTime / 3))
        {
            if (!Input.GetButton("Jump"))
            {
                //audioSource.clip = m_SmallJump;
                if (!m_playingJump)
                {
                    AudioSource.PlayClipAtPoint(m_SmallJump, transform.position);
                    m_playingJump = true;
                }
                m_IsJumping = false;
            }
            
            if (!m_playingJump)
            {
                //audioSource.clip = m_SuperJump;
                AudioSource.PlayClipAtPoint(m_SuperJump, transform.position);
                m_playingJump = true;
            }
        }

        if (m_IsJumping && m_JumpElapsedTime < m_JumpTime)
        {
            m_JumpElapsedTime += Time.fixedDeltaTime;
            float proportionCompleted = Mathf.Clamp01(m_JumpElapsedTime / m_JumpTime);
            float currentForce = Mathf.Lerp(m_JumpForce, 0.0f, proportionCompleted);
            
            m_Body.AddForce(Vector3.up * currentForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else
        {
            m_IsJumping = false;
        }
    }
}
