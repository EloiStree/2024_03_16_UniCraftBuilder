using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Scratch3DMono_DefaultBlocksExecutionManager : Scratch3DMono_AbstractBlocksExecutionManager {

    public A_ScratchBlockableMono m_blockToExecute;
    public Scratch3DMono_AbstractSearchNextCastBlocks m_lookForNextBlock;
    public Scratch3DUIMono_DebugStateEnterExit m_useEnterExitDebug;
    public NextType m_executionOfNextBlock = NextType.JustTheFirstNextBlock;
    public NextExecuteType m_executionTypeForNext = NextExecuteType.WaitFinishing;
    public enum NextType { ForeachNextBlocks, JustTheFirstNextBlock }
    public enum NextExecuteType {WaitFinishing, Broadcast }

    public override IEnumerator DoTheScratchableStuff()
    {
        NotifyEnterNextUpdate();
        if (m_blockToExecute != null)
        {
            yield return (m_blockToExecute.DoTheScratchableStuff());
        }

        if (m_lookForNextBlock == null)
        {
            NotifyExitNextUpdate();
        }
        else { 
            m_lookForNextBlock.GetNextBlocks(out IEnumerable<Scratch3DRootTagMono> rootTags);
            if (rootTags != null) {


                if (m_executionOfNextBlock==NextType.ForeachNextBlocks) {
                    if (rootTags != null && rootTags.Count() > 0)
                    {
                        foreach (var item in rootTags)
                        {
                            if (item != null)
                            {
                                yield return (ExecuteNext(item));
                            }
                        }

                    }
                }
                else if (m_executionOfNextBlock == NextType.JustTheFirstNextBlock)
                {
                    if (rootTags!=null && rootTags.Count() > 0) {
                        Scratch3DRootTagMono next = rootTags.First();
                        yield return (ExecuteNext(next));
                    }
                    
                }
            }
        }

        NotifyExitNextUpdate();
        yield return null;
    }

    

    private void NotifyExitNextUpdate()
    {
        if(m_useEnterExitDebug)
            m_useEnterExitDebug.NotifyExitNextUpdate();
    }

    private void NotifyEnterNextUpdate()
    {
        if (m_useEnterExitDebug)
            m_useEnterExitDebug.NotifyEnterNextUpdate();
    }

    private IEnumerator ExecuteNext(Scratch3DRootTagMono next)
    {
        if (next != null) { 
            Scratch3DMonoStaticUtility.SearchForScratch3DMonoExecutor(
                             next, out bool fnext, out Scratch3DMono_AbstractBlocksExecutionManager executor);
            if (fnext && executor != null)
            {
                if (m_executionTypeForNext == NextExecuteType.WaitFinishing)
                    yield return executor.DoTheScratchableStuff();
                else executor.ExecuteBlockAsCoroutine();
            }
        }
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        //Not tested;
        DoTheScratchableStuff();
    }
}