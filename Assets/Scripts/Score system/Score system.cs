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

    private const string Score = "Score";

    private bool stopTime = false;

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
        LoadResults();

        timer = PlayerName.timer;
        timerRunning = true;
        resultsPanel.SetActive(false);
    }

    void Update()
    {
        if (timerRunning && !stopTime)
        {
            timer += Time.deltaTime;

            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            PlayerName.timer = timer;
        }

        if (Input.GetKey("0") && Input.GetKey(KeyCode.LeftShift))
        {
            PlayerPrefs.DeleteKey(Score);
            Debug.Log("All scores were deleted");
            stopTime = true;
            PlayerName.playerName = "";
            PlayerName.timer = 0;
            allResults.Clear();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!timerRunning) return;
        if (!other.CompareTag("Player")) return;

        timerRunning = false;

        string playerName = string.IsNullOrWhiteSpace(PlayerName.playerName) ? "Unnamed" : PlayerName.playerName;
        allResults.Add(new PlayerResult(playerName, timer));

        ShowResults();
    }

    void ShowResults()
    {
        resultsPanel.SetActive(true);

        allResults.Sort((a, b) => a.time.CompareTo(b.time));

        resultsText.text = "Results:\n";

        foreach (var result in allResults)
        {
            int minutes = Mathf.FloorToInt(result.time / 60f);
            int seconds = Mathf.FloorToInt(result.time % 60f);
            resultsText.text += result.name + " - " + string.Format("{0:00}:{1:00}", minutes, seconds) + "\n";
        }

        PlayerName.timer = 0;
        PlayerName.playerName = "";
        PlayerPrefs.SetString(Score, resultsText.text);
        PlayerPrefs.Save();
    }

    void LoadResults()
    {
        allResults.Clear();

        if (PlayerPrefs.HasKey(Score))
        {
            string[] lines = PlayerPrefs.GetString(Score).Split('\n');
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split('-');
                if (parts.Length != 2) continue;

                string name = parts[0].Trim();
                string[] timeParts = parts[1].Trim().Split(':');
                if (timeParts.Length != 2) continue;

                if (int.TryParse(timeParts[0], out int minutes) &&
                    int.TryParse(timeParts[1], out int seconds))
                {
                    float time = minutes * 60f + seconds;
                    allResults.Add(new PlayerResult(name, time));
                }
            }
        }
    }
}




/*using System.Collections.Generic;
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

    private bool stopTime = false;

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

        if (PlayerName.timer==0)
        {
            timer = 0;
        }
        else
        {
            timer = PlayerName.timer;
        }

        timerRunning = true;
        resultsPanel.SetActive(false);
    }

    void Update()
    {
        if (timerRunning && stopTime==false)
        {
            timer += Time.deltaTime;

            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);

            // update timer text
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            PlayerName.timer = timer;
        }

        /*if (Input.GetKey("6") && Input.GetKey(KeyCode.LeftShift))
        {
            stopTime = true;
            PlayerName.playerName = "";
            Debug.Log("Current score session was deleted" + PlayerName.timer);
            //delete timer save
            PlayerName.timer = 0;

        }*/

//delete the score results
/* if (Input.GetKey("0") && Input.GetKey(KeyCode.LeftShift))
 {
     PlayerPrefs.DeleteKey("Score");
     Debug.Log("All scores were deleted");
     stopTime = true;
     PlayerName.playerName = "";
     PlayerName.timer = 0;
     allResults.Clear();
 }
}

void OnTriggerEnter(Collider other)
{
 if (!timerRunning) return;
 if (!other.CompareTag("Player")) return;

 timerRunning = false;

 // save result with fallback name
 string playerName = string.IsNullOrWhiteSpace(PlayerName.playerName) ? "Unnamed" : PlayerName.playerName;
 allResults.Add(new PlayerResult(playerName, timer));

 ShowResults();

}



void ShowResults()
{


 resultsPanel.SetActive(true);

 // Sorting
 allResults.Sort((a, b) => a.time.CompareTo(b.time));

 resultsText.text = "Results:\n";

 /*if (!PlayerPrefs.HasKey("Score"))
 {
     resultsText.text = "Results:\n";
 }
 else
 {
     resultsText.text = PlayerPrefs.GetString("Score");
 }    */


/*foreach (var result in allResults)
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
*/