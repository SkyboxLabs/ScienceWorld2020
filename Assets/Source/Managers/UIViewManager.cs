using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIViewManager : MonoBehaviour
{
    public PetNeedsView m_HungerView;
    public PetNeedsView m_HappinessView;
    public Button m_FeedPetButton;
    public Button m_CuddlePetButton;

    public void ToggleHungerObjects(bool IsActive)
    {
        m_HungerView.gameObject.SetActive(IsActive);
        m_FeedPetButton.gameObject.SetActive(IsActive);
    }
    public void ToggleHappinessObjects(bool IsActive)
    {
        m_HappinessView.gameObject.SetActive(IsActive);
        m_CuddlePetButton.gameObject.SetActive(IsActive);
    }
}
