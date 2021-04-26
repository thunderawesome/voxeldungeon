using UnityEngine;

public abstract class AbstractAssetModel : ScriptableObject
{
    #region Public Properties
    public string Name => m_name;

    public string Description => m_description;

    public bool IsActive => m_isActive;

    #endregion

    #region Private Fields

    [SerializeField]
    private string m_name = "Asset";

    [SerializeField, TextArea]
    private string m_description = string.Empty;

    [SerializeField]
    private bool m_isActive = false;

    #endregion
}