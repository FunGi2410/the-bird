using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour
{
    public List<Transform> targetPosInBars;
    public List<GameObject> selectedCards;
    public int curMaxSlot;

    [SerializeField] int indexPosInBar;

    public int IndexPosInBar { 
        get => indexPosInBar; 
        set
        {
            if(value < targetPosInBars.Count)
                indexPosInBar = value;
        }
    }

    //[SerializeField] Vector2 nextToPos = new Vector2(107.3f, 0f);

    //public static InBarManager Instance { get; private set; }
    void Awake()
    {
        foreach(Transform pos in transform)
        {
            this.targetPosInBars.Add(pos);
        }
        this.curMaxSlot = this.targetPosInBars.Count;
        //Instance = this;
        CardInBar.LineUpCardAction += this.LineUpCard;
    }

    /*public void AddTargetPos()
    {
        if (this.targetPosInBars.Count > this.curMaxSlot) return;
        GameObject targetPos = Instantiate(this.targetPosInBars[0], this.gameObject.transform);
        targetPos.GetComponent<RectTransform>().anchoredPosition = this.targetPosInBars[this.targetPosInBars.Count - 1].GetComponent<RectTransform>().anchoredPosition + this.nextToPos;
        this.targetPosInBars.Add(targetPos);
    }*/

    public void LineUpCard()
    {
        print(this.selectedCards.Count);
        for (int i = 0; i < this.selectedCards.Count; i++)
        {
            this.selectedCards[i].GetComponent<CardInBar>().MoveToTarget(this.targetPosInBars[i]);
        }
    }

    private void OnDestroy()
    {
        CardInBar.LineUpCardAction -= this.LineUpCard;
    }
}
