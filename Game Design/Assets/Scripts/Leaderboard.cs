using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public TMP_InputField teamNameInput;
    public TextMeshProUGUI leaderboardDisplay;
    public TextMeshProUGUI scoreText;
    public Button submitBtn;
    List<PlayerInfo> highscoreArraySession;
    public string highscoreArraySaved;

    public class PlayerInfo
    {
        public string teamName;
        public int score;

        public PlayerInfo(string teamName, int score)
        {
            this.teamName = teamName;
            this.score = score;
        }
    }

    private void Start()
    {
        DisplayScore();
        highscoreArraySession = new List<PlayerInfo>();
        LoadLeaderboard();
        
        if(highscoreArraySession.Count > 0)
        {
            if (ScoreManager.Score <= highscoreArraySession[highscoreArraySession.Count - 1].score &&
                highscoreArraySession.Count == 5)
            {
                teamNameInput.gameObject.SetActive(false);
                submitBtn.gameObject.SetActive(false);
            }
            else
            {
                submitBtn.onClick.AddListener(delegate { SaveScore(ScoreManager.Score.ToString(), teamNameInput.text, teamNameInput); });
            }
        }
        else
        {
            submitBtn.onClick.AddListener(delegate { SaveScore(ScoreManager.Score.ToString(), teamNameInput.text, teamNameInput); });
        }


    }

    private void Update()
    {
        ClearLeaderboard();
    }

    void DisplayScore()
    {
        scoreText.text = "Your team's score:        " + ScoreManager.Score;
    }

    void SaveScore(string score, string teamName, TMP_InputField nameInput)
    {
        if(nameInput.text != "")
        {
            PlayerInfo highscore = new PlayerInfo(teamName, int.Parse(score));
            highscoreArraySession.Add(highscore);

            teamNameInput.text = "";
            teamNameInput.gameObject.SetActive(false);
            submitBtn.gameObject.SetActive(false);

            SortLeaderboard();
        }
        else
        {
            nameInput.selectionColor = Color.red;
        }
    }

    void SortLeaderboard()
    {
        for(int i = highscoreArraySession.Count -1; i > 0; i--)
        {
            if(highscoreArraySession[i].score > highscoreArraySession[i - 1].score)
            {
                PlayerInfo tempInfo = highscoreArraySession[i - 1];

                highscoreArraySession[i - 1] = highscoreArraySession[i];
                highscoreArraySession[i] = tempInfo;

            }
            
        }

        if (highscoreArraySession.Count > 5)
        {
            highscoreArraySession.RemoveAt(5);
        }

        UpdatePlayerPrefsString();
    }

    void UpdatePlayerPrefsString()
    {

        highscoreArraySaved = "";

        for (int i = 0; i < highscoreArraySession.Count; i++)
        {
            highscoreArraySaved += highscoreArraySession[i].teamName + ",";
            highscoreArraySaved += highscoreArraySession[i].score + ",";
        }

        PlayerPrefs.SetString("Saved Scores", highscoreArraySaved);

        UpdateLeaderboardVisual();
    }

    void UpdateLeaderboardVisual()
    {
        leaderboardDisplay.text = "";

        for (int i = 0; i <= highscoreArraySession.Count - 1; i++)
        {
            leaderboardDisplay.text += highscoreArraySession[i].teamName + "  :  " + highscoreArraySession[i].score + " \n";
        }
    }

    void LoadLeaderboard()
    {
        highscoreArraySaved = PlayerPrefs.GetString("Saved Scores", "");

        string[] highscoreArraySaved2 = highscoreArraySaved.Split(',');

        for(int i = 0; i < highscoreArraySaved2.Length - 2; i += 2)
        {
            PlayerInfo loadedInfo = new PlayerInfo(highscoreArraySaved2[i], int.Parse(highscoreArraySaved2[i + 1]));

            highscoreArraySession.Add(loadedInfo);

            UpdateLeaderboardVisual();
        }
    }

    void ClearLeaderboard()
    {
        if(Input.GetKeyDown (KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();

            leaderboardDisplay.text = "";
        }
    }
}