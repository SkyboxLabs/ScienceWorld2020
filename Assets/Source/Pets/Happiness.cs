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
    //These are our public variables - these can be accessed anywhere else in the project. 
    public UIViewManager m_UIViewManager; //This is a refrence to the script that will update all of the User Interface related to this system

    //These are public variables with a private Set meaning it allows us to acces the variable anywhere else, but only modify it in this script.
    public bool CanBeSnuggled { get; private set; } //This is a True/False variable that lets us know if the pet can be interacted with.
    public double CurrentHappiness { get; private set; } //This is a number variable that lets us know what the pet's current happiness is at

    public bool IsLowHappiness { get; private set; } 

    //These are our private variables. These can only be accessed and modified in this script.
    private bool m_CanGetSadder = true;
    private Pet m_Pet;

    private void Awake() //Unity calls this when the script is turned on for the first time
    {
        m_Pet = GetComponent<Pet>(); //Get the instace of the Pet Script attatched to this game object

        if (m_Pet == null) // [Advanced] If the pet controller is not found, print this error to the Consle window. This way we know why the code is breaking!
        {
            print("Failed to initalize pet. Missing " + gameObject.name + "'s pet script! " +
                "Make sure to add it by hitting 'Add Component' in the inspector(right side) on the pet, type 'Pet' and hit the 'Pet' script");
        }

        if (!m_UIViewManager)
        {
            Debug.LogWarning("Currently your Pet has no UI View Manager - be sure to assign it one so it works! Look for 'UIViewManager' in the hierarchy(left side");
        }

        if (!m_Pet.m_IsInitilized)
        {
            m_Pet.Initilize();
        }

        CurrentHappiness = m_Pet.m_StartingHappiness; //Initilize our pet's current happiness to be the happiness we determined in Pet (Our numbers for designers)

        CanBeSnuggled = true; //Can our pet be snuggled? Yes! This will be used to controll the "snuggle" button. 
        IsLowHappiness = false; 
    }

    private void Update() // Unity will call this every frame
    {
        if (m_CanGetSadder && !IsLowHappiness) // If the pet can get sadder run the next code
        {
            StartCoroutine(GetSadder()); // Coroutine is a special type of function - more on this later!
        }
    }

    IEnumerator GetSadder()
    {
        m_CanGetSadder = false;
        yield return new WaitForSeconds(m_Pet.m_HappinessDecayInSeconds);

        CurrentHappiness -= m_Pet.m_HappinessRemovedOverTime;

        // If our pet is as sad as they can get, set their food points to 0 and tell the game to stop allowing the pet to get sadder :(
        if (CurrentHappiness <= 0)
        {
            CurrentHappiness = 0;
            IsLowHappiness = true;
        }

        m_CanGetSadder = true;
    }

    public void Cuddle()
    {
        m_Pet.SetAnimatorTrigger(AnimatorTriggers.Cuddled);
        StartCoroutine(PetIsCuddled());

        if (m_Pet.m_PetType == PetType.Dog)
        {
            //if you ever wanted a pet to do special logic, you would put it here! 
        }
    }

    private IEnumerator PetIsCuddled()
    {
        // You want to turn off the button during this time so users cannot set off too many functions of what are called coroutines (functions that can come back) 
        CanBeSnuggled = false;
        IsLowHappiness = false; 

        //This is how you add to a variable in most languages
        //Can also be writen as m_CurrentHappiness = m_CurrentHappiness + m_Pet.HappinessAddedWhenPet
        CurrentHappiness += m_Pet.m_HappinessAddedWhenPet;

        // If our pet is as full as they can get, set their food points to 0 and tell the game to stop allowing the pet to get happer :) 
        if (CurrentHappiness > m_Pet.MaxPetStat)
        {
            CurrentHappiness = m_Pet.MaxPetStat;
        }

        // This tells the function to wait how ever long m_Pet.TimeBetweenCuddles is before coming back and finishing the rest of the code
        yield return new WaitForSeconds(m_Pet.m_TimeBetweenCuddles);

        CanBeSnuggled = true;
    }

    private void OnDisable()
    {
        // There is a bug with the game - the coroutines disalbe when the pet is turned off, and when we swap between pets, we turn them off, thus not getting to the last part
        // of the coroutine code. This ensures that the game will still work, however, if the player toggles between pets quickly, our game tuning will no longer act as expected. 

        CanBeSnuggled = true;

        if (CurrentHappiness <= 0)
        {
            CurrentHappiness = 0;
        }
        else
        {
            m_CanGetSadder = true;
        }
    }
}