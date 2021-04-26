using UnityEngine;
public class EntityModel : AbstractAssetModel
{
    #region Public Properties
    public int Health => m_health;

    public int Damage => m_damage;

    #endregion

    #region Protected Methods

    protected GameObject InstantiatePrefab(Transform targetTransform, bool worldPositionStays)
    {
        return Instantiate(m_prefab, targetTransform, worldPositionStays).Initialize(this);
    }

    #endregion

    #region Private Fields

    [SerializeField]
    private int m_health = 10;

    [SerializeField]
    private int m_damage = 1;

    #endregion

    #region Protected Fields

    [SerializeField]
    protected GameObject m_prefab = null;

    #endregion
}

public static class EntityModelExtensions
{
    #region Public Methods

    public static GameObject Initialize(this GameObject obj, EntityModel character)
    {
        if (obj.TryGetComponent<Entity>(out Entity entity))
        {
            entity.Initialize(character);
        }
        else
        {
            entity = obj.AddComponent<Entity>();
            entity.Initialize(character);
        }
        return obj;
    }

    #endregion
}