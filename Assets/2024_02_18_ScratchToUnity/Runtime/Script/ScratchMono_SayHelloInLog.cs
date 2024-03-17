using System.Collections;
using UnityEngine;

public class ScratchMono_SayHelloInLog : A_ScratchBlockableMono
{
    public string m_textToDisplay;
    public LogType m_logType;
    public enum LogType { Log, Warning,Error}
    public override IEnumerator DoTheScratchableStuff()
    {
        DoTheScratchableStuffWithoutCoroutine();
           yield return null;
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        if (m_logType == LogType.Log)
            Debug.Log(m_textToDisplay);
        else if (m_logType == LogType.Warning)
            Debug.LogWarning(m_textToDisplay);
        else if (m_logType == LogType.Error)
            Debug.LogError(m_textToDisplay);
    }
}


