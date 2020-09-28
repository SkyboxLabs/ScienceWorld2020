using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetNeedsView : MonoBehaviour
{
    public Image m_PetNeedIcon;
    public Image m_PetNeedFillBar;

    public void UpdatePetNeedsFillBar(double NewValue)
    {
        //matches the max value in the Pet Deubug UI 
        float fillAmount = (float)(NewValue / 100);
        m_PetNeedFillBar.fillAmount = fillAmount;
        m_PetNeedIcon.color = new Color(1, 1, 1, fillAmount);
    }
}
