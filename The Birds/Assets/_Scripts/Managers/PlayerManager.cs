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
