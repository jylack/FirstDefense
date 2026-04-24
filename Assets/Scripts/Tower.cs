using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _range = 3f;    // 공격 사거리
    [SerializeField] private int _damage = 10;      // 공격 데미지
    [SerializeField] private float _fireRate = 1f;  // 초당 공격 횟수
    private float _fireCooldown = 0f;               // 현재 쿨타임
    private Enemy _targetEnemy;                     // 현재 타겟 적

    private void Update()
    {
        // 쿨타임 감소
        _fireCooldown -= Time.deltaTime;

        // 사거리 안에서 가장 가까운 적 탐색
        FindTarget();

        // 타겟 있고 쿨타임 끝나면 공격
        if (_targetEnemy != null && _fireCooldown <= 0f)
            Attack();
    }

    private void FindTarget()
    {
        // 사거리 안의 모든 콜라이더 탐색
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _range);

        float closestDistance = float.MaxValue;
        _targetEnemy = null;

        foreach (Collider2D hit in hits)
        {
            // Enemy 컴포넌트 있는 오브젝트만 타겟으로
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
        // 타겟에게 데미지 주고 쿨타임 초기화
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