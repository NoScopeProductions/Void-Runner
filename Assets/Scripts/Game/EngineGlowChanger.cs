using UnityEngine;
using System.Collections;

public class EngineGlowChanger : MonoBehaviour {


    public Player player;

    public Texture RegularGlow;
    public Texture BoostGlow;

    private Texture Current;
	
	public void Update () {
        CheckTexture();
	}

    public void CheckTexture()
    {
        if (player.State == Player.PlayerState.BOOSTING ||
            player.State == Player.PlayerState.DEACTIVATING_BOOST ||
            player.State == Player.PlayerState.OVER_GAP)
        {
            if (Current != BoostGlow)
            {
                Current = BoostGlow;
                renderer.material.mainTexture = Current;
            }
        }
        else
        {
            if (Current != RegularGlow)
            {
                Current = RegularGlow;
                renderer.material.mainTexture = Current;
            }
        }
    }
}
