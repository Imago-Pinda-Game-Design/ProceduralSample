using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject CircleGameObject;
    public GameObject SquareGameObject;

    public int MinCircles;
    public int MaxCircles;

    public float MaxPlatformHeightDistance;

    void Awake()
    {
        GenerateLevel(SquareGameObject, CircleGameObject, Random.Range(MinCircles, MaxCircles), 25, 10);
    }

    void GenerateLevel(GameObject prefabA, GameObject prefabB, int total, float width, float height)
    {
        // Random.InitState(0);

        float platformHeight = Random.Range(-height / 2, height / 2);
        GameObject prefab;

        int cooldown = 0;
        int invertedCount = 0;

        for (int i = 0; i < total; i++)
        {
            prefab = prefabA;

            if (invertedCount > 0) {
                prefab = prefabB;
                invertedCount--;
            }
            else if (cooldown <= 0 && Random.value < 0.5)
            {
                prefab = prefabB;
                cooldown = 7;
                invertedCount = Random.Range(1, 3);
            }

            cooldown--;

            float distance = width / total;
            float platformNewHeight = platformHeight + Random.Range(
                -MaxPlatformHeightDistance / 2,
                MaxPlatformHeightDistance / 2
            );

            Vector2 position = new Vector2(distance * i, platformNewHeight);
            Instantiate(prefab, position, Quaternion.identity);

            platformHeight = platformNewHeight;
        }
    }
}
