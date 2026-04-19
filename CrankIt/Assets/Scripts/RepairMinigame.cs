using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class RepairMinigame : MonoBehaviour
{
    public static RepairMinigame Instance;

    [Header("UI")]
    public GameObject panel;
    public TMP_Text sequenceText;
    public TMP_Text feedbackText;

    [Header("Paramètres")]
    public int sequenceLength = 4;

    KeyCode[] possibleKeys = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
    string[] arrows = { "↑", "↓", "←", "→" };

    List<KeyCode> sequence = new List<KeyCode>();
    int currentIndex = 0;
    bool active = false;

    PipelineRenderer targetPipe;
    Action onSuccess;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void StartRepair(PipelineRenderer pipe, Action callback)
    {
        targetPipe = pipe;
        onSuccess = callback;
        GenerateSequence();
        panel.SetActive(true);
        active = true;
        feedbackText.text = "";
    }

    void GenerateSequence()
    {
        sequence.Clear();
        currentIndex = 0;
        string display = "";

        for (int i = 0; i < sequenceLength; i++)
        {
            int r = Random.Range(0, possibleKeys.Length);
            sequence.Add(possibleKeys[r]);
            display += arrows[r] + " ";
        }

        sequenceText.text = display.Trim();
    }

    void Update()
    {
        if (!active) return;

        foreach (KeyCode key in possibleKeys)
        {
            if (Input.GetKeyDown(key))
            {
                if (key == sequence[currentIndex])
                {
                    currentIndex++;
                    feedbackText.text = "✓";
                    feedbackText.color = Color.green;

                    if (currentIndex >= sequenceLength)
                        Repaired();
                }
                else
                {
                    // Mauvaise touche → reset
                    currentIndex = 0;
                    feedbackText.text = "✗";
                    feedbackText.color = Color.red;
                    GenerateSequence();
                }
                break;
            }
        }
    }

    void Repaired()
    {
        active = false;
        panel.SetActive(false);
        targetPipe.SetState(PipeState.Empty);
        onSuccess?.Invoke();
    }
}
