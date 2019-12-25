using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;
    private Joycon.Button? m_pressedButtonL;
    private Joycon.Button? m_pressedButtonR;

    public float joyspeedz;
    public float joyspeedy;
    public float joyspeedx;
    public float stickspeed;

    Rigidbody rb;
    Animator anime;

    public float WalkSpeed;
    public float jumpPower;
    float mouseX;
    public float mouseSpeed;

    public GameObject StarEffect;
    public GameObject StunEffect;
    public Transform HitPos;
    public Transform StunHit;
    public Transform Hampos;

    public bool isShield;
    public bool PressShield;
    public bool isStun = false;
    bool isJump;
    bool isAttack = false;

    public GameObject PlayerPre;
    GameObject SpawnPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anime = GetComponent<Animator>();

        SpawnPos = GameObject.Find("PlayerSpawner");

        isShield = false;
        PressShield = false;
        isStun = false;

        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);
        m_joyconR = m_joycons.Find(c => !c.isLeft);
    }

    // Update is called once per frame
    void Update()
    {
        var gyro = m_joyconR.GetAccel();
        var stick = m_joyconL.GetStick();

        stickspeed = stick[0];
        joyspeedy = gyro.y;
        joyspeedx = gyro.x;
        joyspeedz = gyro.z;

        //mouseX = Input.GetAxis(JoyNum) * mouseSpeed;

        if (stickspeed > 0.2f)
        {
            transform.Rotate(0, mouseSpeed, 0 * Time.deltaTime);
        }
        else if(stickspeed < -0.2f)
        {
            transform.Rotate(0, -mouseSpeed, 0 * Time.deltaTime);
        }
        else if(stickspeed == 0)
        {
            transform.Rotate(0, 0, 0);
        }

        anime.SetBool("Walk", false);
        anime.SetBool("Shield", false);

        if (isStun == false)
        {
            WalkSpeed = 13;
            jumpPower = 1800;
        }
        else if (isStun == true)
        {
            WalkSpeed = 0;
            jumpPower = 0;
        }

        if (m_joyconL.GetButton(Joycon.Button.SHOULDER_2) && isShield == false)
        {
            Walk();
            if (joyspeedy >= 0.9f && isAttack == false)
            {
                Attack();
            }
            else if (joyspeedx <= 0.6f && joyspeedz >= 0.8f && isAttack == false && isShield == false && isAttack == false)
            {
                anime.SetTrigger("Attack2");
                isAttack = true;
            }

            if (stickspeed > 0.2f)
            {
                transform.Rotate(0, mouseSpeed-2, 0 * Time.deltaTime);
            }
            else if (stickspeed < -0.2f)
            {
                transform.Rotate(0, -(mouseSpeed-2), 0 * Time.deltaTime);
            }
            else if (stickspeed == 0)
            {
                transform.Rotate(0, 0, 0);
            }
        }

        if (m_joyconR.GetButtonDown(Joycon.Button.SHOULDER_1) && isShield == false && isJump == false)
        {
            Jump();
        }

        else if (joyspeedy >= 0.9f && isAttack == false)
        {
            Attack();
            isAttack = true;
        }
        else if (joyspeedx <= 0.6f && joyspeedz >= 0.8f && isAttack == false && isShield == false && isAttack == false)
        {
        anime.SetTrigger("Attack2");
        isAttack = true;
        }
        else if (m_joyconR.GetButton(Joycon.Button.SHOULDER_2))
        {
        Block();
        }

        if (m_joyconR.GetButtonUp(Joycon.Button.SHOULDER_2))
        {
        isShield = false;
        PressShield = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hammer" && isShield == false)
        {
            Death();
        }
        if (other.gameObject.tag == "Hammer" && isShield == true)
        {
            isShield = false;
            anime.SetBool("Shield", false);
        }
        if (other.gameObject.tag == "Helmet")
        {
            anime.SetTrigger("Stun");
            transform.position += transform.forward * -1.8f;
            StunEffects();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
        }
    }


    void Walk()
    {
        transform.position += transform.forward * Time.deltaTime * WalkSpeed;
        anime.SetBool("Walk", true);
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpPower);
        isJump = true;
    }

    void Attack()
    {
        anime.SetTrigger("Attack");
    }

    void Block()
    {
        anime.SetBool("Shield", true);
        isShield = true;
    }

    public void Stun()
    {
        isStun = true;
    }
    public void UnStun()
    {
        isStun = false;
    }

    void Death()
    {
        anime.SetBool("Dead", true);
        Destroy(gameObject, 1.5f);
        WalkSpeed = 0;
        jumpPower = 0;
    }

    public void Effects()
    {
        Instantiate(StarEffect, HitPos.position, Quaternion.identity);
    }

    public void Effects2()
    {
        Instantiate(StarEffect, Hampos.position, Quaternion.identity);
    }

    public void StunEffects()
    {
        Instantiate(StunEffect, StunHit.position, Quaternion.identity);
    }

    void Respawn()
    {
        Instantiate(PlayerPre, SpawnPos.transform.position, SpawnPos.transform.rotation);
    }
    
    public void ShakeController()
    {
        m_joyconR.SetRumble(240, 480, 0.65f, 100);
    }

    public void ShakeController2()
    {
        m_joyconR.SetRumble(240, 480, 0.65f, 450);
    }
    public void StopAttack()
    {
        isAttack = false;
    }
}