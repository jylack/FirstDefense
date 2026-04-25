using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WaveManager _waveManager;    // 웨이브 매니저 참조
    [SerializeField] private MapData _mapData;            // 맵 데이터 참조

    public void SpawnEnemy(SpawnRoute route)
    {
        // 풀에서 적 꺼내기
        GameObject enemyObj = ObjectPool.Instance.GetEnemy();
        Enemy enemy = enemyObj.GetComponent<Enemy>();

        // 스폰 루트 기반으로 웨이포인트 변환
        Transform[] waypoints = CreateWaypoints(route);

        // 적 초기화 (속도, 체력도 맵데이터에서 받아오기)
        enemy.Init(waypoints, _waveManager, _mapData.enemySpeed, _mapData.enemyHp);
    }

    private Transform[] CreateWaypoints(SpawnRoute route)
    {
        // Vector2 배열을 Transform 배열로 변환
        Transform[] waypoints = new Transform[route.waypointPositions.Length];
        for (int i = 0; i < route.waypointPositions.Length; i++)
        {
            GameObject wp = new GameObject($"WP_{i}");
            wp.transform.position = route.waypointPositions[i];
            waypoints[i] = wp.transform;
        }
        return waypoints;
    }
}