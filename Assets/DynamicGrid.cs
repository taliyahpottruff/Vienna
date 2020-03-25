using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(GridLayoutGroup))]
public class DynamicGrid : MonoBehaviour {
	public int cellsPerRow = 5;

	private GridLayoutGroup grid;
	private RectTransform rectTransform;

	private void Awake() {
		grid = GetComponent<GridLayoutGroup>();
		rectTransform = GetComponent<RectTransform>();
	}

	private void Update() {
		if (rectTransform == null) {
			rectTransform = GetComponent<RectTransform>();
		}

		float totalSpacing = grid.spacing.x * (cellsPerRow - 1);
		float totalPadding = grid.padding.left + grid.padding.right;
		float cellSize = (rectTransform.rect.width - totalPadding - totalSpacing) / cellsPerRow;
		grid.cellSize = new Vector2(cellSize, cellSize);
	}
}