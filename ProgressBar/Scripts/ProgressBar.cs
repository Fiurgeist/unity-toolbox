using UnityEngine;

public class ProgressBar : AbstractProgressBar {
	public RectTransform barTransform;

	RectTransform rectTransform;
    float progressBase;

    override protected void Awake() {
        base.Awake();

        rectTransform = GetComponent<RectTransform>();
    }

	void OnGUI() {
		ResizeBar();
	}

    override public void ProgressUpdate() {
		currentProgress = 1f - (progress.value / progress.maxValue);
	}

	void ResizeBar() {
		barTransform.sizeDelta = new Vector2(-rectTransform.rect.width * currentProgress, barTransform.sizeDelta.y);
	}
}
