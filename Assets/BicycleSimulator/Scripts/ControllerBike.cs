using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(Conectar))]
public class ControllerBike : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float angle;
    [SerializeField]
    private int freno = 0;
    [SerializeField]
    private int velocidad = 0;
    [SerializeField]
    private int var_freno = 0;
    [SerializeField]
    private int seguir = 0;
    GameObject hudText;

    private SerialPort serialPort;

    private PlayerMotor motor;

    public Conectar conectar;
    private Text txt;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        conectar = GetComponent<Conectar>();
        hudText = GameObject.FindWithTag("InfoText"); ;
        txt = hudText.GetComponent<Text>();
    }
    
    void Update()
    {
        txt.text = "INPUT : " + freno.ToString() + "       |       Velocidad : " + velocidad.ToString();
    }

    void FixedUpdate()
    {

        try
        {
            string value = conectar.data;
            string[] vals = value.Split(',');
            var_freno = freno;
            velocidad = Int32.Parse(vals[1]);
            //freno = freno-1;
            freno = Int32.Parse(vals[0]);

            bool frenar = false;

            if (freno < 400)
            {
                frenar = true;
            }
            else frenar = false;

            /*           if (frenar == false && velocidad == 0 && seguir > 30)
                       {
                           velocidad = seguir;
                       }*/
            try
            {
                velocidad = velocidad / 30;
                if ((freno > 500) && (velocidad != 0))
                {
                    speed = velocidad / 2; //2
                }
                else if (freno > 465 && freno <= 500)
                {
                    speed = velocidad / 5;
                }
                else if (freno > 430 && freno <= 465)
                {
                    speed = velocidad / 7;
                }
                else if (freno > 400 && freno <= 430)
                {
                    speed = velocidad / 10;
                }
                else speed = 0;
            }
            catch
            {
                Debug.Log("Error control de velocidad");
            }

            float mousepos = Input.mousePosition.x;// ("Mouse X");

            //angle = mousepos;
            if (mousepos < 650 && mousepos > 600)
            {
                angle = -10;

            }
            else if (mousepos < 600 && mousepos > 550)
            {
                angle = -20;
            }
            else if (mousepos < 550)
            {
                angle = -30;
            }
            else if (mousepos > 650 && mousepos < 710)
            {
                angle = 0;
            }
            else if (mousepos > 710 && mousepos < 760)
            {
                angle = 10;
            }
            else if (mousepos > 760 && mousepos < 810)
            {
                angle = 20;
            }
            else if (mousepos > 810)
            {
                angle = 30;
            }

            float _movement = Mathf.Min(speed, 5.0f) * 9.0f;
            motor.Move(_movement);

            Vector3 _rotation = new Vector3(0.0f, Mathf.Pow(speed, 0.5f) * Mathf.Sin(Mathf.Deg2Rad * angle) * 0.8f, 0.0f);

            motor.Rotate(_rotation);

            seguir = velocidad;
        }
        catch
        {
            Debug.Log("Error Update Bike");
        }

    }

    bool verificar(String[] vec)
    {
        String a = vec[0];
        for (int i = 0; i < vec.Length - 2; i++)
        {
            if (a != vec[i])
            {
                return true;
            }
        }
        return false;
    }
}
