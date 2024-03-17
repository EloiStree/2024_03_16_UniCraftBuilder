using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScratchAbstractMono_PushSoloSerialPort : A_ScratchBlockableMono
{
    public string m_comId = "COM1";
    public string m_messageAsText;

    public override IEnumerator DoTheScratchableStuff()
    {
        yield return null;
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
    }
}



public class ScratchAbstractMono_PushSoloSerialPort_Bytes : MonoBehaviour
{
    public string m_comId = "COM1";
    public byte[] m_messageAsbytes;
}
/// <summary>
/// Warning some native code can't use the name
/// </summary>
public class ScratchAbstractMono_PushSoloSerialPortDeviceName : MonoBehaviour
{
    public string m_deviceName = "";
    public string m_messageAsText;
}

/// <summary>
/// Warning some native code can't use the name
/// </summary>
public class ScratchAbstractMono_PushSoloSerialPortDeviceName_Bytes : MonoBehaviour
{
    public string m_deviceName = "";
    public byte[] m_messageAsbytes;
}
