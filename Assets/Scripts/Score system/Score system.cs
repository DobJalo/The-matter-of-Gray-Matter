using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoresystem : MonoBehaviour
{
    public Text timerText;
    public GameObject resultsPanel;
    public TMP_Text resultsText;

    private float timer = 0f;
    private bool timerRunning = false;

    private const string Score = "Score"; //save slot

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
        if(PlayerName.timer==0)
        {
            timer = PlayerName.timer;
        }
        else
        {
            timer = 0;
        }

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

            PlayerName.timer = timer;
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

        if (!PlayerPrefs.HasKey("Score"))
        {
            resultsText.text = "Results:\n";
        }
        else
        {
            resultsText.text = PlayerPrefs.GetString("Score");
        }    


            foreach (var result in allResults)
            {
                int minutes = Mathf.FloorToInt(result.time / 60f);
                int seconds = Mathf.FloorToInt(result.time % 60f);

                resultsText.text += result.name + " - " + string.Format("{0:00}:{1:00}", minutes, seconds) + "\n";
            }

        //delete timer save
        PlayerName.timer = 0;

        //save results as a save
        PlayerName.playerName = "";
        PlayerPrefs.SetString(Score, resultsText.text);
        PlayerPrefs.Save();
    }
}
