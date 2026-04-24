using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;  // 적 스포너 참조
    [SerializeField] private int _enemyCountPerWave = 5;  // 웨이브당 적 수
    [SerializeField] private float _spawnInterval = 1f;   // 적 생성 간격
    [SerializeField] private float _waveCooldown = 3f;    // 웨이브 사이 대기 시간

    private int _currentWave = 0;   // 현재 웨이브 번호
    private int _aliveEnemyCount;   // 현재 살아있는 적 수

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        while (true)
        {
            _currentWave++;
            _aliveEnemyCount = _enemyCountPerWave;
            Debug.Log($"{_currentWave} 웨이브 시작");

            // 웨이브당 적 순서대로 생성
            for (int i = 0; i < _enemyCountPerWave; i++)
            {
                _enemySpawner.SpawnEnemy();
                yield return new WaitForSeconds(_spawnInterval);
            }

            // 모든 적이 죽을 때까지 대기
            yield return new WaitUntil(() => _aliveEnemyCount <= 0);

            Debug.Log($"{_currentWave} 웨이브 종료, {_waveCooldown}초 후 다음 웨이브");
            yield return new WaitForSeconds(_waveCooldown);
        }
    }

    // 적이 죽을 때 호출
    public void OnEnemyDied()
    {
        _aliveEnemyCount--;
    }
}