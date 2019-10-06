using UnityEngine;

public class AbstractProgressProvider: MonoBehaviour {
    public float value;
    public float maxValue;

    private AbstractProgressBar bar;

    void Awake() {
        bar = GetComponent<AbstractProgressBar>();
    }

    public void OnChanged() {
        bar.OnProgressChanged();
    }
}
