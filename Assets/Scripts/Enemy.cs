using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3f; // 이동 속도
    [SerializeField] private int _maxHp = 100; // 최대 체력
    private int _currentHp; // 현재 체력
    private Transform[] _waypoints; // 웨이포인트 배열
    private int _currentIndex = 0; // 현재 목표 웨이포인트 인덱스

    // 외부에서 웨이포인트 배열 받아 초기화
    public void Init(Transform[] waypoints)
    {
        _waypoints = waypoints;
        _currentHp = _maxHp; // 체력 초기화
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
        Debug.Log($"적 체력 : {_currentHp}");

        if (_currentHp <= 0)
            Die();
    }

    // 적 사망 처리
    private void Die()
    {
        Debug.Log("적 사망");
        Destroy(gameObject);
    }
}