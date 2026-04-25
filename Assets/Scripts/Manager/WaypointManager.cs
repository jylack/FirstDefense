using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints; // 웨이포인트 배열

    // 웨이포인트 배열 반환
    public Transform[] GetWaypoints() => _waypoints;

    // 씬 뷰에서 웨이포인트 경로를 노란 선으로 시각화
    private void OnDrawGizmos()
    {
        if (_waypoints == null || _waypoints.Length < 2) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < _waypoints.Length - 1; i++)
        {
            if (_waypoints[i] != null && _waypoints[i + 1] != null)
                Gizmos.DrawLine(_waypoints[i].position, _waypoints[i + 1].position);
        }
    }
}