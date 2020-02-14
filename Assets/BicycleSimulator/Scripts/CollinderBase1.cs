using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollinderBase1 : MonoBehaviour
{
    public bool _collinder;

    private void Start()
    {
        _collinder = false;
    }

    void OnTriggerEnter(Collider obj)
    {
        _collinder = true;
        Debug.Log(_collinder);
    }

    /*void OnTriggerExit(Collider obj)
    {
        _collinder = false;
    }
    */
}
