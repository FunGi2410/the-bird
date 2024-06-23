using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;
using System.Threading.Tasks;

public class CardInBar : MonoBehaviour, IPointerDownHandler
{
    BarManager barManager;
    public float speed;
    public Transform posCardSelect;

    public GameObject canvas;

    bool isReturn = false;

    [SerializeField] private GameObject cardInGame;

    public static UnityAction LineUpCardAction;

    private void Awake()
    {
        print("awake");
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

        // Pool bullet for this object
        PoolBullet();

        // Destroy card in bar
        Destroy(this.gameObject);
    }

    void PoolBullet()
    {
        ObjectCard objectCard = this.cardInGame.GetComponent<ObjectCard>();
        PoolInfo bulletInfo = new PoolInfo(objectCard.RangeBirdSO.bulletType, 10, objectCard.RangeBirdSO.bulletPrefab);
        ObjectPoolManager.instance.objToPools.Add(bulletInfo);
        ObjectPoolManager.instance.PooledObject(bulletInfo);
    }

    public void MoveToTarget(Transform posTarget)
    {
        transform.DOMove(posTarget.position, 0.5f);
        //transform.position = posTarget.position;
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
            
            
            this.posCardSelect.GetComponent<CardSelect>().SetStateCard();
        }
    }

    private void OnDestroy()
    {
        GameManager.startGame -= this.DesCardBarAndInitCardGame;
    }

    /*IEnumerator MoveToTarget(Transform posTarget)
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, posTarget.position) < 0.001f)
            {
                StopCoroutine("MoveToTarget");

                if (this.isReturn)
                {
                    Destroy(this.gameObject);
                    GameManager.startGame -= this.DesCardBarAndInitCardGame;
                    this.barManager.selectedCards.Remove(this.gameObject);
                    //ChangePosCard?.Invoke();

                    this.posCardSelect.GetComponent<CardSelect>().SetStateCard();
                }
                break;
            }

            transform.position = Vector3.MoveTowards(transform.position, posTarget.position, this.speed);

            yield return new WaitForSeconds(0.01f);
        }
    }*/
}
