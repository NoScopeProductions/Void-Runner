using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class LeaderboardsEvents : MonoBehaviour 
{

    public dfLabel RankLabels;
    public dfLabel NameLabels;
    public dfLabel ScoreLabels;

	public void OnIsVisibleChanged( dfControl control, bool value )
	{
        if (value)
        {
            //it's visible
            RankLabels.Text = "";
            NameLabels.Text = "";
            ScoreLabels.Text = "";
            StartCoroutine(FillLeaderboard());
        }
	}

    private IEnumerator FillLeaderboard()
    {
        var query = ParseObject.GetQuery("HighScoreObject").OrderByDescending("Score").FindAsync();

        //Wait for the query to finish.
        while(!query.IsCompleted) { yield return null; }

        IEnumerable<ParseObject> Players = query.Result;

        int rank = 1;
        foreach (var p in Players)
        {
            RankLabels.Text += rank++ + "\n";
            NameLabels.Text += p["Name"] + "\n";
            ScoreLabels.Text += string.Format("{0:N0}\n", p["Score"]);
        }

        while (rank <= 100)
        {
            RankLabels.Text += rank++ + "\n";
            NameLabels.Text += "NoScope" + "\n";
            ScoreLabels.Text += "0\n";
        }
    }

}
