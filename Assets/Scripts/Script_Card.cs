using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Script_Card : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Scriptable_Card associateCard;
    public TextMeshProUGUI attackText, defenseText, nameText;

    private Image imageComp;
    private int childPosition;
    private Transform parentTransform;

    public void SetupDatas(Scriptable_Card newAssociateCard)
    {
        associateCard = newAssociateCard;
        imageComp = GetComponent<Image>();
        parentTransform = transform.parent;
        CheckPositon();
        SetupTexts();
    }

    public void SetupTexts()
    {
        attackText.text = associateCard.attack.ToString("");
        defenseText.text = associateCard.life.ToString("");
        nameText.text = associateCard.cardName;
        imageComp.sprite = associateCard.unitSprite;
    }

    public void CheckPositon()
    {
        childPosition = transform.GetSiblingIndex();
    }

    public bool Fuse(GameObject targetFuse)
    {
        Scriptable_Card fuseCard = targetFuse.GetComponent<Script_Card>().associateCard;

        for (int i = 0; i < fuseCard.fusionList.Count; i++)
        {
            if (fuseCard.fusionList[i].materia == associateCard)
            {
                targetFuse.GetComponent<Script_Card>().SetupDatas(fuseCard.fusionList[i].result);
                transform.SetParent(parentTransform.parent.transform);
                Script_GameManager.Instance.RemoveCardInHand(this);
                Script_GameManager.Instance.CheckUpCardPosition();
                Script_GameManager.Instance.DrawACard();
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
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Script_Card>() && Fuse(eventData.pointerCurrentRaycast.gameObject))
            {

            }
            else if(eventData.pointerCurrentRaycast.gameObject.CompareTag("DropZone"))
            {
                Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                
                if(Physics.Raycast(cameraRay,out hit))
                {
                    Engage(hit.point);
                }
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

    void Engage(Vector3 engagePosition)
    {
        Instantiate(associateCard.unitPrefab, engagePosition, associateCard.unitPrefab.transform.rotation);
        Script_GameManager.Instance.RemoveCardInHand(this);
        Script_GameManager.Instance.DrawACard();
        Destroy(gameObject);
        Debug.Log("Engage");
    }

    void ResetParent()
    {
        transform.SetParent(parentTransform);
        transform.SetSiblingIndex(childPosition);
    }


}
