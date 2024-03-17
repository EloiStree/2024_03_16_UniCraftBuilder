using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScratchMono_OnGameStart : MonoBehaviour
{
    public UnityEvent m_onGameAwake;
    public UnityEvent m_onGameStart;
    public DropableScratchBlockableMonoArrayExe m_onAwakeBlocks;
    public DropableScratchBlockableMonoArrayExe m_onStartBlocks;
    public ScratchExecutionParallelType m_executionType;
    public ScratchExecutionTiming m_executionTiming;

    [ContextMenu("Repush Awake")]
    public void Awake()
    {
        m_onGameAwake.Invoke();
      StartCoroutine(  DropableScratchBlockUtilityExe.Execute(m_executionType, m_executionTiming, m_onAwakeBlocks));
    }
    [ContextMenu("Repush Start")]
    public void Start()
    {
        m_onGameStart.Invoke();
        StartCoroutine(DropableScratchBlockUtilityExe.Execute(m_executionType, m_executionTiming, m_onStartBlocks));
    }
}
public enum ScratchExecutionParallelType { InParallel, StepByStep }
public enum ScratchExecutionTiming { LetPlayModeChoose, UseCoroutine, ExecuteDirectly  }