using System.Collections;
using UnityEngine;

/*
  [Advanced]
  - This line of code makes sure that if a GameObject has a "Pet Hunger" script attatched, it also has a "Pet"
  - This script requires the Pet Controller to work!
*/

[RequireComponent(typeof(Pet))]

public class Happiness : MonoBehaviour
{

    // Our inital variables to start the game. 
    private bool m_CanGetSadder = true;
    private bool m_CanBeSnuggled = true;
    private double m_PatTimer;
    private double m_CurrentHappiness;

    private Pet m_Pet;
    public UIViewManager UIManager;

    private void Awake()
    {
        m_Pet = GetComponent<Pet>(); //Get the instace of the Pet Script attatched to this game object

        if (m_Pet == null) // [Advanced] If the pet controller is not found, print this error to the Consle window. This way we know why the code is breaking!
        {
            print("Failed to initalize pet. Missing " + gameObject.name + "'s pet controller!");
        }

        if (!m_Pet.m_IsInitilized)
        {
            m_Pet.Initilize();
        }

        m_CurrentHappiness = m_Pet.StartingHappiness; //Initilize our pet's current happiness to be the happiness we determined in Pet (Our numbers for designers)

        m_CanBeSnuggled = true; //Can our pet be snuggled? This is useful later in the script!
    }

    private void Update() // Unity will call this every frame
    {
        if (m_CanGetSadder) // If the pet can get sadder run the next code
        {
            StartCoroutine(GetSadder()); // Coroutine is a special type of function - more on this later!
        }
    }

    IEnumerator GetSadder()
    {
        m_CanGetSadder = false;
        yield return new WaitForSeconds(m_Pet.HappinessDecayInSeconds);

        m_CurrentHappiness -= m_Pet.HappinessRemovedOverTime;
        UIManager.m_HappinessView.UpdatePetNeedsFillBar(m_CurrentHappiness, m_Pet.MaxPetStat, "Happiness");

        // If our pet is as sad as they can get, set their food points to 0 and tell the game to stop allowing the pet to get sadder :(
        if (m_CurrentHappiness <= 0)
        {
            m_CurrentHappiness = 0;
        }
        else
        {
            m_CanGetSadder = true;
        }
    }

    public void Cuddle()
    {
        m_Pet.SetAnimatorTrigger(AnimatorEnums.Happy);
        StartCoroutine(PetIsCuddled());

        if (m_Pet.PetType == PetType.Dog)
        {
            //if you ever wanted a pet to do special logic, you would put it here! 
        }
    }

    private IEnumerator PetIsCuddled()
    {
        UIManager.m_CuddlePetButton.interactable = false;
        m_CurrentHappiness += m_Pet.HappinessAddedWhenPet;
        UpdatePetHappiness(); 

        // If our pet is as full as they can get, set their food points to 0 and tell the game to stop allowing the pet to get happer :) 
        if (m_CurrentHappiness > m_Pet.MaxPetStat)
        {
            m_CurrentHappiness = m_Pet.MaxPetStat;
        }

        yield return new WaitForSeconds(m_Pet.TimeBetweenCuddles);
        UIManager.m_CuddlePetButton.interactable = true;
    }

    public void UpdatePetHappiness()
    {
        UIManager.m_HappinessView.UpdatePetNeedsFillBar(m_CurrentHappiness, m_Pet.MaxPetStat, "Happiness");
    }
}