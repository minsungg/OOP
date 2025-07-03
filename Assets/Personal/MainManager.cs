using Newtonsoft.Json.Bson;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    string path = Application.dataPath + "/save.json";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Field");
    }

    public void ScoreButton(GameObject ScoreBoard, TextMeshProUGUI PlatScore, TextMeshProUGUI TopScore)
    {
        ScoreBoard.SetActive(true);

        ScoreData data = LoadData();
        PlatScore.text = "Platformer\n" + data._PlatScore;
        TopScore.text = "TopDown\n" + data._TopScore;
    }

    public void CloseButton(GameObject ScoreBoard)
    {
        ScoreBoard.SetActive(false);
    }

    [System.Serializable]
    public class ScoreData
    {
        public int _PlatScore, _TopScore;

    }

    public void SaveData(ScoreData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public ScoreData LoadData()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<ScoreData>(json);
        }

        ScoreData data = new ScoreData();
        data._PlatScore = data._TopScore = 0;
        return data;
    }
}
