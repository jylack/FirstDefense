using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance; // 싱글톤

    [SerializeField] private GameObject _enemyPrefab; // 적 프리팹
    [SerializeField] private int _poolSize = 20;      // 미리 생성할 적 수
    private Queue<GameObject> _pool = new Queue<GameObject>(); // 대기 중인 적 큐

    private void Awake()
    {
        Instance = this;
        InitPool();
    }

    // 풀 초기화 - 미리 적 생성해두기
    private void InitPool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab);
            enemy.SetActive(false); // 비활성화 상태로 대기
            _pool.Enqueue(enemy);
        }
    }

    // 풀에서 적 꺼내기
    public GameObject GetEnemy()
    {
        // 풀에 대기중인 적 있으면 꺼내서 반환
        if (_pool.Count > 0)
        {
            GameObject enemy = _pool.Dequeue();
            enemy.SetActive(true);
            return enemy;
        }

        // 풀이 비어있으면 새로 생성
        GameObject newEnemy = Instantiate(_enemyPrefab);
        return newEnemy;
    }

    // 적을 풀에 반환
    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false); // 비활성화 후 대기
        _pool.Enqueue(enemy);
    }
}