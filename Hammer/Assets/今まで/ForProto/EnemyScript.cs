using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody rb;
    bool isHit = false;

    public Transform exPos;
    public GameObject hitEffect;

    public int maxHP;
    int HP;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player_Bullet")
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
            rb.AddExplosionForce(32.5f, new Vector3(other.transform.position.x,other.transform.position.y - 2f,other.transform.position.z - 1f), 20f,2.5f, ForceMode.Impulse);
            Destroy(gameObject, 2f);
        }
    }
}
