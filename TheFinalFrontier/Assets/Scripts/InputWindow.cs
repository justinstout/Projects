using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InputWindow : MonoBehaviour
{
    public GameObject NameSubmitted;
    private Button OkButton;
    private TMP_InputField InputField;


    void Awake() {
        OkButton = transform.Find("OkButton").GetComponent<Button>();
        InputField = transform.Find("InputField").GetComponent<TMP_InputField>();
    }

    void Start() {
        Show((string inputText) => {
            Debug.Log("Ok clicked: " + inputText);
            HighScoreTable.Name = inputText;
        }, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 3);
    }

    public void Show(Action<string> onOk, string validCharacters, int charLimit) {
        gameObject.SetActive(true);
        NameSubmitted.SetActive(false);

        InputField.onValidateInput = (string text, int charIndex, char addedChar) => {
            return ValidateChar(validCharacters, addedChar);
        };

        InputField.characterLimit = charLimit;

        OkButton.onClick.AddListener(delegate {
            Hide();
            onOk(InputField.text);
        });
    }

    public void Hide() {
        gameObject.SetActive(false);
        NameSubmitted.SetActive(true);
    }

    private char ValidateChar(string validChars, char addedChar) {
        if (validChars.IndexOf(addedChar) != -1) {
            return addedChar;
        } else {
            return '\0';
        }
    }
}
