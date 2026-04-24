using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _towerPrefab;  // 배치할 타워 프리팹
    private bool _isPlacing = false;                   // 현재 배치 모드인지

    private void Update()
    {
        // T키 누르면 배치 모드 토글
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            _isPlacing = !_isPlacing;
            Debug.Log(_isPlacing ? "배치 모드 ON" : "배치 모드 OFF");
        }

        // 배치 모드일 때 마우스 클릭으로 타워 배치
        if (_isPlacing && Mouse.current.leftButton.wasPressedThisFrame)
            PlaceTower();
    }

    private void PlaceTower()
    {
        // 마우스 위치를 월드 좌표로 변환
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );
        mousePos.z = 0f;

        // 해당 위치에 타워 생성
        Instantiate(_towerPrefab, mousePos, Quaternion.identity);
        Debug.Log($"타워 배치 : {mousePos}");
    }
}