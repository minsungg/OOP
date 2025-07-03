using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    public GameObject EnemyPrefab;
    private float spawnRate = 1.0f;

    private TextMeshProUGUI ScoreText;

    public int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("EnemySpawn", 1.0f, 1/spawnRate);

        ScoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "" + score;
    }

    private void EnemySpawn()
    {
        Instantiate(EnemyPrefab, new Vector3(Random.Range(-5, 5), Random.Range(-10, 10), 0), EnemyPrefab.transform.rotation);
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
