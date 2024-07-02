using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LevelData
{
    public int curNumberLevel;
    public int amountUnlockedCard;
    public LevelData(LevelManager level)
    {
        this.curNumberLevel = level.CurNumberLevel;
        this.amountUnlockedCard = level.AmountUnlockedCard;
    }
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int curNumberLevel;
    private RewardCard rewardCard;
    private PlayerManager playerManager;
    private Transform canvasTransform;
    [SerializeField] private int amountUnlockedCard;

    [SerializeField] private DataLevel_SO dataLevelSO;

    public Animator transitionAnimator;

    private void Awake()
    {
        this.rewardCard = GameObject.FindGameObjectWithTag("RewardCard").GetComponent<RewardCard>();
        this.playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        this.canvasTransform = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    public int CurNumberLevel { get => curNumberLevel; set => curNumberLevel = value; }
    public DataLevel_SO DataLevelSO { get => dataLevelSO; }
    public int AmountUnlockedCard { get => amountUnlockedCard; set => amountUnlockedCard = value; }

    public void WinLevel()
    {
        // Init reward card
        // Get card base on Level
        GameObject rewardCardIntance = Instantiate(this.rewardCard.GetRewardCard(this.CurNumberLevel), this.canvasTransform);
        rewardCardIntance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        if (rewardCardIntance.CompareTag("BirdCardReward"))
        {
            this.AmountUnlockedCard++;
        }

        // Increase Level Number
        this.curNumberLevel++;

        // Unlock card in collection
        this.playerManager.UnlockNewPlantCard(this.AmountUnlockedCard);
        
        //this.dataLevelSO.CurLevel = this.curNumberLevel;
        LevelDataHandler.instance.Save();
    }
    
    /*public void LoadDataSO()
    {
        // Load from Scriptable object
        this.curNumberLevel = this.DataLevelSO.CurLevel;
    }*/
    public void LoadData(LevelData data)
    {
        this.curNumberLevel = data.curNumberLevel;
        this.amountUnlockedCard = data.amountUnlockedCard;
    }

    public void OnNextLevel()
    {
        GameManager.instance.ResumeGame();
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        this.transitionAnimator.SetTrigger("start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
