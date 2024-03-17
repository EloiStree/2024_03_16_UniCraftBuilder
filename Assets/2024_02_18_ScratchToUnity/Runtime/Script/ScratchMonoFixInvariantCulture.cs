using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;

public class ScratchMonoFixInvariantCulture : MonoBehaviour
{
    static ScratchMonoFixInvariantCulture()
    {
        SetApplicationAsCultureInvariant();
    }

    void Awake()
    {
        SetApplicationAsCultureInvariant();
    }
    private void OnValidate()
    {
        SetApplicationAsCultureInvariant();

    }

    public static void SetApplicationAsCultureInvariant()
    {

        Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

    }
}
