using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Public Properties

    public int Health => m_health;

    public int Damage => m_damage;

    public bool IsDefeated => m_isDefeated;

    #endregion

    #region Protected Fields

    [SerializeField]
    protected int m_health = 1;

    [SerializeField]
    protected int m_damage = 1;

    protected bool m_isDefeated = false;

    #endregion

    #region Public Methods

    public virtual void Initialize(EntityModel entity)
    {
        m_health = entity.Health;
        m_damage = entity.Damage;
    }

    #endregion
}