using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    #region Private Fields

    [SerializeField]
    private GameObject[] m_monsters;

    #endregion

    #region Protected Methods    

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
                m_monsters[i].SetActive(true);
            }
        }
        else
        {
            return Task.FromException(new Exception("Failed to initialize room set-up."));
        }

        return Task.CompletedTask;
    }

    public Task RoomClearAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return Task.CompletedTask;
    }

    #endregion

}
