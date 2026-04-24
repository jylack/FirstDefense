using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _towerPrefab;  // 배치할 타워 프리팹
    [SerializeField] private int _towerCost = 50; // 타워 배치 비용

    private bool _isPlacing = false;                   // 현재 배치 모드인지
    private GameObject _previewTower;                  // 미리보기 타워 오브젝트

    private void Update()
    {
        // T키 누르면 배치 모드 토글
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            _isPlacing = !_isPlacing;

            if (_isPlacing)
                ShowPreview();  // 배치 모드 ON → 미리보기 생성
            else
                HidePreview();  // 배치 모드 OFF → 미리보기 제거
        }

        // 배치 모드일 때
        if (_isPlacing)
        {
            MovePreview();  // 미리보기 마우스 따라 이동

            // 클릭하면 타워 배치
            if (Mouse.current.leftButton.wasPressedThisFrame)
                PlaceTower();
        }
    }

    private void ShowPreview()
    {
        // 반투명 미리보기 타워 생성
        _previewTower = Instantiate(_towerPrefab);
        SpriteRenderer sr = _previewTower.GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, 0.5f); // 반투명 처리

        // 미리보기는 타워 기능 비활성화
        _previewTower.GetComponent<Tower>().enabled = false;
    }

    private void HidePreview()
    {
        // 미리보기 타워 제거
        if (_previewTower != null)
            Destroy(_previewTower);
    }

    private void MovePreview()
    {
        if (_previewTower == null) return;

        // 미리보기 타워를 마우스 위치로 이동
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );
        mousePos.z = 0f;
        _previewTower.transform.position = mousePos;
    }

    private void PlaceTower()
    {
        // 골드 부족하면 배치 안됨
        if (!GameManager.Instance.SpendGold(_towerCost)) return;
        // 미리보기 위치에 실제 타워 배치
        Instantiate(_towerPrefab, _previewTower.transform.position, Quaternion.identity);
    }
}