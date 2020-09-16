using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetNeedsView : MonoBehaviour
{
    public Image m_PetNeedIcon;
    public Image m_PetNeedFillBar;

    public void UpdatePetNeedsFillBar(double NewHungerValue, double MaxHungerValue, string Origin)
    {
        float fillAmount = (float)(NewHungerValue / MaxHungerValue);
        print(Origin + " " + fillAmount.ToString());
        m_PetNeedFillBar.fillAmount = fillAmount;
        m_PetNeedIcon.color = new Color(1, 1, 1, fillAmount);
    }

}
