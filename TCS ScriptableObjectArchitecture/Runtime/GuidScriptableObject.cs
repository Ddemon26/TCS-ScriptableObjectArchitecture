using System;
using UnityEngine;
namespace TCS.ScriptableObjectArchitecture {
    /// <summary>
    /// ScriptableObject that stores a GUID for unique identification. The population of this field is implemented
    /// inside an Editor script.
    /// </summary>
    [Serializable]
    public abstract class GuidScriptableObject : ScriptableObject {
        [HideInInspector]
        [SerializeField]
        byte[] m_guid;

        public Guid Guid => new(m_guid);

        void OnValidate() {
            if (m_guid.Length == 0) {
                m_guid = Guid.NewGuid().ToByteArray();
            }
        }
    }
}