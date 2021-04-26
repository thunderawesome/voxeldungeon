using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Battlerock/ScriptableObjects/Monster", order = 1)]
public class MonsterModel : EntityModel
{
    #region Public Properties
    public IReadOnlyCollection<LootModel> Loot => m_loot;

    #endregion

    #region Private Fields

    [SerializeField]
    private LootModel[] m_loot;

    #endregion

}