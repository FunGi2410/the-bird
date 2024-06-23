using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LevelData
{
    public int curNumberLevel;

    public LevelData(LevelManager level)
    {
        this.curNumberLevel = level.CurNumberLevel;
    }
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int curNumberLevel;
    private RewardCard rewardCard;
    private PlayerManager playerManager;
    private Transform canvasTransform;

    [SerializeField] private DataLevel_SO dataLevelSO;

    private void Awake()
    {
        //this.LoadData();

        this.rewardCard = GameObject.FindGameObjectWithTag("RewardCard").GetComponent<RewardCard>();
        this.playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        this.canvasTransform = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    public int CurNumberLevel { get => curNumberLevel; set => curNumberLevel = value; }
    public DataLevel_SO DataLevelSO { get => dataLevelSO; }

    public void WinLevel()
    {
        // Init reward card
        // Get card base on Level
        GameObject rewardCardIntance = Instantiate(this.rewardCard.GetRewardCard(this.CurNumberLevel), this.canvasTransform);
        rewardCardIntance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        // Unlock card in collection
        this.playerManager.UnlockNewPlantCard(this.CurNumberLevel);

        // Increase Level Number
        this.dataLevelSO.CurLevel = ++this.curNumberLevel;
    }

    public void LoadData(LevelData data)
    {
        //this.curNumberLevel = this.DataLevelSO.CurLevel;
        this.curNumberLevel = data.curNumberLevel;
    }

    public void OnNextLevel()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
