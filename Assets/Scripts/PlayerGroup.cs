using UnityEngine;

public class PlayerGroup : MonoBehaviour
{
    public GameObject[] players;

    [SerializeField]
    private float m_followThreshold = 1f;

    [SerializeField]
    private float m_duration = 5f;

    private float m_time = 0.0f;

    private void LateUpdate()
    {
        FollowDelay(m_duration);
    }

    private void FollowDelay(float duration)
    {
        if (players?.Length > 0)
        {      
            if (m_time < duration)
            {      
                // Avoid the first player in the list since it is the leader
                for (int i = 1; i < players.Length; i++)
                {
                    var leader = players[i - 1];
                    var distance = Vector3.Distance(leader.transform.position, players[i].transform.position);
                    if (distance >= m_followThreshold)
                    {
                        players[i].transform.position = Vector3.Lerp(players[i].transform.position, leader.transform.position, m_time / duration);
                        players[i].transform.LookAt(leader.transform, Vector3.up);
                    }
                }

                m_time += Time.deltaTime;
            }
            else
            {
                m_time = 0;
            }
        }
    }
}
