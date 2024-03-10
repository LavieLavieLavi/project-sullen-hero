using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBuzzSaw : MonoBehaviour
{
    [SerializeField] float  RotateSpeedZ;
    [SerializeField] float RotateSpeedY;


    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            transform.Rotate(0, 0, 360 * RotateSpeedZ * Time.deltaTime);
        }

        else if (gameObject.CompareTag("Crown"))
        {
            transform.Rotate(0, 360 * RotateSpeedY * Time.deltaTime, 0);
        }


    }



}
