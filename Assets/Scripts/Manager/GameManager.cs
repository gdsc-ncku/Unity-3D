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
    [SerializeField] AssetReference mainScene, advenatureScene;
    [SerializeField] GameObject[] Enemys;
    // Start is called before the first frame update
    void Start()
    {
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
        gameStatus.LoadingSceneHandle = Addressables.LoadSceneAsync(mainScene, LoadSceneMode.Additive);
    }

    public void GameStart()
    {
        gameStatus.Level++;
    }

    public void GameReset()
    {
        gameStatus.Level = 0;
    }

    public void LevelChoose()
    {
        if(gameStatus.Level == 1)
        {
            level1();
        }
    }

    public void level1()
    {
        StartCoroutine(SpawnRandomOnNavMesh());
    }
    IEnumerator SpawnRandomOnNavMesh()
    {
        for (int i = 0; i < level1EnemyNum; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            if (randomPosition != Vector3.zero)
            {
                GameObject instance = Instantiate(Enemys[Random.Range(0, Enemys.Length)], randomPosition, Quaternion.identity);
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

            yield return null;
        }
    }

    Vector3 GetRandomPosition()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * radius;
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
