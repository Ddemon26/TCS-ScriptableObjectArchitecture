using System;
using System.Collections.Generic;
using UnityEngine;
namespace TCS.ScriptableObjectArchitecture {
    /// <summary>
    /// ScriptableObject class that contains a list of a given type. The instance of this ScriptableObject can be
    /// referenced by components, without a hard reference between systems.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RuntimeCollection<T> : ScriptableObject {
        public List<T> m_items = new();

        public event Action<T> ItemAdded;

        public event Action<T> ItemRemoved;

        public void Add(T item) {
            if (m_items.Contains(item)) return;
            m_items.Add(item);
            ItemAdded?.Invoke(item);
        }

        public void Remove(T item) {
            if (!m_items.Contains(item)) return;
            m_items.Remove(item);
            ItemRemoved?.Invoke(item);
        }
    }
}