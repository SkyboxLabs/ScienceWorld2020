using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PetDebugUIScroll : MonoBehaviour
{
    public TextMeshProUGUI m_ValueText;
    public Slider m_Slider;

    private float MaxValue;

    public void SetText(float value)
    {
        m_ValueText.text = value + "/" + MaxValue;
    }

    public void SetValue(float value)
    {
        m_ValueText.text = value + "/" + MaxValue;
        m_Slider.value = value;
    }

    public void SetMaxValue(float max)
    {
        MaxValue = max;
        m_Slider.maxValue = max;
    }

    public void SetMinValue(float min)
    {
        m_Slider.minValue = min;
    }
}
