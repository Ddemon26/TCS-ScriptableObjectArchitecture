using UnityEngine;

namespace Unity.BossRoom.Infrastructure
{
    [CreateAssetMenu (menuName = "TCS/SOA/IntVariable", fileName = "IntVariable")]
    public class IntVariable : ScriptableObject
    {
        public int Value;
    }
}
