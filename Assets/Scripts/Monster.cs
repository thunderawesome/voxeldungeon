using UnityEngine;

public class Monster : Entity
{
    public MonsterModel Model => m_model;

    [SerializeField]
    private MonsterModel m_model;

    private void Awake()
    {
        Initialize(m_model);
    }

    public override void Initialize(EntityModel entity)
    {
        base.Initialize(entity);        
    }
}