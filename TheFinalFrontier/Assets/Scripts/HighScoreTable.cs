using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform EntryContainer;
    private Transform EntryTemplate;
    private List<HighScoreEntry> HighScoreEntryList;
    private List<Transform> HighScoreEntryTransformList; 
    public static string Name;
    private HUD HUD;
    private int ParsedScore;
    private int HighScoreListLimit;
    void Awake() {
        EntryContainer = transform.Find("HighScoreEntryContainer");
        EntryTemplate = EntryContainer.Find("HighScoreEntryTemplate");
        EntryTemplate.gameObject.SetActive(false);
        HUD = HUD.Instance;

        int.TryParse(HUD.ScoreText.text.Substring(7), out ParsedScore);
        AddHighScore(ParsedScore, Name); //!!!!

        string jsonString = PlayerPrefs.GetString("HighScoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        for (int i = 0; i < highscores.HighScoreEntryList.Count; i++) {
            for (int j = i + 1; j < highscores.HighScoreEntryList.Count; j++) {
                if (highscores.HighScoreEntryList[j].score > highscores.HighScoreEntryList[i].score) {
                    HighScoreEntry tmp = highscores.HighScoreEntryList[i];
                    highscores.HighScoreEntryList[i] = highscores.HighScoreEntryList[j];
                    highscores.HighScoreEntryList[j] = tmp;
                }
            }
        }

        HighScoreEntryTransformList = new List<Transform>();
        for (int i = 0; i < 10; i++) {
            CreateHighScoreEntry(highscores.HighScoreEntryList[i], EntryContainer, HighScoreEntryTransformList);
        }

        string json = JsonUtility.ToJson(highscores.HighScoreEntryList);
        PlayerPrefs.SetString("HighScoreTable", json);
        PlayerPrefs.Save();
    }

    void Update() {
        
    }

    private void CreateHighScoreEntry(HighScoreEntry hse, Transform container, List<Transform> transformList) {
        float templateHeight = 23.0f;
        Transform entryTransform = Instantiate(EntryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, 10 - (templateHeight * transformList.Count));
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank) {
            default: rankString = rank + "th"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        entryTransform.Find("RankEntry").GetComponent<Text>().text = rankString;
        entryTransform.Find("ScoreEntry").GetComponent<Text>().text = hse.score.ToString();
        entryTransform.Find("NameEntry").GetComponent<Text>().text = hse.name;

        transformList.Add(entryTransform);
    }

    public static void AddHighScore(int score, string name) {
        HighScoreEntry highScoreEntry = new HighScoreEntry{score = score, name = name};

        string jsonString = PlayerPrefs.GetString("HighScoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        highscores.HighScoreEntryList.Add(highScoreEntry); //!!!

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("HighScoreTable", json);
        PlayerPrefs.Save();
    }

    private class HighScores {
        public List<HighScoreEntry> HighScoreEntryList;
    }
    
    [System.Serializable]
    private class HighScoreEntry {
        public int score;
        public string name;
    }
}
