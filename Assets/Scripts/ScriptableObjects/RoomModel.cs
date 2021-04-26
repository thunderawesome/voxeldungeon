using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "Battlerock/ScriptableObjects/Room", order = 1)]
public class RoomModel : AbstractAssetModel
{
    #region Public Properties
    public IReadOnlyCollection<MonsterModel> Monsters => m_monsters;

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
    private MonsterModel[] m_monsters;

    #endregion

}