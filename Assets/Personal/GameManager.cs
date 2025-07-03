using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private MainManager Instance;

    private Button StartButton, ScoreButton, CloseButton;
    private GameObject ScoreBoard;
    private TextMeshProUGUI PlatText, TopText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = GameObject.Find("MainManager").GetComponent<MainManager>();

        StartButton = GameObject.Find("Start").GetComponent<Button>();
        ScoreButton = GameObject.Find("Score").GetComponent<Button>();

        ScoreBoard = GameObject.Find("Canvas").transform.Find("ScoreBoard").gameObject;

        CloseButton = ScoreBoard.transform.Find("Close").gameObject.GetComponent<Button>();

        PlatText = ScoreBoard.transform.Find("PlatformerScore").gameObject.GetComponent<TextMeshProUGUI>();
        TopText = ScoreBoard.transform.Find("TopDownScore").gameObject.GetComponent<TextMeshProUGUI>();

        Debug.Log("Instance : " + Instance != null);
        Debug.Log("Start : " + StartButton != null);
        Debug.Log("Score : " + ScoreButton != null);
        Debug.Log("Close : " + CloseButton != null);

        StartButton.onClick.AddListener(Instance.StartButton);
        ScoreButton.onClick.AddListener(() => Instance.ScoreButton(ScoreBoard, PlatText, TopText));
        CloseButton.onClick.AddListener(() => Instance.CloseButton(ScoreBoard));
    }
}
