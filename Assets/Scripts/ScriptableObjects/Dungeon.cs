using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon", menuName = "Battlerock/ScriptableObjects/Dungeon", order = 1)]
public class Dungeon : AbstractAsset
{
    #region Public Properties

    public IReadOnlyCollection<Room> Rooms => m_rooms;
    public bool IsCleared => m_isCleared;

    #endregion

    #region Private Fields

    [SerializeField]
    private Room[] m_rooms;

    private bool m_isCleared = false;

    #endregion

}