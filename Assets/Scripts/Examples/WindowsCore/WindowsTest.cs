using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WARNING! Attach this script will cause system shut down!!!
/// Please save all your work and test this script.
/// </summary>
public class WindowsTest : MonoBehaviour {
    public int countDownTime = 10;
	// Use this for initialization
	void Start () {
        Invoke("ShutdownWindows", countDownTime);	
	}

    void ShurdownWindows()
    {
        WindowsCore.DoExitWindows(WindowsCore.EWX_SHUTDOWN);
    }
	
}
