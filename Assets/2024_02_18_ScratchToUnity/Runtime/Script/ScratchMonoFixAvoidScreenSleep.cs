using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchMonoFixAvoidScreenSleep : MonoBehaviour
{
    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
