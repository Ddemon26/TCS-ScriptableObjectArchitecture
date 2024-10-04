using UnityEngine;
namespace TCS.ScriptableObjectArchitecture {
    [CreateAssetMenu(menuName = "TCS/SOA/IntVariable", fileName = "IntVariable")]
    public class IntVariable : ScriptableObject {
        public int m_value;
    }
}