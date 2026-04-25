using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance; // 싱글톤

    [SerializeField] private int _difficulty = 1; // 난이도 (1~5)

    // 생성된 맵 데이터
    public SpawnRoute[] SpawnRoutes { get; private set; }

    private void Awake()
    {
        Instance = this;
        GenerateMap(_difficulty);
    }

    public void GenerateMap(int difficulty)
    {
        // 난이도에 따라 스폰 수 결정 (난이도 = 스폰 수)
        int spawnCount = difficulty;
        SpawnRoutes = new SpawnRoute[spawnCount];

        for (int i = 0; i < spawnCount; i++)
        {
            // 스폰 위치는 왼쪽에서 위아래로 퍼지게
            Vector2 spawnPos = new Vector2(-6f, i % 2 == 0 ? i * 1.5f : -i * 1.5f);

            // 해당 스폰에서 기지까지 경로 자동 생성
            Vector2[] waypoints = GenerateWaypoints(spawnPos, difficulty);

            SpawnRoutes[i] = new SpawnRoute
            {
                spawnPosition = spawnPos,
                waypointPositions = waypoints
            };
        }

        Debug.Log($"난이도 {difficulty} 맵 생성 완료 - 스폰 {spawnCount}개");
    }

    private Vector2[] GenerateWaypoints(Vector2 spawnPos, int difficulty)
    {
        // 난이도 높을수록 꺾이는 횟수 증가
        int turnCount = difficulty + 1;
        Vector2[] waypoints = new Vector2[turnCount + 2]; // 시작 + 중간 + 끝

        // 시작점
        waypoints[0] = spawnPos;

        // 중간 경유지 자동 생성
        float xStep = 8f / (turnCount + 1); // x축 간격
        for (int i = 1; i <= turnCount; i++)
        {
            float x = spawnPos.x + xStep * i;
            // 짝수 인덱스는 위, 홀수는 아래로 꺾임
            float y = i % 2 == 0 ? spawnPos.y + 2f : spawnPos.y - 2f;
            waypoints[i] = new Vector2(x, y);
        }

        // 기지 위치 (오른쪽 끝)
        waypoints[turnCount + 1] = new Vector2(4f, 0f);

        return waypoints;
    }
}

[System.Serializable]
public class SpawnRoute
{
    public Vector2 spawnPosition;       // 스폰 위치
    public Vector2[] waypointPositions; // 해당 스폰 전용 경로
}