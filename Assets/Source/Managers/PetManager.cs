﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PetManager : MonoBehaviour
{
    [SerializeField]
    private UIViewManager m_UIViewManager;
    [SerializeField]
    private List<Pet> m_Pets = new List<Pet>(); //This is a list! We can store multiples of information
    private int m_ActivePet = 0;

    public Happiness CurrentPetHappinessScript { get; private set; }
    public Hunger CurrentPetHungerScript { get; private set; }

    private bool m_PetShouldLookSad = false;
    private bool m_PetIsSad = false;

    private void Awake()
    {
        if (m_Pets.Count == 0)
        {
            Debug.LogError("Currently your PetManager has no pets! Make sure to add a pet to m_Pets by clicking on the Pet Canvas in the Heirarchy(left side)," +
                " and adding to Pets on Pet Manager in the Inspector (right side)");
        }

        foreach (Pet PetElement in m_Pets)
        {
            if (PetElement == null)
            {
                Debug.LogWarning("Element in Pets is null - be sure to assign it a value!");
                break;
            }
        }

        if (!m_UIViewManager)
        {
            Debug.LogWarning("Currently your PetManager has no UI View Manager - be sure to assign it one so it works!");
        }
    }

    private void Start()
    {
        SwapPet(m_ActivePet);
    }

    private void Update()
    {
        UpdateAnimatorState();
    }

    public void SwapPet(int newActivePet) //This is a function we can call on our buttons to swap the pets. The number represents what element in the list it is.
    {
        foreach (Pet pet in m_Pets)
        {
            pet.gameObject.SetActive(false);
        }

        m_ActivePet = newActivePet;

        m_Pets[m_ActivePet].gameObject.SetActive(true);

        CurrentPetHappinessScript = m_Pets[m_ActivePet].GetComponent<Happiness>();
        CurrentPetHungerScript = m_Pets[m_ActivePet].GetComponent<Hunger>();

        UpdateListeners();
    }

    private void UpdateListeners()
    {
        m_UIViewManager.m_FeedPetButton.onClick.RemoveAllListeners();
        m_UIViewManager.m_CuddlePetButton.onClick.RemoveAllListeners();

        m_UIViewManager.m_FeedPetButton.onClick.AddListener(FeedButtonOnClick);
        m_UIViewManager.m_CuddlePetButton.onClick.AddListener(CuddleButtonOnClick);
    }

    private void CuddleButtonOnClick()
    {
        CurrentPetHappinessScript?.Cuddle();
    }

    private void FeedButtonOnClick()
    {
        CurrentPetHungerScript?.Feed();
    }

    public Pet GetActivePet()
    {
        return m_Pets[m_ActivePet];
    }

    private void UpdateAnimatorState()
    {
        if (CurrentPetHappinessScript && CurrentPetHungerScript)
        {
            if (CurrentPetHappinessScript.IsLowHappiness && CurrentPetHungerScript.IsLowHunger)
            {
                m_PetShouldLookSad = true;
            }
            else if (CurrentPetHappinessScript.IsLowHappiness || CurrentPetHungerScript.IsLowHunger)
            {
                m_PetShouldLookSad = true;
            }
            else
            {
                m_PetShouldLookSad = false;
            }
        }
        else if (CurrentPetHungerScript && !CurrentPetHappinessScript)
        {
            if (CurrentPetHungerScript.IsLowHunger)
            {
                m_PetShouldLookSad = true;
            }
            else
            {
                m_PetShouldLookSad = false;
            }
        }
        else if (!CurrentPetHungerScript && CurrentPetHappinessScript)
        {
            if (CurrentPetHappinessScript.IsLowHappiness)
            {
                m_PetShouldLookSad = true;
            }
            else
            {
                m_PetShouldLookSad = false;
            }
        }

        if (m_PetIsSad != m_PetShouldLookSad)
        {
            m_PetIsSad = m_PetShouldLookSad;
            GetActivePet().SetAnimatorBool(AnimatorBools.IsSad, m_PetIsSad);
        }
    }
}
