using UnityEngine;
using UnityEngine.EventSystems;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Unlock skill");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Show skill tooltip");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Hide skill tooltip");
    }
}
