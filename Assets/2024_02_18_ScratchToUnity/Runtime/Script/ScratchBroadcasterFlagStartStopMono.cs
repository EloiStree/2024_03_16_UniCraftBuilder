using UnityEngine;

public class ScratchBroadcasterFlagStartStopMono :MonoBehaviour{

   [SerializeField ]bool m_useAutoStartAtEnable = true;
   [SerializeField] bool m_useAutoStopAtDisable = true;
   [SerializeField] bool m_useAutoStopAtDestroy = true;
     void OnEnable()
    {
        if (m_useAutoStartAtEnable)
        {
            BroadcastGreenFlagStartEvent();
        }
    }
     void OnDisable()
    {
        if (m_useAutoStopAtDisable)
        {
            BroadcastGreenRedStopEvent();
        }
    }
     void OnDestroy()
    {
        if (m_useAutoStopAtDestroy)
        {
            BroadcastGreenRedStopEvent();
        }
    }


    public  void BroadcastGreenFlagStartEvent()
    {
        ScratchBroadcasterSingletonFlagStartStop.TriggerStartFlag();
    }
    public void BroadcastGreenRedStopEvent()
    {
        ScratchBroadcasterSingletonFlagStartStop.TriggerRedStop();
    }
}