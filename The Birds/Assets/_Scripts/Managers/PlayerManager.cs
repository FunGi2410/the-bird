using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Object cards sellect
    [SerializeField] private List<GameObject> birdCardCollection;
    [SerializeField] private Objects_SO birdCardCollectionData;

    private void Awake()
    {
        this.birdCardCollection = this.birdCardCollectionData.GetAllObjectsData();
    }

    private void Start()
    {
    }

    public void UnlockCard()
    {
        for (int i = 0; i <= LevelManager.instance.AmountUnlockedCard; i++)
        {
            this.UnlockNewPlantCard(i);
            Debug.Log("Unlocked " + i);
        }
    }

    public List<GameObject> GetBirdCardCollection()
    {
        return this.birdCardCollection;
    }

    public void UnlockNewPlantCard(int indexPlantCard)
    {
        if (indexPlantCard >= this.birdCardCollection.Count) return;
        this.birdCardCollection[indexPlantCard].GetComponent<CardSelect>().IsUnlocked = true;
    }
}
