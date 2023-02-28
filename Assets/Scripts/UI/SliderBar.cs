using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    [SerializeField] private bool isEnemy;
    [SerializeField]private Transform whoIsParent;
    [SerializeField]private Slider _Slider;
    
    [SerializeField] private Vector3 Offset;
    

    public void SetMaxSlider(int _value)
    {
        if (isEnemy)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        _Slider.maxValue = _value;
        _Slider.value = _value;
    }

    public void SetSlider(int _value)
    {
        _Slider.value = _value;
    }
    private void Update()
    {
        if (isEnemy)
        {
            _Slider.transform.position = Camera.main.WorldToScreenPoint(whoIsParent.position + Offset);
        }
    }
}
