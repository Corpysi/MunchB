using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerTest : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSpeed;

        transform.Rotate(0, mouseX, 0 * Time.deltaTime);
        Cursor.lockState = CursorLockMode.Locked;

        anime.SetBool("Walk", false);
        anime.SetBool("Shield", false);

        if (isStun == false)
        {
            WalkSpeed = 13;
            jumpPower = 1800;
        }
        else if(isStun == true)
        {
            WalkSpeed = 0;
            jumpPower = 0;
        }

        if (Input.GetKey(KeyCode.Mouse0) && isShield == false)
        {
            Walk();
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Attack();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse4))
            {
                anime.SetTrigger("Attack2");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isShield == false && isJump == false)
        {
            Jump();
        }

        else if (Input.GetKeyDown(KeyCode.Mouse1) && isShield == false) 
        {
            Attack();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse4) && isShield == false)
        {
            anime.SetTrigger("Attack2");
        }
        else if (Input.GetKey(KeyCode.Mouse3) && PressShield == false)
        {
            Block();
        }

        if (Input.GetKeyUp(KeyCode.Mouse3))
        {
            isShield = false;
            PressShield = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hammer" && isShield == false)
        {
            Death();
        }
        if(other.gameObject.tag == "Hammer" && isShield == true)
        {
            isShield = false;
            anime.SetBool("Shield", false);
        }
        if(other.gameObject.tag == "Helmet")
        {
            anime.SetTrigger("Stun");
            transform.position += transform.forward * -1.8f;
            StunEffects();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
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
