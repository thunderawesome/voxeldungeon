using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Battlerock/ScriptableObjects/Monster", order = 1)]
public class Monster : Character
{
    #region Public Properties
    public IReadOnlyCollection<Loot> Loot => m_loot;
    public bool IsDefeated => m_isDefeated;

    #endregion

    #region Private Fields

    [SerializeField]
    private Loot[] m_loot;

    private bool m_isDefeated = false;

    #endregion

}