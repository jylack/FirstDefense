using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "FirstDefense/MapData")]
public class MapData : ScriptableObject
{
    [Header("맵 정보")]
    public string mapName;        // 맵 이름
    public int difficulty;        // 난이도 (1~5) - 이 값으로 맵 자동생성

    [Header("웨이브 설정")]
    public int enemyCountPerWave; // 웨이브당 적 수
    public float spawnInterval;   // 적 생성 간격
    public float waveCooldown;    // 웨이브 사이 대기 시간

    [Header("적 설정")]
    public float enemySpeed;      // 적 이동 속도
    public int enemyHp;           // 적 체력
}