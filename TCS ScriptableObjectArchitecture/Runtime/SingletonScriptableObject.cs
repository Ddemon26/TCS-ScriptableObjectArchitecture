using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace TCS.ScriptableObjectArchitecture {
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject {
        // Static dictionary to handle instances of different generic types to prevent conflicts between different versions of T
        static readonly System.Collections.Concurrent.ConcurrentDictionary<System.Type, ScriptableObject> Instances = new();
        // Lock object for ensuring thread safety
        static readonly object LockObject = new(); 

        /// <summary>
        /// Gets the Singleton instance of the ScriptableObject.
        /// If the instance does not exist, attempts to find or load it by type.
        /// </summary>
        public static T Instance {
            get {
                lock (LockObject) {
                    // Ensure all access to the instance dictionary is synchronized
                    // Attempt to get the existing instance from the dictionary
                    if (Instances.TryGetValue(typeof(T), out var existingInstance) && existingInstance is T typedInstance) {
                        return typedInstance;
                    }

                    // Attempt to find an existing instance in the project by type
                    var instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();

                    // Log an error if no instance is found, ensuring there is an instance in the project
                    if (!instance) {
                        Debug.LogError($"No instance of {typeof(T)} found. Make sure there is one in your project.");
                    }

                    // Assign the found instance to the dictionary
                    Instances[typeof(T)] = instance;

                    return instance;
                }
            }
        }

        /// <summary>
        /// Called when the ScriptableObject is enabled.
        /// Ensures that there is only one instance of the Singleton in the project.
        /// </summary>
        protected virtual void OnEnable() {
            lock (LockObject) {
                // Lock to ensure thread safety when enabling the instance
                if (!IsInstanceRegistered()) {
                    RegisterInstance(); // Register this instance as the Singleton
                }
                else if (IsDuplicateInstance()) {
                    HandleDuplicateInstance(); // Handle any duplicate instances that may exist
                }
            }
        }

        // Checks if an instance is already registered in the dictionary
        bool IsInstanceRegistered()
            => Instances.TryGetValue(typeof(T), out var existingInstance) && existingInstance;

        // Registers this instance as the Singleton instance
        void RegisterInstance() => Instances[typeof(T)] = this;

        // Checks if the current instance is a duplicate
        bool IsDuplicateInstance()
            => Instances.TryGetValue(typeof(T), out var existingInstance) && existingInstance != this;

        // Handles the scenario where a duplicate instance is detected
        void HandleDuplicateInstance() {
            Debug.LogWarning($"Another instance of {typeof(T)} already exists. This duplicate instance will be deleted.");

            if (Application.isPlaying) {
                // Do not delete assets during play mode, log a warning instead
                Debug.LogWarning("Skipping deletion of asset during play mode.");
                return;
            }

#if UNITY_EDITOR
            ScheduleDuplicateAssetDeletion(); // Schedule the duplicate asset for deletion
#endif
        }

#if UNITY_EDITOR
        // Schedules the deletion of the duplicate asset in the Unity Editor
        void ScheduleDuplicateAssetDeletion() {
            string assetPath = AssetDatabase.GetAssetPath(this);
            if (!string.IsNullOrEmpty(assetPath)) {
                // Delay the deletion to ensure it doesn't interfere with Unity's current operations
                EditorApplication.delayCall += () =>
                {
                    DeleteDuplicateAsset(assetPath);
                    EditorGUIUtility.PingObject(Instances[typeof(T)]);
                };
            }
        }

        // Deletes the duplicate asset from the AssetDatabase
        void DeleteDuplicateAsset(string assetPath) {
            try {
                AssetDatabase.DeleteAsset(assetPath); // Delete the asset at the specified path
                AssetDatabase.Refresh(); // Refresh the AssetDatabase to reflect changes
                Debug.Log($"Duplicate asset {assetPath} deleted.");
            }
            catch (System.Exception ex) {
                Debug.LogError($"Failed to delete duplicate asset: {ex.Message}");
            }
        }
#endif

        /// <summary>
        /// Called when the ScriptableObject is destroyed.
        /// Ensures that the Singleton instance is cleared if it matches the current instance.
        /// </summary>
        protected virtual void OnDestroy() {
            lock (LockObject) {
                // Lock to ensure thread safety when destroying the instance
                if (!Instances.TryGetValue(typeof(T), out var existingInstance) || existingInstance != this) return;
                Instances.TryRemove(typeof(T), out _); // Remove this instance from the dictionary
                
                // Optional: Unload unused assets to free up memory
                //Resources.UnloadUnusedAssets();
                
            }
        }

        /// <summary>
        /// Resets the Singleton instance when the subsystem is registered.
        /// Useful for ensuring a fresh state at subsystem startup.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetInstance() {
            lock (LockObject) {
                // Lock to ensure thread safety during instance reset
                Instances.TryRemove(typeof(T), out _); // Reset the instance to ensure a fresh start
            }
        }
    }
}
