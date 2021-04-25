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
                    var currentPlayer = players[i];
                    var distance = Vector3.Distance(leader.transform.position, currentPlayer.transform.position);
                    if (distance >= m_followThreshold)
                    {
                        currentPlayer.transform.position = Vector3.Lerp(currentPlayer.transform.position, leader.transform.position, m_time / duration);
                        currentPlayer.transform.LookAt(leader.transform, Vector3.up);
                        currentPlayer.GetComponent<Animator>().SetInteger("State",1);
                    }
                    else
                    {
                        currentPlayer.GetComponent<Animator>().SetInteger("State", 0);
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