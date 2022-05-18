using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class takeScreenshot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.S))
        {
            TakeScreenshot();
            Debug.Log("shot!");
        }
    }

    private string directoryName = "Screenshots";
    

    public void TakeScreenshot()
    {
        DirectoryInfo screenshotDirectory = Directory.CreateDirectory(directoryName);
        string fileName = "screenshot" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png";
        string fullPath = Path.Combine(screenshotDirectory.FullName, fileName);
        ScreenCapture.CaptureScreenshot(fullPath);
    }
}
