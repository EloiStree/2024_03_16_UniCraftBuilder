using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// ///This class should send an abstract value as struct to be ready in the project and by developer that want to code the native code or push it out on udp/websocket.
/// To Code later.
/// </summary>
public class ScratchAbstractMono_PushSoloUDP : MonoBehaviour
{
    public string m_ipAddress;
    public string m_port;
    public string m_messageToSendAsTextUTF8;

}
public class ScratchAbstractMono_PushSoloUDP_Unicode : MonoBehaviour
{
    public string m_ipAddress;
    public string m_port;
    public string m_messageToSendAsText;

}


public class ScratchAbstractMono_PushSoloUDP_Bytes : MonoBehaviour
{
    public string m_ipAddress;
    public string m_port;
    public byte[] m_messageToSendAsBytes= new byte[2];
}