using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatSpawner : Spawner
{
    public GameObject wheatPrefab;

    private float timer = 0f;
    public Vector2 secondsBetweenSpawnMinMax;

    private float spawnAngleMax = 15f;

    protected void Awake()
    {
        float sunHeight = wheatPrefab.GetComponent<RectTransform>().rect.height;
        UIManager.instance.HalfHeightOfCanvas += sunHeight;
    }

    private void Update()
    {
        if (!GameManager.instance.IsStartGame) return;
        float secondsBetweenSpawn = Random.Range(secondsBetweenSpawnMinMax.x, secondsBetweenSpawnMinMax.y);
        timer += Time.deltaTime;
        if (timer < secondsBetweenSpawn) return;
        timer = 0f;
        this.Spawn();
    }

    protected override void Spawn()
    {
        // Active
        GameObject wheat = InstanceObject(PoolObjectType.wheat);
        //Debug.Log("Wheat spawn");
        // Set position
        float ranPosX = Random.Range(-UIManager.instance.HalfWidthOfCanvas, UIManager.instance.HalfWidthOfCanvas);
        wheat.GetComponent<RectTransform>().anchoredPosition = new Vector2(ranPosX, UIManager.instance.HalfHeightOfCanvas);

        // Set rotation
        float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
        wheat.transform.Rotate(Vector3.forward * spawnAngle);
    }
}
