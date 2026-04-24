using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int _damage = 1; // 적 1마리당 기지 체력 감소량

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 적이 기지에 닿으면
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy == null) return;

        // 기지 체력 감소
        GameManager.Instance.TakeDamage(_damage);
        //Debug.Log("으억");
        // 적 제거 (WaveManager에 사망 알림)
        enemy.ReachBase();
    }
}