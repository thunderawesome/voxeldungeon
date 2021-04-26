using Runtime.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PlayerGroup))]
public class DungeonSceneManager : SceneManager
{
    #region Public Properties

    public IReadOnlyCollection<DungeonModel> Dungeons => m_dungeons;

    #endregion

    #region Private Fields

    [SerializeField]
    private DungeonModel[] m_dungeons;

    private DungeonModel m_currentDungeon;

    private PlayerGroup m_playerGroup;

    private TextMeshPro m_textMesh;

    #endregion

    #region Private Methods

    private async void Awake()
    {
        using (var tokenSource = new UnityTokenSource())
        {
            var token = tokenSource.Token;
            await SetupSceneAsync(token, out RoomManager currentRoom);
            await currentRoom?.SetupRoomAsync(token);

            m_textMesh = GetComponentInChildren<TextMeshPro>();

            m_playerGroup = gameObject.GetComponent<PlayerGroup>();
            m_playerGroup.Initialize();

            await RunIntroSequenceAsync(token);
            await CombatSequenceAsync(currentRoom, token);

            Debug.Log("COMPLETE!");
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

    protected override async Task RunIntroSequenceAsync(CancellationToken token)
    {
        await base.RunIntroSequenceAsync(token);

        m_textMesh.text = "3";
        await Task.Delay(TimeSpan.FromSeconds(1));
        m_textMesh.text = "2";
        await Task.Delay(TimeSpan.FromSeconds(1));
        m_textMesh.text = "1";
        await Task.Delay(TimeSpan.FromSeconds(1));
        m_textMesh.text = "FIGHT!";
        await Task.Delay(TimeSpan.FromSeconds(1));
        m_textMesh.text = string.Empty;
    }

    protected async Task CombatSequenceAsync(RoomManager currentRoom, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        await currentRoom.RoomClearAsync(token);
    }

    protected override Task ShowResultsAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return Task.CompletedTask;
    }

    #endregion
}