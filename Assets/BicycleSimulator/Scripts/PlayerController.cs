using System.Collections.Generic;
using UnityEngine;
using System; 

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float angle;

    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMotor motor;

    protected Queue<Vector3> filterDataQueue = new Queue<Vector3>();
    public int filterLength = 3;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();

        for (int i = 0; i < filterLength; i++)
            filterDataQueue.Enqueue(Input.acceleration);
    }

    void Update()
    {
        speed = LowPassAccelerometer().y * 7.0f;
        float _movement = Mathf.Min(speed, 5.0f) * 4.5f;
        motor.Move(_movement);

        angle = LowPassAccelerometer().x * 360.0f;
        Vector3 _rotation = new Vector3(0.0f, Mathf.Pow(speed, 0.5f) * Mathf.Sin(Mathf.Deg2Rad * angle) * 0.8f, 0.0f);
        motor.Rotate(_rotation);
    }

    public Vector3 LowPassAccelerometer()
    {
        if (filterLength <= 0)
            return Input.acceleration;

        filterDataQueue.Enqueue(Input.acceleration);
        filterDataQueue.Dequeue();

        Vector3 vFiltered = Vector3.zero;
        foreach (Vector3 v in filterDataQueue)
            vFiltered += v;
        vFiltered /= filterLength;
        return vFiltered;
    }
}