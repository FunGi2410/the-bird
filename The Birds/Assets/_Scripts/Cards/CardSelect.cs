using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour, IPointerDownHandler
{
    public GameObject objectDrag;

    public GameObject cardInBar;

    public GameObject canvas;

    bool isChoosed = false;
    private bool isUnlocked;

    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }

    private void Start()
    {
        this.canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (InBarManager.Instance.targetPosInBars.Count <= InBarManager.Instance.curMaxSlot && !this.isChoosed)
        {
            GameObject cardInBarInstance = Instantiate(this.cardInBar, this.canvas.transform);
            cardInBarInstance.transform.position = this.transform.position;
            cardInBarInstance.GetComponent<CardInBar>().posCardSelect = this.gameObject.transform;

            InBarManager.Instance.selectedCards.Add(cardInBarInstance);
            cardInBarInstance.name = "Card " + (InBarManager.Instance.selectedCards.Count).ToString();
            
            this.SetStateCard();
        }
    }

    public void SetStateCard()
    {
        this.isChoosed = !this.isChoosed;

        // Blur card when selected
        Image imgObj = this.gameObject.transform.GetChild(1).GetComponent<Image>();
        var tmpColor = imgObj.color;

        if (!this.isChoosed) tmpColor.a = 1f;
        else tmpColor.a = 0.3f;
        imgObj.color = tmpColor;
    }
}
