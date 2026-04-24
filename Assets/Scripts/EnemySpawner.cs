using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab; // 생성할 적 프리팹
    [SerializeField] private WaypointManager _waypointManager; // 웨이포인트 매니저 참조
    [SerializeField] private WaveManager _waveManager; // WaveManager 참조 추가

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(_enemyPrefab);
        // WaveManager도 같이 전달
        enemy.GetComponent<Enemy>().Init(
            _waypointManager.GetWaypoints(),
            _waveManager
        );
    }
}