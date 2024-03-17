using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUntilCameraHaveAllInViewPortMono : MonoBehaviour
{
    public float m_rotationAngle = 45;
    public MeshRenderer[] m_meshRenderer;
    public Camera m_cameraToMove;

    public List<Vector3> m_worldPoints = new List<Vector3>();
    public float m_radiusMultiplicator = 1.1f;

    [ContextMenu("Refresh")]
    public void Refresh() {
        SetMeshRenderer(m_meshRenderer);
    }

    public void SetMeshRenderer(MeshRenderer[] renderer) {

        if (renderer == null || renderer.Length==0)
            return;
        Transform camDirection = m_cameraToMove.transform;
        m_worldPoints.Clear();
        m_meshRenderer = renderer;

        
        camDirection.rotation = Quaternion.Euler(m_rotationAngle, 0, 0);
        foreach (var item in renderer)
        {
            BoxCollider collider = item.gameObject.GetComponent<BoxCollider>();
            if (null== collider)
                collider = item.gameObject.AddComponent<BoxCollider>();
            m_worldPoints.AddRange(GetColliderCorners(collider));
          }

        camDirection.position = CalculateMiddlePoint(m_worldPoints);
        //Eloi.E_DrawingUtility.DrawLines(5, Color.red, m_worldPoints.ToArray());

        bool isOneOutOfView = true;
        int antiLoop = 100000;
        while (isOneOutOfView) {
            camDirection.position += camDirection.forward * -0.1f;
            isOneOutOfView = false;
            for (int i = 0; i < m_worldPoints.Count; i++)
            {
                if(!IsPointInView(m_worldPoints[i], m_cameraToMove))
                 {
                    isOneOutOfView = true;
                    break;
                }
            }
            antiLoop--;
            if (antiLoop < 0)
                break;
        }

    } // Function to calculate the middle point of a list of world points
    public Vector3 CalculateMiddlePoint(List<Vector3> worldPoints)
    {
        // Ensure there are points to calculate
        if (worldPoints == null || worldPoints.Count == 0)
        {
            Debug.LogError("No points provided to calculate the middle point.");
            return Vector3.zero;
        }

        Vector3 sum = Vector3.zero;

        // Sum all world points
        foreach (Vector3 point in worldPoints)
        {
            sum += point;
        }

        // Calculate average position
        Vector3 middlePoint = sum / worldPoints.Count;

        return middlePoint;
    }
    public float m_paddingPercent=0.05f;
    public  bool IsPointInView(Vector3 point, Camera camera)
    {
        Vector3 viewportPoint = camera.WorldToViewportPoint(point);
        return viewportPoint.x >= (0f+m_paddingPercent) && viewportPoint.x <= 1- m_paddingPercent && viewportPoint.y >= 0 + m_paddingPercent && viewportPoint.y <= 1- m_paddingPercent && viewportPoint.z > 0;
    }
    Vector3[] GetColliderCorners(BoxCollider collider)
    {
        Vector3[] corners = new Vector3[8];

        // Get the local corners in the collider's local space
        Vector3[] localCorners = new Vector3[8];
        Vector3 center = collider.center;
        Vector3 extents = collider.size * 0.5f;

        localCorners[0] = center + new Vector3(extents.x, extents.y, extents.z);
        localCorners[1] = center + new Vector3(extents.x, extents.y, -extents.z);
        localCorners[2] = center + new Vector3(extents.x, -extents.y, extents.z);
        localCorners[3] = center + new Vector3(extents.x, -extents.y, -extents.z);
        localCorners[4] = center + new Vector3(-extents.x, extents.y, extents.z);
        localCorners[5] = center + new Vector3(-extents.x, extents.y, -extents.z);
        localCorners[6] = center + new Vector3(-extents.x, -extents.y, extents.z);
        localCorners[7] = center + new Vector3(-extents.x, -extents.y, -extents.z);

        // Transform local corners to world space
        for (int i = 0; i < 8; i++)
        {
            corners[i] = collider.transform.TransformPoint(localCorners[i]);
        }

        return corners;
    }
}