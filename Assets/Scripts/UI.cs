using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI recordText;
    [HideInInspector] public List<int> addedPoints = new List<int>();

    [SerializeField] private Button _restartGameButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject gameOverText;
    private int _totalScore = 0;

    private int _listIndex = 0;
    
    #region Singleton Class: UI
    
    public static UI Instance;
    private void Awake()
    {
        Time.timeScale = 1f;
        
        if (Instance == null)
            Instance = this;
    }

    #endregion
    
    private void Start()
    {
        Button button = _restartGameButton.GetComponent<Button>();
        button.onClick.AddListener(RestartGame);
        recordText.text = "Record: " + PlayerPrefs.GetInt("record", 0).ToString();
    }

    private void Update()
    {
        // ReSharper disable once RedundantCheckBeforeAssignment
        if (_listIndex != addedPoints.Count)
        {
            _totalScore += addedPoints[_listIndex];
            _listIndex = addedPoints.Count;
            scoreText.text = "Puntos: " +_totalScore.ToString();
            recordText.text = "Record: " + PlayerPrefs.GetInt("record", 0).ToString();
            if(_totalScore > PlayerPrefs.GetInt("record", 0))
            {
                PlayerPrefs.SetInt("record", _totalScore);
                
            }
            recordText.text = "Record: " + PlayerPrefs.GetInt("record", _totalScore).ToString();
        }

        if (RedZone.Instance.isGameOver)
            GameOver();
    }

    private void GameOver()
    {
        restartButton.SetActive(true);
        gameOverText.SetActive(true);
        Time.timeScale = 0f;
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
    
}
