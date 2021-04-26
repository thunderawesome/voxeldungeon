using Runtime.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(PlayerGroup))]
public class DungeonSceneManager : SceneManager
{
    #region Public Properties

    public IReadOnlyCollection<Dungeon> Dungeons => m_dungeons;

    #endregion

    #region Private Fields

    [SerializeField]
    private Dungeon[] m_dungeons;

    private Dungeon m_currentDungeon;

    private PlayerGroup m_playerGroup;

    #endregion

    #region Private Methods

    private async void Awake()
    {
        using (var tokenSource = new UnityTokenSource())
        {
            var token = tokenSource.Token;
            await SetupSceneAsync(token, out RoomManager currentRoom);
            await currentRoom?.SetupRoomAsync(token);

            m_playerGroup = gameObject.GetComponent<PlayerGroup>();
            m_playerGroup.Initialize();
        }
    }

    #endregion

    #region Protected Methods    

    protected Task SetupSceneAsync(CancellationToken token, out RoomManager currentRoom)
    {
        token.ThrowIfCancellationRequested();

        currentRoom = null;

        if (m_dungeons?.Length > 0)
        {
            // Create the Dungeon Instance
            for (int i = 0; i < m_dungeons.Length; i++)
            {
                if (m_dungeons[i].IsActive)
                {
                    m_currentDungeon = m_dungeons[i];
                    foreach (var room in m_currentDungeon.Rooms)
                    {
                        if (room.IsActive)
                        {
                            var roomInstance = room.InstantiatePrefab(transform, true);
                            currentRoom = roomInstance.GetComponent<RoomManager>();
                            // Skip out of the loop since we don't want to load all the dungeons at once.
                            return Task.CompletedTask;
                        }
                    }
                }
            }
        }
        return Task.FromException(new Exception("Failed to initialize dungeon set-up."));
    }

    protected override Task RunIntroSequenceAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return Task.CompletedTask;
    }

    protected override Task ShowResultsAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return Task.CompletedTask;
    }

    #endregion
}