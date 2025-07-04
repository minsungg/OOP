using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public List<GameObject> rainPrefabs;
    private Vector3 randomPos;

    public int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnRain", 2.0f, 1.0f);
        randomPos = new Vector3(0, 6, 0);
    }

    private void SpawnRain()
    {
        score += 1;
        randomPos.x = Random.Range(-8.0f, 8.0f);
        Instantiate(rainPrefabs[Random.Range(0, rainPrefabs.Count)], randomPos, Quaternion.identity);
    }

    public void GameOver()
    {
        int p = GameObject.Find("MainManager").GetComponent<MainManager>().LoadData()._PlatScore;
        int t = GameObject.Find("MainManager").GetComponent<MainManager>().LoadData()._TopScore;
        if (p < score)
        {
            GameObject.Find("MainManager").GetComponent<MainManager>().SaveData(score, t);
        }
        SceneManager.LoadScene("Field");
    }
}
