using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores : MonoBehaviour
{
    const string privateCode = "abacLLCKrUObw9RV-oSc4AeOdUIHig6UiyLOoWzpKXRA";
    const string publicCode = "5ef9e938377eda0b6c8d9308";
    const string webUrl = "http://dreamlo.com/lb/";
    public Highscore[] highscoresList;

    private void Awake()
    {
        AddScore("kays", 1010);
        AddScore("kas", 22);
        AddScore("kys", 100);


        DownloadScores();

    }
    public struct Highscore
    {
        public string userName;
        public string score;
        public Highscore(string userName, string score)
        {
            this.userName = userName;
            this.score = score;
        }
    }

    public void AddScore(string userName, int score)
    {
        StartCoroutine(UploadHighscore(userName, score));
    }
 IEnumerator UploadHighscore(string userName, int score)
    {
        WWW url = new WWW(webUrl + privateCode + "/add/" + WWW.EscapeURL(userName) + "/0/" + score);
        yield return url;
        if (string.IsNullOrEmpty(url.error))
        {
            Debug.Log("Uploaded "+ userName + "s score");
        }
        else
        {
            Debug.Log("Upload failed:" + url.error);
        }
    }

    public void DownloadScores()
    {
        StartCoroutine("DownloadHighscores");
    }
    IEnumerator DownloadHighscores()
    {
        Debug.Log("start download");
        WWW url = new WWW(webUrl + publicCode + "/pipe");
        yield return url;
        if (string.IsNullOrEmpty(url.error))
        {
            Debug.Log(url.text);
        }
        else
        {
            Debug.Log("Download failed:" + url.error);
        }
    }
    public void FormatHighscore(string textStream)
    {
        string[] scores = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        int i = 0;
        foreach (string score in scores)
        {
            string[] scoreEntry = score.Split(new char[] { '|' });
            string username = scoreEntry[0];

            int seconds = int.Parse(scoreEntry[1]);
            int minutes = seconds % 60;
            seconds = seconds - (60 * minutes);

            string highscore = minutes.ToString("00") + ":" + seconds.ToString("00.00"); ;

            highscoresList[i] = new Highscore(username, score);
            i++;
            Debug.Log("username: " + highscoresList[i].userName + " score: " + highscoresList[i].score);
        }
    }
}
