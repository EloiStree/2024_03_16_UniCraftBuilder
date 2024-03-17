using System.Collections;

public class DropableScratchBlockUtilityExe {

    public static IEnumerator Execute(ScratchExecutionParallelType executionType, ScratchExecutionTiming executionTiming,
        params AbstraceEnumerableDropableScratchBlocks[] groupOfAction)
    {
        foreach (var item in groupOfAction)
        {
            yield return Execute(executionType, executionTiming, item);
        }
    }
    public static IEnumerator Execute(ScratchExecutionParallelType executionType, ScratchExecutionTiming executionTiming,
        AbstraceEnumerableDropableScratchBlocks groupOfAction)
    {
        foreach (var item in groupOfAction.GetGroupOfBlock())
        {
            if (executionType == ScratchExecutionParallelType.InParallel)
            {
                if (executionTiming == ScratchExecutionTiming.LetPlayModeChoose)
                    item.ExecuteBlockAsDependingOfPlayMode();
                if (executionTiming == ScratchExecutionTiming.UseCoroutine)
                    item.ExecuteBlockAsCoroutine();
                if (executionTiming == ScratchExecutionTiming.ExecuteDirectly)
                    item.ExecuteBlockWithoutCoroutine();
            }
            if (executionType == ScratchExecutionParallelType.StepByStep)
            {
                if (executionTiming == ScratchExecutionTiming.UseCoroutine)
                   yield  return  item.DoTheScratchableStuff();
                if (executionTiming == ScratchExecutionTiming.ExecuteDirectly)
                    item.DoTheScratchableStuffWithoutCoroutine();
                if (executionTiming == ScratchExecutionTiming.LetPlayModeChoose)
                    yield return item.DoTheScratchableStuffDependingOfPlayMode();
            }
        }
        yield return null;
    }
}
