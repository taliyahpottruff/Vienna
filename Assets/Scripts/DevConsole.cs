using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Vienna {
    public class DevConsole : MonoBehaviour {
        [SerializeField]
        private GameObject holder;
        [SerializeField]
        private TMP_InputField inputField;
        [SerializeField]
        private TextMeshProUGUI console;
        [SerializeField]
        private ScrollRect scrollRect;

        private Controls controls;

        private void Awake() {
            controls = new Controls();
            controls.UI.Submit.performed += Submit_performed;
            controls.UI.DevConsole.performed += ToggleConsole_performed;
            controls.Enable();
        }

        private void OnDestroy() {
            controls.UI.Submit.performed -= Submit_performed;
            controls.UI.DevConsole.performed -= ToggleConsole_performed;
            controls.Disable();
        }

        public void OpenConsole() {
            if (Time.timeScale > 0) {
                holder.SetActive(true);
                Time.timeScale = 0;
            }
        }

        public void CloseConsole() {
            holder.SetActive(false);
            Time.timeScale = 1;
        }

        public void SubmitCommand(string text) {
            string[] parts = text.Split(new char[] { ' ' }, 2);
            string command = parts[0], fullArgs = (parts.Length > 1) ? parts[1] : "";
            string[] args = fullArgs.Split(' ');

            Debug.Log($"Command: \"{command}\" Args: \"{fullArgs}\"");

            switch (command) {
                case "print":
                    AddTextToConsole(fullArgs);
                    break;
                case "damage":
                    if (args.Length < 1) {
                        AddTextToConsole($"<color=red>You must provide an amount of damage to deal!</color>");
                    }

                    try {
                        var damage = float.Parse(args[0]);
                        GameObject.FindGameObjectWithTag("Player").GetComponent<Living>().DealDamage(damage);
                        AddTextToConsole($"Dealt {damage} to the player");
                        CloseConsole();
                    } catch (FormatException) {
                        AddTextToConsole($"<color=red>Damage must be a number!</color>");
                    }
                    break;
                case "effect":
                    if (args.Length < 1) {
                        AddTextToConsole($"<color=red>Please specify an effect to apply!</color>");
                    }

                    switch (args[0]) {
                        case "healing":
                            GameObject.FindGameObjectWithTag("Player").GetComponent<Living>().healthEffects.Add(new HealthEffect() { 
                                secondsRemaining = 30,
                                effects = new Dictionary<string, object>() { { "healing", 5f } },
                                image = "Items/Bandage"
                            });
                            CloseConsole();
                            break;
                        case "bleeding":
                            GameObject.FindGameObjectWithTag("Player").GetComponent<Living>().healthEffects.Add(new HealthEffect() {
                                secondsRemaining = 60,
                                effects = new Dictionary<string, object>() { { "bleeding", -1f } },
                                image = "BloodDrop"
                            });
                            CloseConsole();
                            break;
                        default:
                            AddTextToConsole($"<color=red>That health effect was not found!</color>");
                            break;
                    }
                    break;
                default:
                    AddTextToConsole($"<color=red>No command \"{command}\" exists!</color>");
                    break;
            }
        }

        private void AddTextToConsole(string text) {
            console.text += $"\n{text}";
            StartCoroutine(ScrollToBottom());
        }

        private void Submit_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
            if (holder.activeSelf) {
                SubmitCommand(inputField.text);
                inputField.text = "";
            }
        }

        private void ToggleConsole_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
            if (holder.activeSelf) {
                CloseConsole();
            } else {
                OpenConsole();
            }
        }

        private IEnumerator ScrollToBottom() {
            yield return new WaitForEndOfFrame();
            scrollRect.verticalNormalizedPosition = 0;
        }
    }
}