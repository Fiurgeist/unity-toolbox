using UnityEngine;

[RequireComponent(typeof(AbstractProgressProvider))]
public abstract class AbstractProgressBar : MonoBehaviour {
    protected AbstractProgressProvider progress;
    protected float currentProgress;

    virtual protected void Awake() {
        progress = GetComponent<AbstractProgressProvider>();
	}

    public void OnProgressChanged() {
        ProgressUpdate();
    }

    abstract public void ProgressUpdate();
}
