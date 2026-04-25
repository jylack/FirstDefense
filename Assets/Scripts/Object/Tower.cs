using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _range = 3f;     // 공격 사거리
    [SerializeField] private int _damage = 10;       // 공격 데미지
    [SerializeField] private float _fireRate = 1f;   // 초당 공격 횟수
    private float _fireCooldown = 0f;                // 현재 쿨타임
    private Enemy _targetEnemy;                      // 현재 타겟 적
    private float _findTargetInterval = 0.5f;        // 타겟 탐색 주기 (0.5초마다)
    private float _findTargetTimer = 0f;             // 타겟 탐색 타이머

    private void Update()
    {
        _fireCooldown -= Time.deltaTime;
        _findTargetTimer -= Time.deltaTime;

        // 0.5초마다만 타겟 탐색 (매 프레임 탐색 X)
        if (_findTargetTimer <= 0f)
        {
            FindTarget();
            _findTargetTimer = _findTargetInterval;
        }

        // 타겟 있고 쿨타임 끝나면 공격
        if (_targetEnemy != null && _fireCooldown <= 0f)
            Attack();
    }

    private void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _range);

        float closestDistance = float.MaxValue;
        _targetEnemy = null;

        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy == null) continue;

            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                _targetEnemy = enemy;
            }
        }
    }

    private void Attack()
    {
        _targetEnemy.TakeDamage(_damage);
        _fireCooldown = 1f / _fireRate;
    }

    // 씬 뷰에서 사거리 시각화
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}