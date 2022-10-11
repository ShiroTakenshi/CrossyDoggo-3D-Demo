using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    string varString;
    float varFloat;
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(varString);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(varFloat);
    }
}
