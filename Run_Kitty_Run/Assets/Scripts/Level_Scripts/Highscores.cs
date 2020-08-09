using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Highscores : MonoBehaviour
{
    // public/private code and url fpr dreamlo tool
    const string privateCode = "abacLLCKrUObw9RV-oSc4AeOdUIHig6UiyLOoWzpKXRA";
    const string publicCode = "5ef9e938377eda0b6c8d9308";
    const string webUrl = "http://dreamlo.com/lb/";
    public Highscore[] highscoresList;

    //used to delete playerprefs for testing, should be deleted in last build
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
    }

    //adds highscore with username and score
    public void AddScore(string userName, int score)
    {
        StartCoroutine(UploadHighscore(userName, score));
    }

    //uploads highscore to dreamlo
    public IEnumerator UploadHighscore(string userName, int score)
    {
        UnityWebRequest url = UnityWebRequest.Get(webUrl + privateCode + "/add/" + UnityWebRequest.EscapeURL(userName) + "/" + score);
        yield return url.SendWebRequest();
        if (url.isNetworkError || url.isHttpError)
        {
            
            Debug.Log("Upload failed:" + url.error);
        }

        else
        {
            Debug.Log("Uploaded " + userName + "s score");
        }
    }


    //deletes highscore from dreamlo
    public IEnumerator DeleteHighscore(string userName)
    {


        UnityWebRequest url = UnityWebRequest.Get(webUrl + privateCode + "/delete/" + UnityWebRequest.EscapeURL(userName));
        yield return url.SendWebRequest();
        if (url.isNetworkError || url.isHttpError)
        {
            Debug.Log("Deletion failed:" + url.error);
        }
        else
        {
            Debug.Log("Deleted " + userName + "s score");
            
        }
    }

   
    //downloads highscores from dreamlo
    public IEnumerator DownloadHighscores()
    {
        Debug.Log("start download");
        UnityWebRequest url = UnityWebRequest.Get(webUrl + publicCode + "/pipe");
        yield return url.SendWebRequest();
        if (url.isNetworkError||url.isHttpError)
        {
            Debug.Log("Download failed:" + url.error);
           
            
        }
        else
        {
            Debug.Log(url.downloadHandler.text);
            FormatHighscore(url.downloadHandler.text);
        }
    }

    //formats downloaded dreamlo highscores to be used for the end screen
    public void FormatHighscore(string textStream)
    {
        string[] scores = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[scores.Length];
        int i = 0;
        foreach (string score in scores)
        {
            string[] scoreEntry = score.Split(new char[] { '|' });
            string username = scoreEntry[0];

            int seconds = int.Parse(scoreEntry[1]);
            
            int minutes = seconds / 60;
            seconds = seconds %60;

            string highscore = minutes.ToString("00") + ":" + seconds.ToString("00.00"); ;

            highscoresList[i] = new Highscore(username, highscore);
            i++;
            
        }
        
    }
}
