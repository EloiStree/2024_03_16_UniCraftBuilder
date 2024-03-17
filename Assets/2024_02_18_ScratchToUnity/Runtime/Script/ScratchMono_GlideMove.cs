using System.Collections;
using UnityEngine;

public class ScratchMono_GlideMove : A_ScratchBlockableMono
{

    public float m_moveValue;
    public float m_timeToGlide;
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
        GlideToGivenValue(m_moveValue, m_timeToGlide);
    }
    public void GlideToGivenValue(float moveValue, float timeToGlide)
    {
        if (m_currentGliding != null)
            StopCoroutine(m_currentGliding);
        m_currentGliding = StartCoroutine(Coroutinable_GlideToGivenValue(moveValue, timeToGlide));
    }

    private void RefreshPathToRun(float moveValue, float timeToGlide)
    {
        m_startPoint = m_whatToMove.position;
        m_timeInGameStartGliding = Time.time;

        m_endPoint = m_whatToMove.position+ m_whatToMove.forward* moveValue;
        m_timeInGameEndGliding = Time.time + timeToGlide;
    }

    public IEnumerator Coroutinable_GlideToGivenValue(float moveValue, float timeToGlide)
    {

        RefreshPathToRun(moveValue, timeToGlide);
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
        return Coroutinable_GlideToGivenValue(m_moveValue, m_timeToGlide);
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        m_whatToMove.position =m_whatToMove.position + m_whatToMove.forward * m_moveValue;
    }
}
