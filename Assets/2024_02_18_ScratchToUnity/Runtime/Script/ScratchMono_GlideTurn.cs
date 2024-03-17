using System.Collections;
using UnityEngine;

public class ScratchMono_GlideTurn : A_ScratchBlockableMono
{

    public float m_rotationInAngleLeftRight;
    public float m_timeToGlide;
    public Transform m_whatToMove;


    [Header("Visible private debug")]
    public Quaternion m_startPoint;
    public float m_timeInGameStartGliding;
    public Quaternion m_endPoint;
    public float m_timeInGameEndGliding;


    public Coroutine m_currentGliding;

    [ContextMenu("Glide to position")]
    public void GlideToValueInInspector()
    {
        GlideToGivenValue(m_rotationInAngleLeftRight, m_timeToGlide);
    }
    public void GlideToGivenValue(float rotateValue, float timeToGlide)
    {
        if (m_currentGliding != null)
            StopCoroutine(m_currentGliding);
        m_currentGliding = StartCoroutine(Coroutinable_GlideToGivenValue(rotateValue, timeToGlide));
    }

    private void RefreshPathToRun(float rotateValue, float timeToGlide)
    {
        m_startPoint = m_whatToMove.rotation;
        m_timeInGameStartGliding = Time.time;

        m_endPoint = m_whatToMove.rotation * Quaternion.Euler(0, rotateValue,0);
        m_timeInGameEndGliding = Time.time + timeToGlide;
    }

    public IEnumerator Coroutinable_GlideToGivenValue(float rotateValue, float timeToGlide)
    {

        RefreshPathToRun(rotateValue, timeToGlide);
        while (Time.time <= m_timeInGameEndGliding)
        {

            if ((m_timeInGameEndGliding - m_timeInGameStartGliding) == 0)
                break;
            float percentThere = 1f - (m_timeInGameEndGliding - Time.time) / (m_timeInGameEndGliding - m_timeInGameStartGliding);
            m_whatToMove.rotation = Quaternion.Lerp(m_startPoint, m_endPoint, percentThere);
            yield return new WaitForEndOfFrame();
        }
    }

    private void Reset()
    {
        m_whatToMove = this.transform;
    }

    public override IEnumerator DoTheScratchableStuff()
    {
        return Coroutinable_GlideToGivenValue(m_rotationInAngleLeftRight, m_timeToGlide);
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        NotifyTheActionIsPossibleWihoutCoroutineButNotImplementedYet();
    }
}
