using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCard : MonoBehaviour
{
    [SerializeField] private List<GameObject> allRewardCards;
    [SerializeField] private Objects_SO rewardCard_SO;

    private void Start()
    {
        this.allRewardCards = this.rewardCard_SO.GetAllObjectsData();
    }

    public GameObject GetRewardCard(int indexRewardCard)
    {
        return this.allRewardCards[indexRewardCard];
    }
}
