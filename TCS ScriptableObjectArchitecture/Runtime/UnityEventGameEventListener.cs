using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
namespace TCS.ScriptableObjectArchitecture {
    /// <summary>
    /// This class implements the IGameEventListener interface and exposes a GameEvent that we can populate within the
    /// inspector. When this GameEvent's Raise() method is fired externally, this class will invoke a UnityEvent.
    /// </summary>
    public class UnityEventGameEventListener : MonoBehaviour, IGameEventListenable {
        [SerializeField] GameEvent m_gameEvent;
        [SerializeField] UnityEvent m_response;

        public GameEvent GameEvent {
            get => m_gameEvent;
            set => m_gameEvent = value;
        }

        void OnEnable() {
            Assert.IsNotNull(GameEvent, "Assign this GameEvent within the editor!");

            GameEvent.RegisterListener(this);
        }

        void OnDisable() => GameEvent.DeregisterListener(this);
        public void EventRaised() => m_response.Invoke();
    }
}