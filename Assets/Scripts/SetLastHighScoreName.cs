using UnityEngine;
using System.Collections.Generic;

public class SetLastHighScoreName : MonoBehaviour
{
    public void SetHighScoreName()
    {
        var text = GetComponent<dfTextbox>();

        text.Text = GlobalPreferences.LastHighScoreName;
    }
}