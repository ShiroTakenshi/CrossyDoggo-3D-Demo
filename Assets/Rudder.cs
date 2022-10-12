using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudder : MonoBehaviour
{
    [SerializeField] Space space;
    [SerializeField] Vector3 v3;

    [SerializeField] float speed;

    private void FixedUpdate()
    {
        transform.Rotate(v3 * speed *Time.deltaTime, space);
        Debug.Log(Time.fixedTime);
    }

    
}
