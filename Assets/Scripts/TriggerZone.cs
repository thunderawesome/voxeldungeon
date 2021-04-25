using Battlerock;
using UnityEngine;

public enum TriggerState
{
    None,
    Active
}

public class TriggerZone : MonoBehaviour
{
    public Sprite interactIconSprite;

    private TriggerState m_state;

    private GameObject m_iconObject;

    private const float ICON_SIZE = 15.0f;

    private string m_triggerName = "";

    [SerializeField]
    private Color m_color = Color.black;

    public TriggerState State
    {
        get
        {
            return m_state;
        }

        set
        {
            m_state = value;
            SetIcon();
        }
    }

    public void InvokeTrigger()
    {
        EventManager.TriggerEvent(m_triggerName);
    }

    private void Start()
    {
        if (m_iconObject == null)
        {
            m_iconObject = new GameObject("Icon");
            var renderer = m_iconObject.AddComponent<SpriteRenderer>();
            renderer.sprite = interactIconSprite;
            renderer.color = m_color;
            m_iconObject.transform.parent = transform;
            m_iconObject.transform.localPosition = Vector3.up * 4;
            m_iconObject.transform.localScale = new Vector3(ICON_SIZE, ICON_SIZE, 1);
            m_iconObject.SetActive(false);
        }

        if (interactIconSprite == null)
        {
            Debug.LogErrorFormat("{0}: No InteractIcon assigned.", name);
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            State = TriggerState.Active;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            State = TriggerState.None;
        }
    }

    private void SetIcon()
    {
        var isActive = State == TriggerState.Active ? true : false;
        m_iconObject.SetActive(isActive);
    }
}
