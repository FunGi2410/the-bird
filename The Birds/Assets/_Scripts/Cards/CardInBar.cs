using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;
using System.Threading.Tasks;

public class CardInBar : MonoBehaviour, IPointerDownHandler
{
    BarManager barManager;
    public Transform posCardSelect;

    public GameObject canvas;

    bool isReturn = false;

    [SerializeField] private GameObject cardInGame;

    public static UnityAction LineUpCardAction;

    private void Awake()
    {
        //print("awake");
        this.barManager = GameObject.FindGameObjectWithTag("BarManager").GetComponent<BarManager>();
        this.canvas = GameObject.FindGameObjectWithTag("Canvas");

        GameManager.startGame += this.DesCardBarAndInitCardGame;
    }

    private void Start()
    {
        // Move to empty slot in bar
        MoveToTarget(this.barManager.targetPosInBars[this.barManager.IndexPosInBar]);
        this.barManager.IndexPosInBar++;
    }

    async public void OnPointerDown(PointerEventData eventData)
    {
        if (this.isReturn) return;
        this.isReturn = true;

        await MoveReturn(this.posCardSelect);
        this.barManager.IndexPosInBar--;
    }

    public void DesCardBarAndInitCardGame()
    {
        // Init card in game
        GameObject cardGameInstance = Instantiate(this.cardInGame, this.canvas.transform);
        cardGameInstance.transform.position = this.transform.position;

        // Destroy card in bar
        Destroy(this.gameObject);
    }

    public void MoveToTarget(Transform posTarget)
    {
        transform.DOMove(posTarget.position, 0.5f);
    }

    async Task MoveReturn(Transform posTarget)
    {
        MoveToTarget(posTarget);
        this.barManager.selectedCards.Remove(this.gameObject);
        LineUpCardAction?.Invoke();
        await Task.Delay(500);
        
        if (this.isReturn)
        {
            GameManager.startGame -= this.DesCardBarAndInitCardGame;
            Destroy(this.gameObject);
            
            
            this.posCardSelect.GetComponent<CardSelect>().SetChoosenStateCard();
        }
    }

    private void OnDestroy()
    {
        GameManager.startGame -= this.DesCardBarAndInitCardGame;
    }
}
