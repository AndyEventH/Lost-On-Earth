using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    [SerializeField] private bool isEnemy;
    [SerializeField]private Transform whoIsParent;
    Slider _Slider;
    
    [SerializeField] private Vector3 Offset;
    public void Start()
    {
        _Slider = GetComponent<Slider>();
        if (isEnemy)
        {
            _Slider.transform.position = Camera.main.WorldToScreenPoint(whoIsParent.position + Offset);
        }
    }

    public void SetMaxSlider(int _value)
    {
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
