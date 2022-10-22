using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenRes : MonoBehaviour
{
    List<int> widths = new List<int>() { 1366, 1280, 1024, 800 };
    List<int> heights = new List<int>() { 768, 720, 768, 600 };

    public void SetScreenSize(int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullscreen);
        Screen.SetResolution(width, height, fullscreen);
        Debug.Log("Resolusi Layar Diganti" + (width, height));
    }
}
