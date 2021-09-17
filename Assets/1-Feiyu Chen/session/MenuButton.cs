using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

 public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
 
     public TextMeshPro theText;
 
     public void OnPointerEnter(PointerEventData eventData)
     {
         theText.color = Color.blue; //Or however you do your color
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
         theText.color = Color.black; //Or however you do your color
     }
 }
