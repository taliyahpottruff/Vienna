using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vienna {
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Living))]
	public class LivingAnimator : MonoBehaviour {
		public float animationSpeed = 0.1f;
		public List<Sprite> upSprites, downSprites, sideSprites;
		public SpriteRenderer hairRenderer, topRenderer, bottomRenderer, coreRenderer, headRenderer;

		private SpriteRenderer sr;
		private Rigidbody2D rb;
		private Living living;

		private Direction direction = Direction.Down;
		private bool moving;
		private Sprite[] hairSprites, topSprites, bottomSprites, coreSprites, headSprites;

		private void Awake() {
			sr = GetComponent<SpriteRenderer>();
			rb = GetComponent<Rigidbody2D>();
			living = GetComponent<Living>();
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
		public void LoadSprites() {
			LoadHairSprites();
			LoadTopSprites();
			LoadBottomSprites();
			LoadCoreSprites();
			LoadHeadSprites();
        }

        public void LoadHairSprites() {
			hairSprites = Resources.LoadAll<Sprite>($"Sprites/Hair/{living.hairType}");
		}

		public void LoadTopSprites() {
			topSprites = Resources.LoadAll<Sprite>($"Sprites/Tops/{living.topType}");
        }

		public void LoadBottomSprites() {
			bottomSprites = Resources.LoadAll<Sprite>($"Sprites/Bottoms/{living.bottomType}");
        }

		public void LoadCoreSprites() {
			coreSprites = Resources.LoadAll<Sprite>("Sprites/Core");
        }

		public void LoadHeadSprites() {
			headSprites = Resources.LoadAll<Sprite>("Sprites/Head");
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
				if (coreRenderer != null) coreRenderer.sprite = Utils.GetSpriteFromArray(animIndex, coreSprites, true);
				if (headRenderer != null) headRenderer.sprite = Utils.GetSpriteFromArray(animIndex, headSprites, true);

				index++;
				yield return new WaitForSeconds(animationSpeed);
			}
		}
	}
}