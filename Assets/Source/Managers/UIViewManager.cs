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

    public PetManager m_PetManager;
    private Hunger m_PetHungerScript;
    private Happiness m_PetHappinessScript;
    private Pet m_CurrentPet;

    private bool m_CurrentPetHasHunger;
    private bool m_CurrentPetHasHappiness;

    private double m_HappinessValue;
    private double m_HungerValue;

    private void Update()
    {
        if (m_PetHappinessScript != m_PetManager.CurrentPetHappinessScript)
        {
            InitilizePetHappinessUI();
        }

        if (m_PetHungerScript != m_PetManager.CurrentPetHungerScript)
        {
            InitilizePetHungerUI();
        }

        if (m_CurrentPetHasHunger == true)
        {
            if (m_HungerValue != m_PetHungerScript.CurrentHunger)
            {
                m_HungerValue = m_PetHungerScript.CurrentHunger;
                m_HungerView.UpdatePetNeedsFillBar(m_HungerValue);
            }

            if (m_FeedPetButton.interactable != m_PetHungerScript.CanBeFed)
            {
                m_FeedPetButton.interactable = m_PetHungerScript.CanBeFed; 
            }
        }

        if (m_CurrentPetHasHappiness == true)
        {
            if (m_HappinessValue != m_PetHappinessScript.CurrentHappiness)
            {
                m_HappinessValue = m_PetHappinessScript.CurrentHappiness;
                m_HappinessView.UpdatePetNeedsFillBar(m_HappinessValue);
            }

            if (m_CuddlePetButton.interactable != m_PetHappinessScript.CanBeSnuggled)
            {
                m_CuddlePetButton.interactable = m_PetHappinessScript.CanBeSnuggled;
            }
        }

    }

    public void InitilizePetHungerUI()
    {
        //change the script that this script refrences to the one in the pet manager
        m_PetHungerScript = m_PetManager.CurrentPetHungerScript;

        //can also be written as m_currentPetHasHunger = m_Pet Hunger Script (See InitilizePetHappinessUI for example of this!) 
        if (m_PetHungerScript != null)
        {
            m_CurrentPetHasHunger = true;
        }
        else
        {
            m_CurrentPetHasHunger = false;
        }

        //Toggle the appropriate game objects on/off depending on if this pet has hunger!
        m_HungerView.gameObject.SetActive(m_CurrentPetHasHunger);
        m_FeedPetButton.gameObject.SetActive(m_CurrentPetHasHunger);
    }
    public void InitilizePetHappinessUI()
    {
        //change the script that this script refrences to the one in the pet manager
        m_PetHappinessScript = m_PetManager.CurrentPetHappinessScript;

        m_CurrentPetHasHappiness = m_PetHappinessScript;

        //Toggle the appropriate game objects on/off depending on if this pet has happiness!
        m_HappinessView.gameObject.SetActive(m_CurrentPetHasHappiness);
        m_CuddlePetButton.gameObject.SetActive(m_CurrentPetHasHappiness);
    }
}
