using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    // Variables
    public Text seedsText;
    [SerializeField] private GameObject[] arrayCollectibles;
    [SerializeField] private int collectiblesNum;
    [SerializeField] private int collectiblesMax;
    
    [SerializeField] private Timer_Controller _timerController;
    [SerializeField] private GameObject timer_txt;

    private Enemy_Controller _enemyController;
    private Player_Controller _playerController;

    private void Awake()
    {
        Instance = this;
        _enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy_Controller>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
    }
    
    void Start()
    {
        // Detección de los collectibles por tag
        arrayCollectibles = GameObject.FindGameObjectsWithTag("Collectable");
        
        // Detección del timer por tag
        timer_txt = GameObject.FindGameObjectWithTag("Timer");
        
        // Detección de cuantos objetos tenemos en la lista
        collectiblesMax = arrayCollectibles.Length;
        
        UpdatePickablesScore();
    }

    private void Update()
    {
        WinLogic();
    }

    #region PickUps
    private void UpdatePickablesScore()
    {
        // Texto en la UI
        seedsText.text = collectiblesNum + " / " + collectiblesMax;
    }

    public void IncreaseScore()
    {
        collectiblesNum++;
        UpdatePickablesScore();
    }
    #endregion

    #region Start Game Method
    public void StartGame()
    {
        timer_txt.SetActive(true);
        _timerController.startTimer();

        _enemyController.enabled = true;
        _playerController.enabled = true;
    }
    #endregion
    
    #region End Game Method
    public void EndGame()
    {
        timer_txt.SetActive(false);

        _enemyController.enabled = false;
        _playerController.enabled = false;
        _playerController.anim.enabled = false;

        StartCoroutine(ChangeSceneToLose());
    }

    IEnumerator ChangeSceneToLose()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Lose");
    }
    

    #endregion
    
    #region WIN LOGIC

    private void WinLogic()
    {
        if (collectiblesNum == collectiblesMax)
        {
            StartCoroutine(ChangeSceneToWin());
        }
    }
    
    IEnumerator ChangeSceneToWin()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Win");
    }

    #endregion

    #region Try Again
    public void TryAgain()
    {
        StartCoroutine(ChangeSceneToPlayMenu());
    }
    IEnumerator ChangeSceneToPlayMenu()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("TestArea");
    }
    #endregion

    #region Quit Game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game succesful");
    }
    #endregion

}
