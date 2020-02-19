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

    private RawImage imageComp;
    private int childPosition;
    private Transform parentTransform;

    private void Start()
    {
        imageComp = GetComponent<RawImage>();
        
        parentTransform = transform.parent;
        childPosition = transform.GetSiblingIndex();
        SetupTexts();
    }

    public void SetupDatas(Scriptable_Card newAssociateCard)
    {
        associateCard = newAssociateCard;
        SetupTexts();
    }

    public void SetupTexts()
    {
        attackText.text = associateCard.attack.ToString("");
        defenseText.text = associateCard.life.ToString("");
        nameText.text = associateCard.name;
    }

    public void Fuse(GameObject targetFuse)
    {
        Scriptable_Card fuseCard = targetFuse.GetComponent<Script_Card>().associateCard;

        for (int i = 0; i < fuseCard.fusionList.Count; i++)
        {
            if (fuseCard.fusionList[i].materia == associateCard)
            {
                targetFuse.GetComponent<Script_Card>().SetupDatas(fuseCard.fusionList[i].result);
                Destroy(gameObject);
                return;
            }
        }

        Debug.Log("Fuse");
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

            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Script_Card>())
            {
                Fuse(eventData.pointerCurrentRaycast.gameObject);
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
