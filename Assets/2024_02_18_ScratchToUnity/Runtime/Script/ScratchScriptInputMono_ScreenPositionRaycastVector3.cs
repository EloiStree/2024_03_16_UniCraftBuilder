using UnityEngine;

public class ScratchScriptInputMono_ScreenPositionRaycastVector3 : AbstractScratchMono_VariableHolderVector3
{
    public Camera m_cameraToUse;
    public LayerMask m_ableToInteractWith = ~1;
    public Transform m_positionIfNoHit;
    public override Vector3 GetVector3()
    {
        Vector3 screenPosition = Input.mousePosition;
        if (Input.touchCount > 0)
            screenPosition = Input.touches[0].position;

        Ray ray = m_cameraToUse.ScreenPointToRay(screenPosition, Camera.MonoOrStereoscopicEye.Mono);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, m_ableToInteractWith, QueryTriggerInteraction.Ignore))
        {

            return hit.point;
        }
        if (m_positionIfNoHit)
            return m_positionIfNoHit.position;
        return Vector3.zero;


    }

    private void Reset()
    {
        m_cameraToUse = Camera.main;
    }
}