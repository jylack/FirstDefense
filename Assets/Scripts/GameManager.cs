using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤

    [SerializeField] private int _startGold = 100;  // 시작 골드
    [SerializeField] private int _startHp = 10;     // 시작 기지 체력
    private int _currentGold;                        // 현재 골드
    private int _currentHp;                          // 현재 기지 체력

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // 초기값 설정 및 UI 갱신
        _currentGold = _startGold;
        _currentHp = _startHp;
        UpdateUI();
    }

    // 골드 추가
    public void AddGold(int amount)
    {
        _currentGold += amount;
        UIManager.Instance.UpdateGold(_currentGold);
    }

    // 골드 소모 (성공 여부 반환)
    public bool SpendGold(int amount)
    {
        if (_currentGold < amount)
        {
            Debug.Log("골드 부족!");
            return false;
        }
        _currentGold -= amount;
        UIManager.Instance.UpdateGold(_currentGold);
        return true;
    }

    // 기지 체력 감소
    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        UIManager.Instance.UpdateHp(_currentHp);

        if (_currentHp <= 0)
            GameOver();
    }

    // 웨이브 UI 갱신
    public void UpdateWave(int wave)
    {
        UIManager.Instance.UpdateWave(wave);
    }

    // 게임 오버
    private void GameOver()
    {
        Debug.Log("게임 오버!");
        Time.timeScale = 0f; // 게임 멈추기
    }

    // 전체 UI 갱신
    private void UpdateUI()
    {
        UIManager.Instance.UpdateGold(_currentGold);
        UIManager.Instance.UpdateHp(_currentHp);
    }
}