using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PetManager : MonoBehaviour
{
    [SerializeField]
    private UIViewManager m_UIManager;
    [SerializeField]
    private List<Pet> ListOfPets = new List<Pet>(); //This is a list! We can store multiples of information

    private int m_ActivePet = 0;
    private Happiness m_CurrentPetHappinessScript;
    private Hunger m_CurrentPetHungerScript;

    private void Awake()
    {
        if (ListOfPets.Count == 0)
        {
            Debug.LogWarning("Currently your PetManager has no pets! Make sure to add a pet to the List of Pets");
        }

        foreach (Pet PetElement in ListOfPets)
        {
            if (PetElement == null)
            {
                Debug.LogWarning("Element in List of Pets is null - be sure to assign it a value!");
                break; 
            }
        }

        if (!m_UIManager)
        {
            Debug.LogWarning("Currently your PetManager has no UI Manager - be sure to assign it one so it works!");
        }
    }

    private void Start()
    {
        SwapPet(m_ActivePet);
    }

    public void SwapPet(int x) //This is a function we can call on our buttons to swap the pets. The number represents what element in the list it is.
    {
        foreach (Pet elementInList in ListOfPets)
        {
            elementInList.gameObject.SetActive(false);
        }

        m_ActivePet = x;
        
        ListOfPets[m_ActivePet].gameObject.SetActive(true);

        m_CurrentPetHappinessScript = ListOfPets[m_ActivePet].GetComponent<Happiness>();
        m_CurrentPetHungerScript = ListOfPets[m_ActivePet].GetComponent<Hunger>();

        UpdateListeners();
        UpdateUI();
    }

    private void UpdateListeners()
    {
        m_UIManager.m_FeedPetButton.onClick.RemoveAllListeners();
        m_UIManager.m_CuddlePetButton.onClick.RemoveAllListeners();
        
        m_UIManager.m_FeedPetButton.onClick.AddListener(FeedButtonOnClick);
        m_UIManager.m_CuddlePetButton.onClick.AddListener(CuddleButtonOnClick);
    }

    private void UpdateUI()
    {
        m_UIManager.ToggleHungerObjects(m_CurrentPetHungerScript);
        m_UIManager.ToggleHappinessObjects(m_CurrentPetHappinessScript);

        m_CurrentPetHungerScript?.UpdatePetHunger();
        m_CurrentPetHappinessScript?.UpdatePetHappiness();
    }

    private void CuddleButtonOnClick()
    {
        m_CurrentPetHappinessScript?.Cuddle();
    }

    private void FeedButtonOnClick()
    {
        m_CurrentPetHungerScript?.Feed();
    }
}
