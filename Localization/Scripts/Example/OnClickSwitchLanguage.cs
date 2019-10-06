using Localization;
using System.Collections.Generic;
using UnityEngine;

public class OnClickSwitchLanguage: MonoBehaviour {
    private LinkedList<string> languages = new LinkedList<string>(new string[] { "en_US", "de_DE" });
    private LinkedListNode<string> currentLanguage;

    void Awake() {
        currentLanguage = languages.First;
    }

    public void Switch() {
        currentLanguage = currentLanguage.Next != null ? currentLanguage.Next : languages.First;
        LocalizationManager.Instance.LoadLocalizedText(currentLanguage.Value);
    }
}
