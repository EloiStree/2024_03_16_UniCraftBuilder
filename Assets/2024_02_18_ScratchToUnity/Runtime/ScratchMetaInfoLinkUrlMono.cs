using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class AbstractScratchUrlMono :  A_ScratchBlockableMono
{
    [ContextMenu("Open Page")]
    public void OpenPage()
    {
        Application.OpenURL(FetchUrl());
    }
    public abstract string FetchUrl();

    public override IEnumerator DoTheScratchableStuff()
    {
        OpenPage();
        yield return null;
    }
    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        OpenPage();
    }
}



