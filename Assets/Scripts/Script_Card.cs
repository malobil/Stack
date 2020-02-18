using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Script_Card : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Card associateCard;
    public TextMeshProUGUI attackText, defenseText;

    private RawImage imageComp;
    private int childPosition;
    private Transform parentTransform;

    private void Start()
    {
        imageComp = GetComponent<RawImage>();
        associateCard = Script_DataManager.Instance.GetCard("Barbare");
        parentTransform = transform.parent;
        childPosition = transform.GetSiblingIndex();
        SetupTexts();
    }

    public void SetupDatas(Card newAssociateCard)
    {
        associateCard = newAssociateCard;
        SetupTexts();
    }

    public void SetupTexts()
    {
        attackText.text = associateCard.Attack.ToString("");
        defenseText.text = associateCard.Life.ToString("");
    }

    public bool Fuse(GameObject targetFuse)
    {
        Card targetCard = targetFuse.GetComponent<Script_Card>().associateCard;

        foreach (Fusions fusions in associateCard.Fusions)
        {
            if(fusions.Materia == targetCard.Name)
            {
                targetFuse.GetComponent<Script_Card>().SetupDatas(Script_DataManager.Instance.GetCard(fusions.Result));
                Destroy(gameObject);
                return true;
            }
        }

        return false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(parentTransform.parent.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        imageComp.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject != null)
        {
            Debug.Log(eventData.pointerCurrentRaycast.gameObject);

            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Script_Card>() && Fuse(eventData.pointerCurrentRaycast.gameObject))
            {
               
            }
            else
            {
                ResetParent();
            }
        }
        else
        {
            ResetParent();
        }
       

        imageComp.raycastTarget = true;
    }

    void ResetParent()
    {
        transform.SetParent(parentTransform);
        transform.SetSiblingIndex(childPosition);
    }


}
