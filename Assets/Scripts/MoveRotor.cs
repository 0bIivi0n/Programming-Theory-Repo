using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRotor : MonoBehaviour
{
    private float speed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, (360.0f * Time.deltaTime * speed), 0.0f);
    }
}
