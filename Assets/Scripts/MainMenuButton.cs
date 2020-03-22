using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : Button {
	public ButtonClickedEvent clickEvent;

	[SerializeField]
	private Image labelBackground;
	[SerializeField]
	private Color normal, highlighted;

	private SoundEffectsManager soundManager;

	protected override void Awake() {
		soundManager = FindObjectOfType<SoundEffectsManager>();
	}

	protected override void OnEnable() {
		clickEvent = onClick;
	}

	public override void OnPointerEnter(PointerEventData eventData) {
		base.OnPointerEnter(eventData);

		Hover();
	}

	public override void OnPointerExit(PointerEventData eventData) {
		base.OnPointerExit(eventData);

		Unhover();
	}

	public override void OnSelect(BaseEventData eventData) {
		base.OnSelect(eventData);

		Debug.Log($"{gameObject.name}: Select");
	}

	public override void OnDeselect(BaseEventData eventData) {
		base.OnDeselect(eventData);

		Debug.Log($"{gameObject.name}: Deselect");
	}

	private void Hover() {
		StopAllCoroutines();
		soundManager.PlaySoundEffect("menuHover");
		StartCoroutine(SmoothLabelBg(100f, highlighted));
	}

	private void Unhover() {
		StopAllCoroutines();
		StartCoroutine(SmoothLabelBg(75f, normal));
	}

	private IEnumerator SmoothLabelBg(float targetHeight, Color color) {
		Vector2 currentSize = labelBackground.rectTransform.sizeDelta;
		Color startColor = labelBackground.color;
		float height = currentSize.y;
		float startTime = Time.time;
		float duration = 0.1f;
		float elapsed = 0f;

		while (elapsed < duration) {
			elapsed = Time.time - startTime;
			height = Mathf.Lerp(currentSize.y, targetHeight, elapsed / duration);
			labelBackground.color = Color.Lerp(startColor, color, elapsed / duration);
			labelBackground.rectTransform.sizeDelta = new Vector2(currentSize.x, height);
			yield return new WaitForEndOfFrame();
		}

		height = targetHeight;
		labelBackground.rectTransform.sizeDelta = new Vector2(currentSize.x, height);
		labelBackground.color = color;
	}
}