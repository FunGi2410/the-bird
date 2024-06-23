using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject draggingObj;
    public GameObject currentContaiter;
    public static GameManager instance;

    public static UnityAction startGame;

    private float curTimerGame;
    private bool isStartGame = false;
    private bool isGameOver = false;
    private bool isPauseGame = false;

    [SerializeField] private const int ONESUNSCORE = 25;
    [SerializeField] private int sunScore;

    public int SunScore { get => sunScore; set => sunScore = value; }
    public float CurTimerGame { get => curTimerGame; set => curTimerGame = value; }
    public bool IsStartGame { get => isStartGame; set => isStartGame = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public bool IsPauseGame { get => isPauseGame; set => isPauseGame = value; }

    private void Awake()
    {
        instance = this;

        ResumeGame();
    }

    
    public IEnumerator StartCountTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            CurTimerGame++;
            UIManager.instance.TimeGameText.text = CurTimerGame.ToString();
        }
    }

    public void AddSun()
    {
        this.SunScore += ONESUNSCORE;
        UIManager.instance.SunScoreText.text = this.SunScore.ToString();
    }

    public void SubSun(int priceSun)
    {
        this.SunScore -= priceSun;
        UIManager.instance.SunScoreText.text = this.SunScore.ToString();
    }

    public bool PlaceObject()
    {
        if (this.draggingObj != null && this.currentContaiter != null)
        {
            GameObject objPlace = Instantiate(this.draggingObj.GetComponent<ObjectDragging>().card.objectGame, this.currentContaiter.transform);
            objPlace.transform.localPosition = Vector2.zero;
            this.currentContaiter.GetComponent<ObjectContainer>().isFull = true;
            this.currentContaiter.GetComponent<Collider2D>().enabled = false;
            return true;
        }
        return false;
    }

    public void OnGameOver()
    {
        this.IsGameOver = true;
        this.PauseGame();
        UIManager.instance.DisplayGameOverPanel();
    }

    public void OnGameWin()
    {
        this.IsGameOver = true;
        this.PauseGame();
        UIManager.instance.DisplayGameWinPanel();
    }

    public void OnStartGame()
    {
        this.IsStartGame = true;
        startGame?.Invoke();
        StartCoroutine(StartCountTimer());
    }

    public void PauseGame()
    {
        this.IsPauseGame = true;
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        this.IsPauseGame = false;
        Time.timeScale = 1;
    }
}
