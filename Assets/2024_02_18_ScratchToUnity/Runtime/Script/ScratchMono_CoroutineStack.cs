using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchMono_CoroutineStack :  A_ScratchBlockableMono
{

    public List<A_ScratchBlockableMono> m_scriptToPlay = new List<A_ScratchBlockableMono>();

    [Header("Debug")]
     List<A_ScratchBlockableMono> m_played = new List<A_ScratchBlockableMono>();
     A_ScratchBlockableMono m_playing;
    public int m_index = 0;
    public Coroutine m_coroutine;
    [ContextMenu("Play Stack")]
    public void PlayStackAsCoroutine()
    {
        if (m_coroutine != null)
            StopCoroutine(m_coroutine);
        m_coroutine = StartCoroutine(Coroutine_Play());
    }

    private IEnumerator Coroutine_Play()
    {
        m_played.Clear(); m_index = 0;
        foreach (var item in m_scriptToPlay)
        {
            if (item is I_ScratchBlockable) {
                m_playing = item;
                I_ScratchBlockable block = (I_ScratchBlockable)item;
                yield return block.DoTheScratchableStuff();
                m_played.Add(item);
            }
            m_index++;
        }
    }

    public override IEnumerator DoTheScratchableStuff()
    {
        yield return  Coroutine_Play();
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        Coroutine_Play();
    }
}


public abstract class A_CoroutineScratchBlockableMono : A_ScratchBlockableMono
{

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        DoTheScratchableStuff();
    }
}

public abstract class A_FunctionScratchBlockableMono : A_ScratchBlockableMono
{
    public override IEnumerator DoTheScratchableStuff()
    {
        DoTheScratchableStuffWithoutCoroutine();
        yield return null;
    }
}

[System.Serializable]
public abstract class AbstraceEnumerableDropableScratchBlocks : I_ScratchBlockableEnumrable
{

    public abstract IEnumerable<A_ScratchBlockableMono> GetGroupOfBlock();
}

public abstract class AbstraceEnumerableExecutableDropableScratchBlocks<T> : AbstraceEnumerableDropableScratchBlocks 
where T: AbstraceEnumerableDropableScratchBlocks
{
    public T m_blocksToExecute;
    public ScratchExecutionParallelType m_executionType;
    public ScratchExecutionTiming m_executionTiming;


    public   IEnumerator GetEnumeratorToExecute()
    {
        yield return DropableScratchBlockUtilityExe.Execute(m_executionType, m_executionTiming, m_blocksToExecute);
    }
}



[System.Serializable]
public class DropableScratchBlockableMonoArray : AbstraceEnumerableDropableScratchBlocks
{

    public A_ScratchBlockableMono[] m_array;

    public override IEnumerable<A_ScratchBlockableMono> GetGroupOfBlock()
    { return m_array; }
}


[System.Serializable]
public class DropableScratchBlockableMonoList : AbstraceEnumerableDropableScratchBlocks
{

    public List<A_ScratchBlockableMono> m_list;

    public override IEnumerable<A_ScratchBlockableMono> GetGroupOfBlock()
    { return m_list; }

}



[System.Serializable]
public class DropableScratchBlockableMonoArrayExe : AbstraceEnumerableDropableScratchBlocks
{

    public A_ScratchBlockableMono[] m_array;

    public override IEnumerable<A_ScratchBlockableMono> GetGroupOfBlock()
    { return m_array; }
}


[System.Serializable]
public class DropableScratchBlockableMonoListExe : AbstraceEnumerableDropableScratchBlocks
{

    public List<A_ScratchBlockableMono> m_list;

    public override IEnumerable<A_ScratchBlockableMono> GetGroupOfBlock()
    { return m_list; }

}
public abstract class A_ScratchBlockableMono :MonoBehaviour, I_ScratchBlockable
{


    public abstract IEnumerator DoTheScratchableStuff();
    public  IEnumerator DoTheScratchableStuffDependingOfPlayMode() {


        if (Application.isPlaying) { 
            yield return StartCoroutine(DoTheScratchableStuff());
        }
        else { 
            DoTheScratchableStuffWithoutCoroutine();
            yield return null;
        }
        

    }


    /// <summary>
    ///  To do some scratch you need a coroutine. But most of the time we are not in the editor in play mode.
    ///  This methode allows to do the action that follow without waiting but it means that gliding or other coroutine dependant can't be execute.
    /// </summary>
    /// <returns></returns>
    public abstract void DoTheScratchableStuffWithoutCoroutine();

    [ContextMenu("Execute Block as Coroutine")]
    public void ExecuteBlockAsCoroutine()
    {
        StartCoroutine(DoTheScratchableStuff());
    }

    [ContextMenu("Execute depending of Play Mode")]
    public void ExecuteBlockAsDependingOfPlayMode()
    {
        if (Application.isPlaying)
            StartCoroutine(DoTheScratchableStuff());
        else DoTheScratchableStuffWithoutCoroutine();
    }


    [ContextMenu("Execute Block without coroutine")]
    public void ExecuteBlockWithoutCoroutine()
    {
        DoTheScratchableStuffWithoutCoroutine();
    }

    public void NotifyTheActionWouldBeDangerousToExecuteOutsideOfPlayMode()
    {
        Debug.Log("The block logic is dangerous in none play mode. Won't be executed", this.gameObject);
    }
    public void NotifyTheActionCantBeDoneInNonePlayMode()
    {
        Debug.Log("Must be in play mode to do this action.", this.gameObject);
    }
    public void NotifyTheActionIsPossibleWihoutCoroutineButNotImplementedYet()
    {
        Debug.Log("The current block won't be executed in play mode but could be implemented.", this.gameObject);
    }

}