using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Localization {
    [ExecuteInEditMode]
    public class LocalizationManager: MonoBehaviour {
        private static LocalizationManager instance = null;
        public static LocalizationManager Instance {
            get {
                if(instance == null) {
                    instance = (LocalizationManager)FindObjectOfType(typeof(LocalizationManager));
                }
                return instance;
            }
        }

        public bool IsReady { get; private set; }
        public UnityEvent languageChangedEvent;

        private string currentLanguage = "en_US";
        private string localizationDir;

        private Dictionary<string, string> localizedText;

        void Awake() {
            if(instance == null) {
                instance = this;
            } else if(instance != this) {
                Destroy(gameObject);
            }
            
            if(Application.isPlaying) {
                DontDestroyOnLoad(gameObject);
            }

            localizationDir = Path.Combine(Application.streamingAssetsPath, "Locales");
            if(languageChangedEvent == null) {
                languageChangedEvent = new UnityEvent();
            }

        }

        private void Start() {
            if(IsReady == false) {
                LoadLocalizedText(currentLanguage);
            }
        }

        public void LoadLocalizedText(string languageCode) {
            IsReady = false;
            localizedText = new Dictionary<string, string>();
            currentLanguage = languageCode;
            string filePath = Path.Combine(localizationDir, $"{currentLanguage}.json");

            if(File.Exists(filePath)) {
                string json = File.ReadAllText(filePath);
                // TODO: load GNU gettext .mo files
                LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(json);

                for(int i = 0; i < loadedData.items.Length; i++) {
                    localizedText.Add(loadedData.items[i].textId, loadedData.items[i].text);
                }
                Debug.Log($"Localization data loaded for {languageCode}, dictionary contains: {localizedText.Count} entries.");
            } else {
                Debug.LogWarning($"Cannot find localization file for '{currentLanguage}' at {filePath}!");
            }

            IsReady = true;
            OnLanguageChanged();
        }

        void OnLanguageChanged() {
            languageChangedEvent.Invoke();
        }
        
        /// <summary>
        /// Get the localized text for the given `textId`.
        /// </summary>
        /// <remarks>
        /// The `textId` can be a generic string like "btn_abort" or the default language option like "Abort".
        /// </remarks>
        /// <param name="textId">The identifier for a localized text.</param>
        /// <returns>The localized text or `textId` if no translation is found.</returns>
        public string GetText(string textId) {
            string localized = textId;
            if(IsReady && localizedText.ContainsKey(textId)) {
                string entry = localizedText[textId];
                if(!string.IsNullOrEmpty(entry)) {
                    localized = entry;
                }
            }

            return localized;
        }

        [MenuItem("EDITORS/Collect TextIds")]
        public static void CollectTextIds() {
            Debug.Log("Start CollectTextIds");
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets/Localization/Prefabs" });
            foreach(string guid in guids) {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                LocalizedText[] texts = prefab.GetComponentsInChildren<LocalizedText>(true);
                // TODO: write/update GNU gettext .po files
                foreach(LocalizedText text in texts) {
                    Debug.Log(text.textId);
                    Debug.Log(text.textIdPlural);
                }
            }
            Debug.Log("CollectTextIds done");
        }
    }
}
