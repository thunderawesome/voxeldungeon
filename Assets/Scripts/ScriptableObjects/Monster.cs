using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Battlerock/ScriptableObjects/Monster", order = 1)]
public class Monster : Character
{
    #region Public Properties
    public IReadOnlyCollection<Loot> Loot => m_loot;
    public bool IsDefeated => m_isDefeated;

    #endregion


    #region Public Methods

    public GameObject InstantiatePrefab(Transform targetTransform, bool worldPositionStays)
    {
        return Instantiate(m_prefab, targetTransform, worldPositionStays);
    }

    #endregion

    #region Private Fields

    [SerializeField]
    private GameObject m_prefab = null;
    [SerializeField]
    private Loot[] m_loot;

    private bool m_isDefeated = false;

    #endregion

}