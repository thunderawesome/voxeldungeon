using Runtime.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PlayerGroup))]
[RequireComponent(typeof(CombatManager))]
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

    private CombatManager m_combatManager;

    #endregion

    #region Private Methods

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

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
            m_combatManager = GetComponent<CombatManager>();
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

        m_combatManager.team1 = m_playerGroup.InitializeCombatants();
        m_combatManager.team2 = currentRoom.InitializeCombatants();

        var combatTask = m_combatManager.RunCombatSequenceAsync(token);
        await combatTask;

        if (combatTask.Result == CombatState.Win)
        {
            m_textMesh.text = "You Win!";
            // only show this if the player wins
            await currentRoom.RoomClearAsync(token);
        }
        else if(combatTask.Result == CombatState.Lose)
        {
            m_textMesh.text = "You lose...";
        }
        await ShowResultsAsync(token);
    }

    protected override Task ShowResultsAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return Task.CompletedTask;
    }

    #endregion
}