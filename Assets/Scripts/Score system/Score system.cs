using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI
using TMPro; // to work with TextMeshPro

public class Scoresystem : MonoBehaviour
{
    public Text timerText; // timer (minutes, seconds) UI
    public GameObject resultsPanel; // panel with results of all players
    public TMP_Text resultsText; // text with these results

    private float timer = 0f;
    private bool timerRunning = false;

    private const string Score = "Score"; // slot to save score

    private bool stopTime = false;

    //checkpoint:
    private const string CheckpointKey = "Checkpoint";

    // sctruct to store an information about player
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

    //List with all results (all players)
    private static List<PlayerResult> allPlayersResults = new List<PlayerResult>();

    void Start()
    {
        LoadResults();

        timer = PlayerName.timer; // get timer value from static variable from another script
        timerRunning = true; // start timer
        resultsPanel.SetActive(false); // show panel with results
    }

    void Update()
    {
        if (timerRunning && !stopTime) // if timer still running
        {
            timer += Time.deltaTime; // add time

            //calculate minutes and seconds
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);

            //show timer value in this format
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            // update static variable
            PlayerName.timer = timer;
        }

        //shortcut for me to test score system
        //it deletes all scores
        if (Input.GetKey("0") && Input.GetKey(KeyCode.LeftShift)) 
        {
            PlayerPrefs.DeleteKey(Score); // delete save slot
            Debug.Log("All scores were deleted"); // inform in console
            stopTime = true; // stol time
            PlayerName.playerName = ""; // delete static variable - player name
            PlayerName.timer = 0; // delete static variable - player time
            allPlayersResults.Clear(); // clear all scores
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerPrefs.GetInt("Checkpoint") != 5)
        {
            PlayerPrefs.SetInt(CheckpointKey, 5);
            PlayerPrefs.Save();
        }


        if (!timerRunning) return;
        if (!other.CompareTag("Player")) return;

timerRunning = false; // stop timer

//use "No name" if there are no name
string playerName = string.IsNullOrWhiteSpace(PlayerName.playerName) ? "No name" : PlayerName.playerName;

// add player name and their time value to the list
allPlayersResults.Add(new PlayerResult(playerName, timer));

ShowResults();
    }

    void ShowResults()
{
    resultsPanel.SetActive(true); // show panel with results

    allPlayersResults.Sort((a, b) => a.time.CompareTo(b.time)); // sort list based on players' time

    resultsText.text = "Results:\n"; // add this text

    //add all of the results to the text
    foreach (var result in allPlayersResults)
    {
        int minutes = Mathf.FloorToInt(result.time / 60f);
        int seconds = Mathf.FloorToInt(result.time % 60f);
        resultsText.text += result.name + " - " + string.Format("{0:00}:{1:00}", minutes, seconds) + "\n";
    }

    PlayerName.timer = 0; // delete time
    PlayerName.playerName = ""; // delete name

    // save results
    PlayerPrefs.SetString(Score, resultsText.text);
    PlayerPrefs.Save();
}

void LoadResults()
{
    allPlayersResults.Clear(); // clear results list

    if (PlayerPrefs.HasKey(Score)) // if there is a saved information with scores
    {
        //split string into lines
        string[] lines = PlayerPrefs.GetString(Score).Split('\n');

        // check every line (name and time)
        foreach (string line in lines)
        {
            // if string is empty - skip
            if (string.IsNullOrWhiteSpace(line)) continue;

            //separate current line with "-" (name - time) 
            var parts = line.Split('-');

            // if line doesn't have name OR time - skip
            if (parts.Length != 2) continue;

            // get player name and delete extra spaces
            string name = parts[0].Trim();

            //separate time with symbol ":" (00:00)
            string[] timeParts = parts[1].Trim().Split(':');

            //if it doesn't have minutes OR seconds - skip
            if (timeParts.Length != 2) continue;

            // transform string to int
            if (int.TryParse(timeParts[0], out int minutes) &&
                int.TryParse(timeParts[1], out int seconds))
            {
                // form time using both minutes and seconds
                float time = minutes * 60f + seconds;

                // add result to all results
                allPlayersResults.Add(new PlayerResult(name, time));
            }
        }
    }
}
}
