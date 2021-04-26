using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon", menuName = "Battlerock/ScriptableObjects/Dungeon", order = 1)]
public class DungeonModel : AbstractAssetModel
{
    #region Public Properties

    public IReadOnlyCollection<RoomModel> Rooms => m_rooms;
    public bool IsCleared => m_isCleared;

    #endregion

    #region Private Fields

    [SerializeField]
    private RoomModel[] m_rooms;

    private bool m_isCleared = false;

    #endregion

}