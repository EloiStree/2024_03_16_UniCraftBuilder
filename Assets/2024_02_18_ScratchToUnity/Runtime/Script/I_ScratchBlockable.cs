using System.Collections;
using System.Collections.Generic;

public interface I_ScratchBlockable {
    public IEnumerator DoTheScratchableStuff();
    public void DoTheScratchableStuffWithoutCoroutine();
    public IEnumerator DoTheScratchableStuffDependingOfPlayMode();
}


public interface I_ScratchBlockableEnumrable {
    public IEnumerable<A_ScratchBlockableMono> GetGroupOfBlock();
}




