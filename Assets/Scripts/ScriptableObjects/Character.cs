using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Battlerock/ScriptableObjects/Character", order = 1)]
public class Character : AbstractAsset
{
    #region Public Properties
    public int Health => m_health;

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
    private int m_health;

    #endregion

}