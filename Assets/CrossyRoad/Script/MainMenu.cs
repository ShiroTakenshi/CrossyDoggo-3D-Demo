using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //memanggil scene gameplay (crossy road);
    public void CrossyRoad()
    {
        SceneManager.LoadScene("CrossyRoad");
    }
}
