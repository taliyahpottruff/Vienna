using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Grid {
    public class BattleManager : MonoBehaviour {
        public GameObject gridSelector;
        public LayerMask gridSelectionMask;
        public TextMeshProUGUI turnDisplay;

        private Controls controls;
        private Vector2 mousePosition;
        private Vector3 hoveredPosition;
        private bool gridHovered, charMoving;
        private RaycastHit hit;
        private List<Faction> factions = new List<Faction>();
        [SerializeField]
        private int phase = 0, turn = 1;
        [SerializeField]
        private GridPlayerController[] player;
        private GridPlayerController activeChar;

        private void Awake() {
            factions.Add(new Faction(player, true));
        }

        private void Start() {
            controls = new Controls();
            controls.UI.Select.performed += Select_performed;
            controls.UI.PointerPosition.performed += ctx => mousePosition = ctx.ReadValue<Vector2>();
            controls.Enable();
        }

        private void Update() {
            if (factions[phase].isPlayer && !charMoving) {
                // Handle mouse
                var ray = Camera.main.ScreenPointToRay(mousePosition);
                gridHovered = Physics.Raycast(ray, out hit, Mathf.Infinity, gridSelectionMask);
                var onGrid = gridHovered && hit.transform.tag.Equals("Grid");
                gridSelector.SetActive(onGrid);

                if (onGrid) {
                    var position = hit.point;
                    position.x = Mathf.Round(position.x);
                    position.z = Mathf.Round(position.z);
                    hoveredPosition = position;
                    gridSelector.transform.position = new Vector3(position.x, position.y + 0.01f, position.z);
                }
            } else {
                if (gridSelector.activeSelf) gridSelector.SetActive(false);
            }
        }

        public void NextPhase() {
            if (++phase >= factions.Count) {
                phase = 0;
                turn++;
            }

            if (!factions[phase].isPlayer) {
                NextPhase();
            }

            turnDisplay.text = $"Turn {turn}\n{factions[phase].name}'s Phase";
        }

        private void Select_performed(InputAction.CallbackContext obj) {
            if (gridHovered && factions[phase].isPlayer && !charMoving) {
                activeChar = factions[phase].Characters[0];
                charMoving = true;
                activeChar.agent.SetDestination(hoveredPosition);
                gridSelector.SetActive(false);
                activeChar.OnFinished += Character_OnFinished;
                
            }
        }

        private void Character_OnFinished() {
            charMoving = false;
            activeChar.OnFinished -= Character_OnFinished;
            NextPhase();
            Debug.Log("Finished moving...");
        }
    }

    public class Faction {
        public readonly string name;
        public readonly bool isPlayer;

        List<GridPlayerController> characters;

        public GridPlayerController[] Characters { get => characters.ToArray(); }

        public Faction(GridPlayerController[] chars, bool player = false, string name = "Player") {
            this.name = name;
            characters = new List<GridPlayerController>(chars);
            isPlayer = player;
        }
    }
}