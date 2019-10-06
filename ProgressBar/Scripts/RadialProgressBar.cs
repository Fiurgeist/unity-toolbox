using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : AbstractProgressBar {
	Image barImage;
	Text progressText;

    override protected void Awake() {
        base.Awake();

        barImage = GetComponent<Image>();
		progressText = GetComponentInChildren<Text>();
    }

	void OnGUI() {
		barImage.fillAmount = currentProgress;
	}

	override public void ProgressUpdate() {
        currentProgress = progress.value / progress.maxValue;
		progressText.text = "" + (int)Mathf.Ceil(progress.value);
	}
}
