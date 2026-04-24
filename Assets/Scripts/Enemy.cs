using UnityEngine;

public class Enemy : MonoBehaviour
{          
    [SerializeField] private float _speed = 3f; // 이동 속도
    [SerializeField] private int _maxHp = 100; // 최대 체력
    [SerializeField] private int _goldReward = 10; // 처치 시 골드 보상

    private WaveManager _waveManager; // WaveManager 참조
    
    private int _currentHp; // 현재 체력
    private Transform[] _waypoints; // 웨이포인트 배열
    private int _currentIndex = 0; // 현재 목표 웨이포인트 인덱스


    // 외부에서 웨이포인트 배열 받아 초기화
    // Init 메서드에 WaveManager 추가
    public void Init(Transform[] waypoints, WaveManager waveManager)
    {
        _waypoints = waypoints;
        _waveManager = waveManager;
        _currentHp = _maxHp;
        transform.position = _waypoints[0].position;
    }

    private void Update()
    {
        if (_waypoints == null || _currentIndex >= _waypoints.Length) return;
        MoveToNextWaypoint();
    }

    
    private void MoveToNextWaypoint()
    {
        Transform target = _waypoints[_currentIndex];
        float distance = Vector2.Distance(transform.position, target.position);

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            _speed * Time.deltaTime
        );

        if (distance < 0.1f)
            _currentIndex++;
    }

    // 데미지 받기
    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        //Debug.Log($"적 체력 : {_currentHp}");

        if (_currentHp <= 0)
            Die();
    }

    // 적 사망 처리
    private void Die()
    {
        GameManager.Instance.AddGold(_goldReward); // 골드 지급
        // WaveManager에게 적 사망 알림
        _waveManager.OnEnemyDied();
        ObjectPool.Instance.ReturnEnemy(gameObject);//풀에 반환
    }

    // 기지 도달 시 호출
    public void ReachBase()
    {
        _waveManager.OnEnemyDied();
        ObjectPool.Instance.ReturnEnemy(gameObject);//풀에 반환
    }
}