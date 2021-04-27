using System.Collections.Generic;
using UnityEngine;

public class PlayerGroup : MonoBehaviour
{
    #region Public Properties

    public IReadOnlyCollection<Monster> Players => m_players;

    #endregion

    #region Private Fields

    [SerializeField]
    private List<Monster> m_players;

    [SerializeField]
    private float m_followThreshold = 3f;

    [SerializeField]
    private float m_duration = 1200f;

    private float m_time = 0.0f;

    #endregion

    #region Public Methods

    public void Initialize()
    {
        for (int i = 0; i < m_players.Count; i++)
        {
            var currentPlayer = m_players[i].Model.InstantiatePrefab(transform, true);

            if (i != 0)
            {
                currentPlayer.transform.position = m_players[i - 1].transform.position + new Vector3(-m_followThreshold, 1, 0);
            }

            m_players[i] = currentPlayer.GetComponent<Monster>();
        }
    }

    public Queue<Monster> InitializeCombatants()
    {
        return new Queue<Monster>(m_players);
    }

    #endregion

    #region Private Methods

    private void LateUpdate()
    {
        FollowDelay(m_duration);
    }

    /// <summary>
    /// Each player in the list of players will follow one another
    /// like an old school jRPG.
    /// </summary>
    /// <param name="duration"></param>
    private void FollowDelay(float duration)
    {
        if (m_players?.Count > 0)
        {
            if (m_time < duration)
            {
                // Avoid the first player in the list since it is the leader
                for (int i = 1; i < m_players.Count; i++)
                {
                    var leader = m_players[i - 1];
                    var currentPlayer = m_players[i];
                    var distance = Vector3.Distance(leader.transform.position, currentPlayer.transform.position);
                    if (distance >= m_followThreshold)
                    {
                        currentPlayer.transform.position = Vector3.Lerp(currentPlayer.transform.position, leader.transform.position, m_time / duration);
                        currentPlayer.transform.LookAt(leader.transform, Vector3.up);
                        currentPlayer.GetComponent<Animator>().SetInteger("State", 1);
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

    #endregion
}