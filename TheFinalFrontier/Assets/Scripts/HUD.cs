using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    private static HUD _instance;
    public Text ScoreText;
    public Text CoinText;
    public Text WaveText;
    private Scene CurrentScene;

    public static HUD Instance {
        get {
            return _instance;
        }
    }

    void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this);
        }
        Debug.Log(ScoreText);

    }

    public void Render(int score, int coinAmount, int waveNumber) {
        ScoreText.text = String.Format("Score: {0}", score);
        CoinText.text = String.Format("Coins: {0}", coinAmount);
        WaveText.text = String.Format("Wave: {0}", waveNumber);

        // if (CurrentScene.name == "GameOver") {
        //     HighScoreTable.AddHighScore(score, "NMP");
        // }
    }
}
