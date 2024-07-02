using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject objectDrag;
    public GameObject objectGame;
    private GameObject objectDragInstance;
    public GameManager gameManager;
    public Canvas canvas;

    private float timmer = 0;
    private float maxTime;
    [SerializeField] private bool isTimming = false;

    [SerializeField] private Image fillTime;

    [SerializeField] private Bird_SO birdSO;

    private RectTransform rectObjDrag;

    public bool IsTimming { get => isTimming; set => isTimming = value; }

    private void Start()
    {
        this.gameManager = GameManager.instance;
        this.canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

        //time
        this.maxTime = this.birdSO.timming;
    }

    private void Update()
    {
        this.Timer();
    }

    private bool IsEnoughSunPrice()
    {
        if(GameManager.instance.SunScore >= this.birdSO.price)
        {
            return true;
        }

        return false;
    }

    private void Timer()
    {
        if (!this.IsTimming) return;
        if (this.timmer >= this.maxTime)
        {
            this.IsTimming = false;
            this.timmer = 0f;
            this.fillTime.fillAmount = 0;
            return;
        }
        this.timmer += Time.deltaTime;
        float progress = this.maxTime - this.timmer;
        this.fillTime.fillAmount = progress / this.maxTime;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (this.isTimming || !this.IsEnoughSunPrice()) return;
        //this.objectDragInstance.transform.position = Input.mousePosition;
        if(this.rectObjDrag != null) this.rectObjDrag.anchoredPosition += eventData.delta;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.isTimming || !this.IsEnoughSunPrice()) return;
        this.objectDragInstance = Instantiate(this.objectDrag, this.canvas.transform);
        this.rectObjDrag = this.objectDragInstance.GetComponent<RectTransform>();
        this.rectObjDrag.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        this.objectDragInstance.GetComponent<ObjectDragging>().card = this;

        this.gameManager.draggingObj = this.objectDragInstance;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (this.gameManager.PlaceObject())
        {
            this.isTimming = true;
            // update sun
            GameManager.instance.SubSun(this.birdSO.price);
        }
        this.gameManager.draggingObj = null;
        
        Destroy(this.objectDragInstance);
    }
}
