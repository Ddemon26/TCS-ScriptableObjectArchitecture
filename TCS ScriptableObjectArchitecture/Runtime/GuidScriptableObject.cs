using UnityEngine;
namespace TCS.ScriptableObjectArchitecture {
    /// <summary>
    /// ScriptableObject that stores a GUID for unique identification. The population of this field is implemented
    /// inside an Editor script.
    /// </summary>
    [System.Serializable] public abstract class GuidScriptableObject : ScriptableObject {
        [SerializeField, HideInInspector]
        byte[] m_guid;

        public System.Guid Guid => new(m_guid);

        void OnValidate() {
            if (m_guid.Length == 0) {
                m_guid = System.Guid.NewGuid().ToByteArray();
            }
        }
    }
}