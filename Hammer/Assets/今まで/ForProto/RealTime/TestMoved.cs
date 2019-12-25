using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoved : MonoBehaviour
{
    [SerializeField]
    LorR joyconType;

    List<Joycon> joycons;
    Joycon joycon;

    public Vector3 rotation;
    Vector3 rotationOffset = new Vector3(0, 180, 0);

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

    Animator anime;

    public float resetTime;
    public float startTime;

    public GameObject pointEfe;
    // Start is called before the first frame update
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

        anime = GetComponent<Animator>();
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

        if (Vecw < 0.6f && Vecw > -0.6f)
        {
            FrontAttack = true;
        }
        else
        {
            FrontAttack = false;
        }

        if (Vecy > 0.5f)
        {
            DownAttack = true;
            FrontAttack = false;
        }
        else
        {
            DownAttack = false;
        }

        if (Accy > 1.1f)
        {
            transform.position += transform.forward * 15 * Time.deltaTime;
        }
    }

    public void StopAttackBool()
    {

    }
}