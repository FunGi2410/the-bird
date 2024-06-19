using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBarManager : MonoBehaviour
{
    public List<GameObject> targetPosInBars;
    public List<GameObject> selectedCards;
    public int curMaxSlot;

    [SerializeField]Vector2 nextToPos = new Vector2(107.3f, 0f);

    public static InBarManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;

        CardInBar.ChangePosCard += this.ChangePosCard;
    }

    public void AddTargetPos()
    {
        if (this.targetPosInBars.Count > this.curMaxSlot) return;
        GameObject targetPos = Instantiate(this.targetPosInBars[0], this.gameObject.transform);
        targetPos.GetComponent<RectTransform>().anchoredPosition = this.targetPosInBars[this.targetPosInBars.Count - 1].GetComponent<RectTransform>().anchoredPosition + this.nextToPos;
        this.targetPosInBars.Add(targetPos);
    }

    public void ChangePosCard()
    {
        for (int i = 0; i < this.selectedCards.Count; i++)
        {
            this.selectedCards[i].GetComponent<CardInBar>().MoveEvent(this.targetPosInBars[i].transform);
        }
    }
}
