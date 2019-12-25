using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTest : MonoBehaviour
{
    Rigidbody rb;
    Animator anime;

    public float rotateSpeed;

    public float WalkSpeed;
    public float jumpPower;

    public GameObject StarEffect;
    public Transform Hampos;
    public GameObject StunEffect;
    public Transform HitPos;
    public Transform StunHit;

    public bool isShield;
    public bool PressShield;
    public bool isStun = false;
    bool isJump;

    public GameObject PlayerPre;
    GameObject SpawnPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anime = GetComponent<Animator>();
        SpawnPos = GameObject.Find("BotSpawner");

        isShield = false;
        PressShield = false;
        isStun = false;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetKey(KeyCode.W) && isShield == false)
        {
            Walk();
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack();
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                anime.SetTrigger("Attack2");
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -rotateSpeed, 0 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, +rotateSpeed, 0 * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.L) && isShield == false && isJump == false)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.J) && isShield == false)
        {
            Attack();
        }
        else if (Input.GetKeyDown(KeyCode.K) && isShield == false)
        {
            anime.SetTrigger("Attack2");
        }
        else if (Input.GetKey(KeyCode.S) && PressShield == false)
        {
            Block();
        }

        if (Input.GetKeyUp(KeyCode.S))
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
            PressShield = true;
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
}