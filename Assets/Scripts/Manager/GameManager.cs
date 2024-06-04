using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int maxAttempts = 100, radius = 500;
    [SerializeField] int level1EnemyNum = 100;
    [SerializeField] GameStatus gameStatus;
    [SerializeField] GameObject[] Enemys;
    // Start is called before the first frame update
    void Start()
    {
        gameStatus.GenerateSpeed = 20f - (1 << (gameStatus.Level - 1)) + 1;
        transform.position = Vector3.zero;
        gameStatus.settingLevel.AddListener(() => LevelChoose());
        StartCoroutine(WaitForLoadingMainScene());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteKey("IsFirstTime");
            Debug.Log("Reset");
        }
    }

    IEnumerator WaitForLoadingMainScene()
    {
        yield return new WaitUntil(() => gameStatus.CatchData);
        gameStatus.LoadOtherScene(false, gameStatus.mainScene);
    }

    public void LevelChoose()
    {
        if(gameStatus.Level != 0)
        {
            level1();
        }
    }

    public void level1()
    {
        gameStatus.Duration = 600 + (gameStatus.Level - 1 ) * 60;
        StartCoroutine(SpawnRandomOnNavMesh());
    }
    IEnumerator SpawnRandomOnNavMesh()
    {
        while(gameStatus.RemainingDuration > 0)
        {
            Vector3 randomPosition = GetRandomPosition();
            if (randomPosition != Vector3.zero)
            {
                GameObject instance = Instantiate(Enemys[UnityEngine.Random.Range(0, Enemys.Length)], randomPosition, Quaternion.identity);
                NavMeshAgent agent = instance.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    Vector3 targetPosition = GetRandomPosition();
                    if (targetPosition != Vector3.zero)
                    {
                        agent.SetDestination(targetPosition);
                    }
                }
            }
            float waitTime = Math.Max(gameStatus.GenerateSpeed, 0.5f);
            yield return new WaitForSeconds(waitTime);
            gameStatus.RemainingDuration -= waitTime;
        }
    }

    Vector3 GetRandomPosition()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomPoint = transform.position + UnityEngine.Random.insideUnitSphere * radius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        Debug.LogWarning("Could not find a valid position on the NavMesh");
        return Vector3.zero;
    }
}
