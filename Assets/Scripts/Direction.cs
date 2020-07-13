using UnityEngine;

public enum Direction {
	Up, Down, Left, Right
}

public class DirectionUtility {
	public static Direction VectorToDirection(Vector2 vector) {
		Vector2 direction = vector.normalized;
		if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) || Mathf.Abs(direction.x) == Mathf.Abs(direction.y)) { //Sideways movement
			if (direction.x > 0) {
				return Direction.Right;
			} else {
				return Direction.Left;
			}
		} else { //Vertical movement
			if (direction.y > 0) {
				return Direction.Up;
			} else {
				return Direction.Down;
			}
		}
	}
}