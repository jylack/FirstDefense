using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab; // 생성할 적 프리팹
    [SerializeField] private WaypointManager _waypointManager; // 웨이포인트 매니저 참조

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        // 적 오브젝트 생성
        GameObject enemy = Instantiate(_enemyPrefab);

        // 웨이포인트 배열 전달해서 이동 시작
        enemy.GetComponent<Enemy>().Init(_waypointManager.GetWaypoints());
    }
}