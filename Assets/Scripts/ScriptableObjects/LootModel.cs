using UnityEngine;

[CreateAssetMenu(fileName = "Loot", menuName = "Battlerock/ScriptableObjects/Loot", order = 1)]
public class LootModel : AbstractAssetModel
{
    #region Public Methods

    public GameObject InstantiatePrefab(Transform targetTransform, bool worldPositionStays)
    {
        return Instantiate(m_prefab, targetTransform, worldPositionStays);
    }

    #endregion

    #region Private Fields


    [SerializeField]
    private GameObject m_prefab = null;

    #endregion

}