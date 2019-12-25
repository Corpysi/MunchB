using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Animator anime;
    public float movementSpeed;
    public float rotateSpeed;

    int weaponMode;
    int changeMode;

    bool isAttack = false;

    public Transform changePos;
    public GameObject changeEfe;

    public GameObject hammerObj;
    public GameObject swordObj;
    public GameObject boomObj;
    public GameObject c_HammerObj;
    public GameObject c_SwordObj;
    public GameObject c_boomObj;

    public GameObject hitEfe;
    public Transform hitPos;

    public Transform sword_PPos;
    public Transform sword_PSPos;
    public Transform sword_PDPos;
    public GameObject swordPro;
    public GameObject swordProSide;
    public GameObject swordProDown;

    public Transform boomthrowPos;
    public GameObject boomPro;
    bool isThrow = false;
    float throwtime = 4f;

    public GameObject uiWeapon1;
    public GameObject uiWeapon2;

    public float boxx;
    public float boxy;
    public float boxz;

    public LayerMask enemyMask;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += (transform.forward * movementSpeed) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += (transform.forward * -movementSpeed) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (Input.GetKeyDown(KeyCode.U) && weaponMode == 0 || Input.GetKeyDown(KeyCode.U) && weaponMode == 3)
        {
            StartCoroutine(ChangeSword());
        }
        if (Input.GetKeyDown(KeyCode.I) && weaponMode == 1 || Input.GetKeyDown(KeyCode.I) && weaponMode == 3)
        {
            StartCoroutine(ChangeHammer());
        }
        if (Input.GetKeyDown(KeyCode.O) && weaponMode == 1 || Input.GetKeyDown(KeyCode.O) && weaponMode == 0)
        {
            StartCoroutine(ChangeBoom());
        }

        if (weaponMode == 0)
        {
            boomObj.SetActive(false);
            swordObj.SetActive(false);
            hammerObj.SetActive(true);

            uiWeapon1.SetActive(true);
            uiWeapon2.SetActive(false);

            anime.SetBool("isSword", false);

            if (Input.GetKeyDown(KeyCode.J) && isAttack == false)
            {
                isAttack = true;
                HammerAttack();
            }
        }
        else if (weaponMode == 1)
        {
            boomObj.SetActive(false);
            hammerObj.SetActive(false);
            swordObj.SetActive(true);

            uiWeapon1.SetActive(false);
            uiWeapon2.SetActive(true);

            anime.SetBool("isSword", true);

            if (Input.GetKeyDown(KeyCode.J) && isAttack == false)
            {
                isAttack = true;
                SwordFrontAttack();
            }
            if (Input.GetKeyDown(KeyCode.K) && isAttack == false)
            {
                isAttack = true;
                SwordSideAttack();
            }
            if (Input.GetKeyDown(KeyCode.L) && isAttack == false)
            {
                isAttack = true;
                SwordSideAttack2();
            }
            if (Input.GetKeyDown(KeyCode.Space) && isAttack == false)
            {
                isAttack = true;
                SwordDownAttack();
            }
        }
        else if(weaponMode == 2)
        {
            boomObj.SetActive(false);
            hammerObj.SetActive(false);
            swordObj.SetActive(false);
        }
        else if(weaponMode == 3)
        {
            hammerObj.SetActive(false);
            swordObj.SetActive(false);

            uiWeapon1.SetActive(false);
            uiWeapon2.SetActive(true);

            if (isThrow == false)
            {
                throwtime = 4f;
                boomObj.SetActive(true);
            }
            else if(isThrow == true)
            {
                throwtime -= Time.deltaTime;
                boomObj.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isThrow = true;
                Instantiate(boomPro, boomthrowPos.position, boomthrowPos.rotation);
            }
            if (throwtime < 0)
            {
                isThrow = false;
            }
        }

        if(changeMode == 0)
        {
            c_boomObj.SetActive(false);
            c_HammerObj.SetActive(false);
            c_SwordObj.SetActive(false);
        }
        else if(changeMode == 1)
        {
            c_boomObj.SetActive(false);
            c_HammerObj.SetActive(true);
            c_SwordObj.SetActive(false);

        }
        else if(changeMode == 2)
        {
            c_boomObj.SetActive(false);
            c_HammerObj.SetActive(false);
            c_SwordObj.SetActive(true);
        }
        else if(changeMode == 3)
        {
            c_boomObj.SetActive(true);
            c_HammerObj.SetActive(false);
            c_SwordObj.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ItemBox")
        {
            StartCoroutine(ChangeSword());
            Destroy(collision.gameObject);
        }
    }

    IEnumerator ChangeSword()
    {
        Instantiate(changeEfe, changePos.position, changePos.rotation);
        yield return new WaitForSeconds(0.01f);
        weaponMode = 2;
        changeMode = 2;
        yield return new WaitForSeconds(0.2f);
        changeMode = 0;
        weaponMode = 1;
        yield break;
    }

    IEnumerator ChangeHammer()
    {
        Instantiate(changeEfe, changePos.position, changePos.rotation);
        yield return new WaitForSeconds(0.01f);
        weaponMode = 2;
        changeMode = 1;
        yield return new WaitForSeconds(0.2f);
        changeMode = 0;
        weaponMode = 0;
        yield break;
    }

    IEnumerator ChangeBoom()
    {
        Instantiate(changeEfe, changePos.position, changePos.rotation);
        yield return new WaitForSeconds(0.01f);
        weaponMode = 2;
        changeMode = 3;
        yield return new WaitForSeconds(0.2f);
        changeMode = 0;
        weaponMode = 3;
        yield break;
    }

    public void HammerEffect()
    {
        Instantiate(hitEfe, hitPos.position, Quaternion.identity);
    }
    
    public void SwordFrontProjectile()
    {
        Instantiate(swordPro, sword_PPos.position, sword_PPos.rotation);
    }

    public void SwordSideProjectile()
    {
        Instantiate(swordProSide, sword_PSPos.position, sword_PSPos.rotation);
    }

    public void SwordDownProjectile()
    {
        Instantiate(swordProDown, sword_PDPos.position, sword_PDPos.rotation);
    }

    public void StopAttack()
    {
        isAttack = false;
    }

    void HammerAttack()
    {
        anime.SetTrigger("hammerAttack");
    }

    void SwordFrontAttack()
    {
        anime.SetTrigger("swordfrontAttack");
    }

    void SwordSideAttack()
    {
        anime.SetTrigger("swordsideAttack");

        Collider[] hitEnemies = Physics.OverlapBox(sword_PPos.position, new Vector3(boxx, boxy, boxz), sword_PPos.rotation, enemyMask);
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);
        }
    }
    void SwordSideAttack2()
    {
        anime.SetTrigger("swordsideAttack2");
    }
    void SwordDownAttack()
    {
        anime.SetTrigger("sworddownAttack");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(sword_PPos.position, new Vector3(boxx, boxy, boxz));
    }
}
