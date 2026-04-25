using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;  // 적 스포너 참조
    [SerializeField] private MapData _mapData;            // 맵 데이터 참조
    private int _currentWave = 0;                         // 현재 웨이브 번호
    private int _aliveEnemyCount;                         // 살아있는 적 수

    private void Start()
    {
        // MapGenerator에 난이도 전달해서 맵 생성
        MapGenerator.Instance.GenerateMap(_mapData.difficulty);
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        while (true)
        {
            _currentWave++;
            _aliveEnemyCount = _mapData.enemyCountPerWave;
            GameManager.Instance.UpdateWave(_currentWave);
            Debug.Log($"{_currentWave} 웨이브 시작");

            // 웨이브당 적 순서대로 생성
            for (int i = 0; i < _mapData.enemyCountPerWave; i++)
            {
                // 스폰 루트 중 랜덤으로 선택해서 생성
                int routeIndex = Random.Range(0, MapGenerator.Instance.SpawnRoutes.Length);
                _enemySpawner.SpawnEnemy(MapGenerator.Instance.SpawnRoutes[routeIndex]);
                yield return new WaitForSeconds(_mapData.spawnInterval);
            }

            // 모든 적이 죽을 때까지 대기
            yield return new WaitUntil(() => _aliveEnemyCount <= 0);

            Debug.Log($"{_currentWave} 웨이브 종료, {_mapData.waveCooldown}초 후 다음 웨이브");
            yield return new WaitForSeconds(_mapData.waveCooldown);
        }
    }

    public void OnEnemyDied()
    {
        _aliveEnemyCount--;
    }
}