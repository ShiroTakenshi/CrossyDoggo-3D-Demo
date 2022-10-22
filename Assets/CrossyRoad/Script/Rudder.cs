using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudder : MonoBehaviour
{
    [SerializeField] Vector3 v3;

    [SerializeField] float speed = 0;

    private void Update()
    {
        transform.Rotate(v3 * Time.deltaTime * speed);
    }


}
