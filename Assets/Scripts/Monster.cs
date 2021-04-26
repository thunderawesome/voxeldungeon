using UnityEngine;

public class Monster : Entity
{
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