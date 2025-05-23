using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoresystem : MonoBehaviour
{
    public Text timerText;
    public GameObject resultsPanel;
    public Text resultsText;

    private float timer = 0f;
    private bool timerRunning = false;

    private struct PlayerResult
    {
        public string name;
        public float time;

        public PlayerResult(string name, float time)
        {
            this.name = name;
            this.time = time;
        }
    }

    private static List<PlayerResult> allResults = new List<PlayerResult>();

    void Start()
    {
        timer = 0;
        timerRunning = true;
        resultsPanel.SetActive(false);
    }

    void Update()
    {
        if (timerRunning)
        {
            timer += Time.deltaTime;

            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);

            // update timer text
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!timerRunning) return;
        if (!other.CompareTag("Player")) return;

        timerRunning = false;

        // save result
        allResults.Add(new PlayerResult(PlayerName.playerName, timer));

        ShowResults();
    }

    void ShowResults()
    {
        resultsPanel.SetActive(true);

        // Sorting
        allResults.Sort((a, b) => a.time.CompareTo(b.time));

        resultsText.text = "Results:\n";

        foreach (var result in allResults)
        {
            int minutes = Mathf.FloorToInt(result.time / 60f);
            int seconds = Mathf.FloorToInt(result.time % 60f);

            resultsText.text += result.name + " - " + string.Format("{0:00}:{1:00}", minutes, seconds) + "\n";
        }
    }
}
