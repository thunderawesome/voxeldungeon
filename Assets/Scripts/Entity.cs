using UnityEngine;

[RequireComponent(typeof(AudioSource))]
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

    protected AudioSource m_audioSource;

    [SerializeField]
    protected AudioClip m_hitSound;
    [SerializeField]
    protected AudioClip m_explosionSound;

    public Color originalColor;

    protected Color m_damageColor = Color.red;

    #endregion

    #region Public Methods

    public virtual void Initialize(EntityModel entity)
    {
        m_health = entity.Health;
        m_damage = entity.Damage;

        m_audioSource = GetComponent<AudioSource>();
        originalColor = GetComponentInChildren<Renderer>().material.color;
    }
    
    public virtual void TakeDamage(int damage)
    {
        if (!m_isDefeated)
        {
            m_health -= damage;
            m_audioSource.clip = m_hitSound;
            m_audioSource.Play();
            GetComponentInChildren<Renderer>().material.color = m_damageColor;
            Debug.Log(gameObject.name + $" took {damage} points of damage.");
        }

        if(m_health <= 0)
        {
            m_health = 0;
            m_isDefeated = true;
            m_audioSource.clip = m_explosionSound;
            m_audioSource.Play();
            Debug.Log(gameObject.name + " is defeated!");
            gameObject.SetActive(false);
        }
    }

    #endregion
}