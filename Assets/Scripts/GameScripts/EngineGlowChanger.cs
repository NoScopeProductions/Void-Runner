﻿using UnityEngine;
using System.Collections;

public class EngineGlowChanger : MonoBehaviour {


    public Player player;

    public Texture RegularGlow;
    public Texture BoostGlow;

    private Texture Current;

	// Use this for initialization
	public void Start () {
        
	}
	
	// Update is called once per frame
	public void Update () {
        CheckTexture();
	}

    public void CheckTexture()
    {
        if (player.State == Player.PlayerState.BOOSTING ||
            player.State == Player.PlayerState.DEACTIVATING_BOOST)
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