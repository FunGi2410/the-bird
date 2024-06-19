using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wheat : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private float speedFalling;
    [SerializeField] private float speedMove;

    private bool isChosen = false;
    [SerializeField] private Transform targetPos;

    private float posDestroy;
    private float heightOfSun;

    private void Awake()
    {
        this.heightOfSun = GetComponent<RectTransform>().rect.height;
    }

    private void Start()
    {
        this.targetPos = GameObject.FindGameObjectWithTag("CurSunScore").transform;

        this.posDestroy = -UIManager.instance.HalfHeightOfCanvas - this.heightOfSun - 1;
    }

    private void FixedUpdate()
    {
        if(!this.isChosen) this.Falling();
        else this.MoveToSunScore();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        this.isChosen = true;
    }

    public void MoveToSunScore()
    {
        if (Vector3.Distance(transform.position, targetPos.position) < 0.001f)
        {
            GameManager.instance.AddSun();
            ObjectPoolManager.instance.CoolObject(gameObject, PoolObjectType.wheat);
            this.isChosen = false;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, this.speedMove);
    }

    public void Falling()
    {
        transform.localPosition += Vector3.down * this.speedFalling * Time.fixedDeltaTime;
        this.Destroy();
    }

    public void Destroy()
    {
        if (transform.localPosition.y < this.posDestroy)
        {
            ObjectPoolManager.instance.CoolObject(gameObject, PoolObjectType.wheat);
            this.isChosen = false;
        }
    }
}
