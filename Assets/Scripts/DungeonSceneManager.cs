﻿using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class DungeonSceneManager : SceneManager
{
    #region Properties

    protected int DifficultyLevel { get { return m_currentDifficultyLevel; } }

    public int MaxDifficultyLevel { get { return m_maxDifficultyLevel; } }

    protected float DifficultyFraction => DifficultyLevel / (float)m_maxDifficultyLevel;

    #endregion

    #region Private Fields

    [Header("Components")]

    [SerializeField]
    private AudioClip m_music = null;

    [SerializeField]
    private AudioClip m_resultsMusic = null;  


    [SerializeField]
    private int m_maxDifficultyLevel = 5;

    private int m_currentDifficultyLevel = 1;
       
    #endregion

    #region Protected Methods    

    protected override Task SetupSceneAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

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