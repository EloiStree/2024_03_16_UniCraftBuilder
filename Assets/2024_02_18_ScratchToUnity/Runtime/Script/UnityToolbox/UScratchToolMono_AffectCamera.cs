using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UScratchToolMono_AffectCamera : A_ScratchBlockableMono
{
    public Camera m_toAffect;

    public float m_timeToChange=3;
    public void ChangeWithRandomColor()
    {
        m_toAffect.backgroundColor = GetRandomColor();
    }
    public IEnumerator ChangeWithRandomColor(float timeToChange)
    {
        Color current = m_toAffect.backgroundColor;
        Color newColor =  GetRandomColor(); 
        float tStart = Time.time;
        float tEnd = Time.time +timeToChange;
        if (tEnd - tStart != 0) { 
            while (tEnd > Time.time) {

                m_toAffect.backgroundColor= Color.Lerp(current, newColor, 1f-((tEnd - Time.time) / (tEnd - tStart)));
                yield return new WaitForEndOfFrame();
            }
        }
        m_toAffect.backgroundColor = newColor;
        yield return null;
    }

    public override IEnumerator DoTheScratchableStuff()
    {
        if (m_timeToChange < 0)
            ChangeWithRandomColor();
        else yield return ChangeWithRandomColor(m_timeToChange);
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        ChangeWithRandomColor();
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1);
    }

    private void Reset()
    {
        m_toAffect = Camera.main;
    }
}
