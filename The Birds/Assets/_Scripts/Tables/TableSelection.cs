using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSelection : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool isActive = true;

    [SerializeField] public static Vector2 DISTANCE_CARDS = new Vector2(110.603f, 141.3f);
    [SerializeField] const int MAX_COLUMN = 5;
    [SerializeField] private Vector2 POS_INIT = new Vector2(-274.406f, 116f);
    [SerializeField] private List<GameObject> cardsInTable;
    [SerializeField] private Objects_SO birdCardCollectionData;
    void Start()
    {
        this.anim = GetComponent<Animator>();

        LoadCardIntoTable();
    }

    public void OnStartGameLevel()
    {
        this.anim.SetTrigger("StartLevel");
    }

    public void SetActiveObject()
    {
        this.isActive = !this.isActive;
        this.gameObject.SetActive(this.isActive);
    }

    private void LoadCardIntoTable()
    {
        Vector2 pos = new Vector2(POS_INIT.x, POS_INIT.y);
        GameObject card;
        int row = MAX_COLUMN;
        for (int i = 0; i < this.birdCardCollectionData.GetLength(); i++)
        {
            card = Instantiate(this.birdCardCollectionData.GetObjectInData(i), transform);
            this.cardsInTable.Add(card);
            this.cardsInTable[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, pos.y);
            pos.x += DISTANCE_CARDS.x;

            if (i >= row)
            {
                pos = new Vector2(POS_INIT.x, pos.y - DISTANCE_CARDS.y);
                row = row + MAX_COLUMN + 1;
            }
        }
    }
}
