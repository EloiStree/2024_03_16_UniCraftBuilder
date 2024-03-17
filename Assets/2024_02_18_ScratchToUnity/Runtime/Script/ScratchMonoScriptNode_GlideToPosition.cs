using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchMonoScriptNode_GlideToPosition : A_ScratchBlockableMono
{ 

    public AbstractScratchMono_VariableHolderVector3 m_position;
    public AbstractScratchMono_VariableHolderAsString m_timeToGlide;
    public Transform m_whatToMove;


    [Header("Visible private debug")]
    public Vector3 m_startPoint;
    public float m_timeInGameStartGliding;
    public Vector3 m_endPoint;
    public float m_timeInGameEndGliding;


    public Coroutine m_currentGliding;

    [ContextMenu("Glide to position")]
    public void GlideToValueInInspector()
    {
        GlideToGivenValue(m_position.GetVector3(), m_timeToGlide.GetValueAsFloat());
    }
    public void GlideToGivenValue(Vector3 position, float timeToGlide)
    {
        if (m_currentGliding != null)
            StopCoroutine(m_currentGliding);

        SetGlidingSetup(position, timeToGlide);

        m_currentGliding = StartCoroutine(Coroutinable_GlideToGivenValue());
    }

    private void SetGlidingSetup(Vector3 position, float timeToGlide)
    {
        m_startPoint = m_whatToMove.position;
        m_timeInGameStartGliding = Time.time;

        m_endPoint = position;
        m_timeInGameEndGliding = Time.time + timeToGlide;
    }

    public IEnumerator Coroutinable_GlideToGivenValue()
    {

        while (Time.time <= m_timeInGameEndGliding)
        {

            if ((m_timeInGameEndGliding - m_timeInGameStartGliding) == 0)
                break;
            float percentThere = 1f - (m_timeInGameEndGliding - Time.time) / (m_timeInGameEndGliding - m_timeInGameStartGliding);
            m_whatToMove.position = Vector3.Lerp(m_startPoint, m_endPoint, percentThere);
            yield return new WaitForEndOfFrame();
        }
    }

    private void Reset()
    {
        m_whatToMove = this.transform;
    }

    public override IEnumerator DoTheScratchableStuff()
    {
        SetGlidingSetup(m_position.GetVector3(), m_timeToGlide.GetValueAsFloat());
        yield return Coroutinable_GlideToGivenValue();
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        m_whatToMove.position = m_position.GetVector3();
    }
}
