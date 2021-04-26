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
            await SetupSceneAsync(tokenSource.Token);
        }
    }

    #endregion

    #region Protected Methods    

    protected override Task SetupSceneAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        if (m_dungeons?.Length > 0)
        {
            // Create the Dungeon Instance
            for (int i = 0; i < m_dungeons.Length; i++)
            {
                if (m_dungeons[i].IsActive)
                {
                    foreach (var room in m_dungeons[i].Rooms)
                    {
                        if (room.IsActive)
                        {
                            room.InstantiatePrefab(transform, true);
                        }
                    }

                    m_playerGroup = gameObject.GetComponent<PlayerGroup>();
                    m_playerGroup.Initialize();

                    m_currentDungeon = m_dungeons[i];
                    // Skip out of the loop since we don't want to load all the dungeons at once.
                    break;
                }
            }
        }
        else
        {
            return Task.FromException(new Exception("Failed to initialize dungeon set-up."));
        }

        return Task.CompletedTask;
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