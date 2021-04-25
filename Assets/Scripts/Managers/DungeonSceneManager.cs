using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class DungeonSceneManager : SceneManager
{

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