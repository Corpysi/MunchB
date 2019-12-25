using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject Cam1;
    public GameObject Cam2;
    public GameObject Cam3;

    public GameObject BotObj;
    public GameObject PlayerObj;
    public Transform SpawnPos;
    public Transform SpawnPos2;
    // Start is called before the first frame update
    void Start()
    {
        Cam1.SetActive(true);
        Cam2.SetActive(false);
        Cam3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Cam1.SetActive(true);
            Cam2.SetActive(false);
            Cam3.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            Cam1.SetActive(false);
            Cam2.SetActive(true);
            Cam3.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Cam1.SetActive(false);
            Cam2.SetActive(false);
            Cam3.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Joystick3Button1))
        {
            Instantiate(BotObj, SpawnPos.position, SpawnPos.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Instantiate(PlayerObj, SpawnPos2.position, SpawnPos2.rotation);
        }
    }
}
