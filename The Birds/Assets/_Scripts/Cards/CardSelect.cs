using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour, IPointerDownHandler
{
    public GameObject objectDrag;
    public GameObject cardInBar;
    public GameObject canvas;
    public GameObject lockObject;
    private BarManager barManager;
    

    [SerializeField] bool isChoosed = false;
    [SerializeField] private bool isUnlocked;


    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }

    private void Awake()
    {
        this.barManager = GameObject.FindGameObjectWithTag("BarManager").GetComponent<BarManager>();
        this.canvas = GameObject.FindGameObjectWithTag("Canvas");

        this.SetUnlockStateCard();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!this.isUnlocked) return;
        CreateCardInBar();
    }

    void CreateCardInBar()
    {
        if (this.barManager.IndexPosInBar < this.barManager.curMaxSlot && !this.isChoosed)
        {
            GameObject cardInBarInstance = Instantiate(this.cardInBar, this.canvas.transform);
            cardInBarInstance.transform.position = this.transform.position;
            cardInBarInstance.GetComponent<CardInBar>().posCardSelect = this.gameObject.transform;

            this.barManager.selectedCards.Add(cardInBarInstance);
            cardInBarInstance.name = "Card " + (this.barManager.selectedCards.Count).ToString();

            this.SetChoosenStateCard();
        }
    }

    public void SetUnlockStateCard()
    {
        this.lockObject.SetActive(!this.isUnlocked);
        this.SetBlurCard(!this.isUnlocked);
    }

    public void SetChoosenStateCard()
    {
        this.isChoosed = !this.isChoosed;
        SetBlurCard(isChoosed);
    }

    public void SetBlurCard(bool state)
    {
        // Blur card when selected
        Image imgObj = this.gameObject.transform.GetChild(1).GetComponent<Image>();
        var tmpColor = imgObj.color;

        if (state == false) tmpColor.a = 1f;
        else tmpColor.a = 0.3f;
        imgObj.color = tmpColor;
    }
}
