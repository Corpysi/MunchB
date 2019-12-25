using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 stickAxis = new Vector3(Input.GetAxisRaw("StickHor"), 0, Input.GetAxisRaw("StickVer"));

        transform.Translate(stickAxis * 14.5f * Time.deltaTime , Space.World);

        if (stickAxis != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(stickAxis), 0.18f);
        }
    }
}
