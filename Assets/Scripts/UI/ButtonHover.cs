using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Image img;

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.color = Color.green; //Or however you do your color
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.color = Color.white; //Or however you do your color
    }
}