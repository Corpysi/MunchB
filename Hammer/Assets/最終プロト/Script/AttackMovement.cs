using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LorR
{
    Left,
    Right
}

public class AttackMovement : MonoBehaviour
{
    [SerializeField]
    LorR joyconType;

    List<Joycon> joycons;
    Joycon joycon;

    public float gyrox;
    public float gyroy;
    public float gyroz;

    public float Vecx;
    public float Vecy;
    public float Vecz;
    public float Vecw;

    public float Accy;
    public float Accx;
    public float Accz;

    public bool JoyAttack = false;
    public bool FrontAttack = false;

    public bool DownAttack = false;

    void Start()
    {
        joycons = JoyconManager.Instance.j;

        switch (joyconType)
        {
            case LorR.Left:
                joycon = joycons.Find(c => c.isLeft);
                break;

            case LorR.Right:
                joycon = joycons.Find(c => !c.isLeft);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var gyro = joycon.GetGyro();
        var ori = joycon.GetVector();
        var accel = joycon.GetAccel();

        Accy = accel.y;
        Accx = accel.x;
        Accz = accel.z;

        gyroy = gyro.y;
        gyroz = gyro.z;
        gyrox = gyro.x;

        Vecx = ori.x;
        Vecy = ori.y;
        Vecz = ori.z;
        Vecw = ori.w;

        if(Vecw < 0.4f && Vecw > -0.6f)
        {
            FrontAttack = true;
        }
        else
        {
            FrontAttack = false;
        }

        if(Vecy > 0.5f)
        {
            DownAttack = true;
            FrontAttack = false;
        }
        else
        {
            DownAttack = false;
        }

        if(Accy > 1.6f && Accy < 3.4f && JoyAttack == false && FrontAttack == true)
        {
            Debug.Log("Light Attack");
            JoyAttack = true;
        }

        if (Accy > 1.6f && Accy < 2.8f && Accz > -0.4f && JoyAttack == false && FrontAttack == false && DownAttack == false)
        {
            Debug.Log("Side L Attack");
            JoyAttack = true;
        }
        else if (Accy > 1.6f && Accy < 2.8f && Accz < -0.3f && JoyAttack == false && FrontAttack == false && DownAttack == false)
        {
            Debug.Log("Side R Attack");
            JoyAttack = true;
        }

        if(Accx > 1.5f &&  Accy < 2.5f && JoyAttack == false && DownAttack == true)
        {
            Debug.Log("Special Attack");
            JoyAttack = true;
        }


        if (Accy > 3.5f && JoyAttack == false && FrontAttack == true)
        {
            Debug.Log("Light Attack");
            JoyAttack = true;
        }

        if (Accy > 2.9f && Accz > -0.4f && JoyAttack == false && FrontAttack == false && DownAttack == false)
        {
            Debug.Log("Side L Attack");
            JoyAttack = true;
        }
        else if (Accy > 2.9f && Accz < -0.3f && JoyAttack == false && FrontAttack == false && DownAttack == false)
        {
            Debug.Log("Side R Attack");
            JoyAttack = true;
        }

        if (Accx > 2.6f && JoyAttack == false && DownAttack == true)
        {
            Debug.Log("Special Attack");
            JoyAttack = true;
        }

        if(joycon.GetButtonDown(Joycon.Button.SHOULDER_1) && JoyAttack == true)
        {
            joycon.Recenter();
            joycon.SetRumble(200, 200, 10, 1);
            JoyAttack = false;
        }
    }
}