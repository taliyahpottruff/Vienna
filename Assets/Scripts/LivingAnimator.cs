using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vienna {
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class LivingAnimator : MonoBehaviour {
		public float animationSpeed = 0.1f;
		public List<Sprite> upSprites, downSprites, sideSprites;
		public string hairType = "ShortHair", topType = "T-Shirt", bottomType = "Pants";
		public SpriteRenderer hairRenderer, topRenderer, bottomRenderer;

		private SpriteRenderer sr;
		private Rigidbody2D rb;

		private Direction direction = Direction.Down;
		private bool moving;
		private Sprite[] hairSprites, topSprites, bottomSprites;

		private void Awake() {
			sr = GetComponent<SpriteRenderer>();
			rb = GetComponent<Rigidbody2D>();
		}

		private void Start() {
			LoadSprites();
			StartCoroutine(Animate());
		}

		private void Update() {
			Vector2 movementVector = rb.velocity.normalized;
			moving = !movementVector.Equals(Vector2.zero);
			if (moving) { //First check if movement is even happening
				direction = DirectionUtility.VectorToDirection(movementVector);

				if (direction == Direction.Left) {
					transform.localScale = new Vector3(-1, 1, 1);
				} else {
					transform.localScale = new Vector3(1, 1, 1);
				}
			}
		}

		private List<Sprite> GetAnimation(Direction direction) {
			switch (direction) {
				case Direction.Down:
					return downSprites;
				case Direction.Up:
					return upSprites;
				default:
					return sideSprites;
			}
		}

        #region Load Sprites
		private void LoadSprites() {
			LoadHairSprites();
			LoadTopSprites();
			LoadBottomSprites();
        }

        private void LoadHairSprites() {
			hairSprites = Resources.LoadAll<Sprite>($"Sprites/Hair/{hairType}");
		}

		private void LoadTopSprites() {
			topSprites = Resources.LoadAll<Sprite>($"Sprites/Tops/{topType}");
        }

		private void LoadBottomSprites() {
			bottomSprites = Resources.LoadAll<Sprite>($"Sprites/Bottoms/{bottomType}");
        }
        #endregion

        private IEnumerator Animate() {
			int index = 0;
			while (true) {
				List<Sprite> animation = GetAnimation(direction);
				if (index >= animation.Count || !moving) index = 0;

				//Set main sprite
				sr.sprite = animation[index];
				var animIndex = int.Parse(animation[index].name.Split('_')[1]);

				//Set additional sprites
				if (hairRenderer != null) hairRenderer.sprite = Utils.GetSpriteFromArray(animIndex, hairSprites, true);
				if (topRenderer != null) topRenderer.sprite = Utils.GetSpriteFromArray(animIndex, topSprites, true);
				if (bottomRenderer != null) bottomRenderer.sprite = Utils.GetSpriteFromArray(animIndex, bottomSprites, true);

				index++;
				yield return new WaitForSeconds(animationSpeed);
			}
		}
	}
}