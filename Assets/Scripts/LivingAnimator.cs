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
		public SpriteRenderer hairRenderer, topRenderer, bottomRenderer, coreRenderer, headRenderer, weaponRenderer;

		private SpriteRenderer sr;
		private Rigidbody2D rb;
		private Living living;

		private bool calculateDirection = true;
		private Direction direction = Direction.Down;
		private bool moving;
		[SerializeField]
		private Sprite[] hairSprites, topSprites, bottomSprites, coreSprites, headSprites, weaponSprites;

		#region State Definitions
		private Dictionary<string, int[]> animationStates = new Dictionary<string, int[]>() {
			{"Aim_Down", new int[] { 24 } },
			{"Aim_Up", new int[] { 25 } },
			{"Aim_Side", new int[] { 26 } }
		};
		public string currentCoreState = "";
        #endregion

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
			if (moving && calculateDirection) { //First check if movement is even happening
				direction = DirectionUtility.VectorToDirection(movementVector);
			}

			if (direction == Direction.Left) {
				transform.localScale = new Vector3(-1, 1, 1);
			} else {
				transform.localScale = new Vector3(1, 1, 1);
			}
		}

		public void SetDirection(bool allowCalculation, Direction direction = Direction.Down) {
			if (!allowCalculation) {
				this.direction = direction;
            }

			calculateDirection = allowCalculation;
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
			LoadWeaponSprites();
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

		public void LoadWeaponSprites() {
			if (living.equippedWeapon != null) {
				var name = living.equippedWeapon.Sprite;
				weaponSprites = Resources.LoadAll<Sprite>($"Sprites/Weapons/{name}");
			} else {
				weaponSprites = new Sprite[] { };
            }
        }
		#endregion

		private IEnumerator Animate() {
			int index = 0, coreIndex = 0;
			while (true) {
				List<Sprite> animation = GetAnimation(direction);
				if (index >= animation.Count || !moving) index = 0;

				// Set main sprite
				sr.sprite = animation[index];
				var animIndex = int.Parse(animation[index].name.Split('_')[1]);

				// Set additional sprites
				if (hairRenderer != null) hairRenderer.sprite = Utils.GetSpriteFromArray(animIndex, hairSprites, true);
				if (bottomRenderer != null) bottomRenderer.sprite = Utils.GetSpriteFromArray(animIndex, bottomSprites, true);
				if (headRenderer != null) headRenderer.sprite = Utils.GetSpriteFromArray(animIndex, headSprites, true);

				// Render upper body state
				if (animationStates.ContainsKey(currentCoreState)) {
					var state = animationStates[currentCoreState];
					if (coreIndex >= state.Length) coreIndex = 0;

					var spriteIndex = state[coreIndex];
					var weaponOffset = 24;

					// Render
					if (coreRenderer != null) coreRenderer.sprite = Utils.GetSpriteFromArray(spriteIndex, coreSprites);
					if (topRenderer != null) topRenderer.sprite = Utils.GetSpriteFromArray(spriteIndex, topSprites);
					if (weaponRenderer != null) weaponRenderer.sprite = Utils.GetSpriteFromArray(spriteIndex - weaponOffset, weaponSprites);
				} else {
					if (coreRenderer != null) coreRenderer.sprite = Utils.GetSpriteFromArray(animIndex, coreSprites, true);
					if (topRenderer != null) topRenderer.sprite = Utils.GetSpriteFromArray(animIndex, topSprites, true);
					if (weaponRenderer != null) weaponRenderer.sprite = null;
				}

				index++;
				coreIndex++;
				yield return new WaitForSeconds(animationSpeed);
			}
		}
	}
}