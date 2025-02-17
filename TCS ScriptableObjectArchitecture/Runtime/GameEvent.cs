using System.Collections.Generic;
using UnityEngine;
namespace TCS.ScriptableObjectArchitecture {
    /// <summary>
    /// Class for encapsulating game-related events within ScriptableObject instances. This class defines a List of
    /// GameEventListeners, which will be notified whenever this GameEvent's Raise() method is fired.
    /// </summary>
    [CreateAssetMenu(menuName = "Tent City Studio/SOA/GameEvent", fileName = "GameEvent")]
    public class GameEvent : ScriptableObject {
        List<IGameEventListenable> m_listeners = new();

        public void Raise() {
            for (int i = m_listeners.Count - 1; i >= 0; i--) {
                if (m_listeners[i] == null) {
                    m_listeners.RemoveAt(i);
                    continue;
                }

                m_listeners[i].EventRaised();
            }
        }

        public void RegisterListener(IGameEventListenable listener) {
            foreach (var t in m_listeners) {
                if (t == listener) {
                    return;
                }
            }

            m_listeners.Add(listener);
        }

        public void DeregisterListener(IGameEventListenable listener) => m_listeners.Remove(listener);
    }
}