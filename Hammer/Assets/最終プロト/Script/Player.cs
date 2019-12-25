using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LorRforMove
{
    Left,
    Right
}

public class Player : MonoBehaviour
{
    //左ジョイコンのスティック関係
    [SerializeField]
    LorRforMove joyconType;

    List<Joycon> joycons;
    Joycon joycon;


    public string Left1;
    public string Left2;

    public float moveSpeed;
    public float getRotateSpeed;
    //

    void Start()
    {
        joycons = JoyconManager.Instance.j;

        switch (joyconType)
        {
            case LorRforMove.Left:
                joycon = joycons.Find(c => c.isLeft);
                break;

            case LorRforMove.Right:
                joycon = joycons.Find(c => !c.isLeft);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //左ジョイコンの移動
        var stick = joycon.GetStick();

        Vector3 stickAxis = new Vector3(stick[0], 0, stick[1]);

        transform.Translate(stickAxis * moveSpeed * Time.deltaTime, Space.World);

        if (stickAxis != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(stickAxis), getRotateSpeed);
        }
        //


    }
}
