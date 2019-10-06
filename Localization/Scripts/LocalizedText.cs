using UnityEngine;
using UnityEngine.UI;

namespace Localization {
    [RequireComponent(typeof(Text))]
    [ExecuteInEditMode]
    public class LocalizedText: MonoBehaviour {
        public string textId;
        public string textIdPlural;
        public AbstractVariableTextContent pluralVariable;

        private Text text;
        private AbstractVariableTextContent[] variables;
        private string localizedTextTemplate;
        private bool needsToRender;
        private bool isPlural;

        void Awake() {
            text = GetComponent<Text>();

            variables = GetComponents<AbstractVariableTextContent>();
        }

        void Start() {
            if(pluralVariable != null) {
                UpdateIsPlural();
            }
            
            UpdateLocalizedTextTemplate();
        }

        void OnEnable() {
            if(Application.isPlaying) {
                LocalizationManager.Instance.languageChangedEvent.AddListener(OnLanguageChanged);
            }
        }

        void OnDisable() {
            if(Application.isPlaying) {
                LocalizationManager.Instance.languageChangedEvent.RemoveListener(OnLanguageChanged);
            }
        }

        void LateUpdate() {
            if(needsToRender) {
                RenderText();
            }
        }

        public void RenderText() {
            string localizedText = localizedTextTemplate;
            foreach(AbstractVariableTextContent content in variables) {
                localizedText = localizedText.Replace($"{{{content.key}}}", content.value);
            }
            text.text = localizedText;
            needsToRender = false;
        }

        private void UpdateLocalizedTextTemplate() {
            localizedTextTemplate = LocalizationManager.Instance.GetText(isPlural ? textIdPlural : textId);
            needsToRender = true;
        }

        public void OnContentChanged() {
            if(pluralVariable != null && (pluralVariable.value == "1") == isPlural) {
                UpdateIsPlural();
                UpdateLocalizedTextTemplate();
            }
            needsToRender = true;
        }

        public void OnLanguageChanged() {
            UpdateLocalizedTextTemplate();
        }

        private void UpdateIsPlural() {
            // TODO: maybe move to LocalizationManager, also add support for languages with other grammatical numbers
            isPlural = pluralVariable.value != "1";
        }
    }

    [RequireComponent(typeof(LocalizedText))]
    public abstract class AbstractVariableTextContent: MonoBehaviour {
        public string key;
        public string value;

        private LocalizedText text;

        void Awake() {
            text = GetComponent<LocalizedText>();
        }

        public void OnChanged() {
            text.OnContentChanged();
        }
    }
}