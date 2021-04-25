using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public GameObject[] waypoints;

    public static WaypointManager Instance;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
        Initialize();
    }

    private void Initialize()
    {
        if (transform.childCount > 0)
        {
            waypoints = transform.GetComponentsOnlyInChildren<GameObject>();

            if (waypoints == null || waypoints.Length <= 0)
            {
                var transforms = transform.GetComponentsOnlyInChildren<Transform>();
                waypoints = new GameObject[transforms.Length];
                for (int i = 0; i < transforms.Length; i++)
                {
                    waypoints[i] = transforms[i].gameObject;
                }
            }
        }
        else
        {
            waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

            if (waypoints == null || waypoints.Length <= 0)
            {
                Debug.LogErrorFormat("{0}: No Waypoints found in the scene.", name);
                return;
            }

            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i].transform.parent = transform;
            }
        }
    }

    public GameObject GetRandomWaypoint()
    {
        return waypoints[Random.Range(0, waypoints.Length - 1)];
    }
}
