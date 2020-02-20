using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GameManager : MonoBehaviour
{
    public static Script_GameManager Instance { get; private set; }

    public List<Script_Card> cardInHand;
    public List<Scriptable_Card> cardDatas;

    public GameObject cardPrefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        for(int i = 0; i<4;i++)
        {
            DrawACard();
        }
    }

    public void CheckUpCardPosition()
    {
        foreach(Script_Card card in cardInHand)
        {
            card.CheckPositon();
        }
    }

    public void DrawACard()
    {
        int rdmIdx = Random.Range(0, cardDatas.Count);
        Scriptable_Card drawedCard = cardDatas[rdmIdx];
        GameObject newCard = Instantiate(cardPrefab);
        Script_UIManager.Instance.SetNewCard(newCard.transform);
        newCard.GetComponent<Script_Card>().SetupDatas(drawedCard);
        AddCardInHand(newCard.GetComponent<Script_Card>());
      

    }

    public void AddCardInHand(Script_Card card)
    {
        cardInHand.Add(card);
    }

    public void RemoveCardInHand(Script_Card card)
    {
        cardInHand.Remove(card);
    }

    
}
