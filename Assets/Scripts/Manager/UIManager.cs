using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // 싱글톤

    [SerializeField] private TMP_Text _goldText;  // 골드 텍스트
    [SerializeField] private TMP_Text _waveText;  // 웨이브 텍스트
    [SerializeField] private TMP_Text _hpText;    // 기지 체력 텍스트

    private void Awake()
    {
        // 싱글톤 설정
        Instance = this;
    }

    // 골드 UI 갱신
    public void UpdateGold(int gold)
    {
        _goldText.text = $"Gold : {gold}";
    }

    // 웨이브 UI 갱신
    public void UpdateWave(int wave)
    {
        _waveText.text = $"Wave : {wave}";
    }

    // 기지 체력 UI 갱신
    public void UpdateHp(int hp)
    {
        _hpText.text = $"HP : {hp}";
    }
}