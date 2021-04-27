using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    #region Private Fields

    [SerializeField]
    private Monster[] m_monsters;

    #endregion

    #region Public Methods    

    /// <summary>
    /// Monsters should be added to a room and their gameobjects disabled
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task SetupRoomAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        if (m_monsters?.Length > 0)
        {
            // Spawn the monsters in a room
            for (int i = 0; i < m_monsters.Length; i++)
            {
                m_monsters[i].gameObject.SetActive(true);
            }
        }
        else
        {
            return Task.FromException(new Exception("Failed to initialize room set-up."));
        }

        return Task.CompletedTask;
    }

    public async Task RoomClearAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        gameObject.SetActive(false);

        await Task.CompletedTask;
    }

    public Queue<Monster> InitializeCombatants()
    {
        return new Queue<Monster>(m_monsters);
    }

    #endregion

}
