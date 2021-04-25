using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public static GameManager Instance;

    public Transform[] spawnpoints;

    private void Awake()
    {
        Instance = this;
        Initialize();
        FindPlayer();        
    }

    private void Initialize()
    {
        if (WaypointManager.Instance == null)
        {
            GameObject waypointManager = new GameObject("WaypointManager");
            waypointManager.transform.parent = this.transform;
            waypointManager.AddComponent<WaypointManager>();
        }
    }

    private void FindPlayer()
    {
        player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogErrorFormat("{0}: No Player found in the scene.", name);
        }

        if (spawnpoints != null && spawnpoints.Length > 0)
        {
            SpawnPlayer(spawnpoints[0]);
        }
    }

    public void SpawnPlayer(Transform spawnPoint)
    {
        player.transform.position = spawnPoint.position;
        player.transform.localRotation = spawnPoint.localRotation;
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
    }
}
