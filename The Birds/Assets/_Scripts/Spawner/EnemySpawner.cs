using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    private const int MAX_AMOUNT_SPAWNER_POS = 5;

    [SerializeField] private List<EnemyWaves> enemyWaves;
    private int indexEnemy = 0;
    private int ranPosSpawner;

    float timer = 0f;
    [SerializeField] float startSpawnTime;

    [SerializeField] int aliveAmountEnemyCurrent;
    private int indexWave = 0; // increse 1 

    private LevelManager levelManager;

    public int AliveAmountEnemyCurrent { 
        get => aliveAmountEnemyCurrent; 
        set
        {
            if(value >= 0) aliveAmountEnemyCurrent = value;
        }
    }

    private void Awake()
    {
        this.levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        EnemyCtrl.OnEnemyDead += EnemyDead;
    }
    private void Start()
    {
        this.LoadEnemySpawnerData();
    }

    private void Update()
    {
        if (GameManager.instance.CurTimerGame < this.startSpawnTime) return;
        this.timer += Time.deltaTime;
        if (this.timer >= this.enemyWaves[indexWave].NextSpawnTime)
        {
            this.Spawn();
            this.timer = 0f;
        }
    }

    void LoadEnemySpawnerData()
    {
        //print("Load enemy spawner");
        this.enemyWaves = this.levelManager.DataLevelSO.EnemySpawnerLevelSO[this.levelManager.CurNumberLevel].enemyWaves;
    }

    void EnemyDead()
    {
        this.AliveAmountEnemyCurrent--;

        if (this.AliveAmountEnemyCurrent <= 0 && this.enemyWaves[indexWave].Enemies.Count <= 0)
        {
            GameManager.instance.OnGameWin();
            this.levelManager.WinLevel();
            GameManager.instance.PauseGame();
        }
    }

    protected override void Spawn()
    {
        if (this.CanSpawn())
        {
            // Active object
            //Debug.Log("Enemy Spawned");
            GameObject enemyObj = InstanceObject(this.enemyWaves[indexWave].Enemies[this.indexEnemy].type);

            // Set position
            enemyObj.transform.SetParent(transform.GetChild(this.ranPosSpawner).transform);
            enemyObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            // inscrese amount enemy alive in game
            this.AliveAmountEnemyCurrent++;
            this.enemyWaves[indexWave].Enemies[indexEnemy].amount--;
        }
    }

    bool CanSpawn()
    {
        if (this.enemyWaves[indexWave].Enemies.Count > 0)
        {
            this.indexEnemy = Random.Range(0, this.enemyWaves[indexWave].Enemies.Count);
        }
        else return false;

        this.ranPosSpawner = Random.Range(0, MAX_AMOUNT_SPAWNER_POS);

        if (this.enemyWaves[indexWave].Enemies[indexEnemy].amount == 0)
        {
            this.enemyWaves[indexWave].Enemies.RemoveAt(indexEnemy);
            if (this.enemyWaves[indexWave].Enemies.Count <= 0 && indexWave < this.enemyWaves.Count - 1) indexWave++;

            return false;
        }
        return true;
    }

    private void OnDestroy()
    {
        EnemyCtrl.OnEnemyDead -= EnemyDead;
    }
}
