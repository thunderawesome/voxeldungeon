using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Queue<Monster> team1;
    public Queue<Monster> team2;

    public async Task<CombatState> RunCombatSequenceAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        // Loop continuously until either Team1 or Team2 are defeated
        while (true)
        {
            await Task.Yield();  

            Monster team1Combatant = team1.Count > 0 ? team1.Dequeue() : null;
            Monster team2Combatant = team2.Count > 0 ? team2.Dequeue() : null;            

            await Task.Delay(TimeSpan.FromSeconds(1));

            team1Combatant?.TakeDamage(team2Combatant.Damage);
            if (team1Combatant?.IsDefeated == false)
            {
                team1.Enqueue(team1Combatant);

                await Task.Delay(TimeSpan.FromSeconds(.1f));
                team1Combatant.gameObject.GetComponentInChildren<Renderer>().material.color = team1Combatant.originalColor;
            }

            await Task.Delay(TimeSpan.FromSeconds(1));

            team2Combatant?.TakeDamage(team1Combatant.Damage);
            if (team2Combatant?.IsDefeated == false)
            {
                team2.Enqueue(team2Combatant);

                await Task.Delay(TimeSpan.FromSeconds(.1f));
                team2Combatant.gameObject.GetComponentInChildren<Renderer>().material.color = team2Combatant.originalColor;
            }

            var isTeamOneDefeated = team1.Count <= 0;
            if (isTeamOneDefeated)
            {
                return await Task.FromResult<CombatState>(CombatState.Lose);
            }

            var isTeamTwoDefeated = team2.Count <= 0;
            if (isTeamTwoDefeated)
            {
                return await Task.FromResult<CombatState>(CombatState.Win);
            }
        }
    }
}

public enum CombatState
{
    Begin,
    Lose,
    Win
}