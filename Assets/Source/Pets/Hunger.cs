using System.Collections;
using UnityEngine;

/*
  [Advanced]
  - This line of code makes sure that if a GameObject has a "Pet Hunger" script attatched, it also has a "Pet"
  - This script requires the Pet Controller to work!
*/

[RequireComponent(typeof(Pet))]

public class Hunger : MonoBehaviour
{
    public UIViewManager m_UIViewManager;

    // Our inital variables to start the game. 
    private bool m_CanGetHungrier = true;
    public bool CanBeFed { get; private set; }
    public double CurrentHunger { get; private set; }

    private Pet m_Pet;

    private void Awake() //Unity calls this when the script is turned on for the first time
    {
        //Grabbing the component 'Pet' off of the object this scripted is attacted to
        m_Pet = GetComponent<Pet>();

        if (m_Pet == null) // [Advanced] If the pet controller is not found, print this error to the Console window. This way we know why the code is breaking!
        {
            print("Failed to initalize pet. Missing " + gameObject.name + "'s pet script! " +
                "Make sure to add it by hitting 'Add Component' in the inspector(right side) on the pet, type 'Pet' and hit the 'Pet' script");
        }

        if (!m_UIViewManager)
        {
            Debug.LogWarning("Currently your PetManager has no UI View Manager - be sure to assign it one so it works! Look for 'UIViewManager' in the hierarchy(left side");
        }

        if (!m_Pet.m_IsInitilized)
        {
            m_Pet.Initilize();
        }

        CurrentHunger = m_Pet.m_StartingHunger;
        CanBeFed = true;
    }

    private void Update() // Unity will call this every frame
    {
        // If the pet can get hungrier and the time in the game is the next interval of which the pet gets hungrier, make the pet get hungrier
        if (m_CanGetHungrier)
        {
            StartCoroutine(GetHungrier());
        }
    }

    IEnumerator GetHungrier()
    {
        m_CanGetHungrier = false;
        yield return new WaitForSeconds(m_Pet.m_HungerTimerInSeconds);

        CurrentHunger -= m_Pet.HungerRemovedWhenHungry;

        // If our pet is as hungry as they can get, set their food points to 0 and tell the game to stop allowing the pet to get hungrier
        if (CurrentHunger <= 0)
        {
            CurrentHunger = 0;
        }
        else
        {
            m_CanGetHungrier = true;
        }
    }

    public void Feed()
    {
        m_Pet.SetAnimatorTrigger(AnimatorEnums.Happy);
        StartCoroutine(GiveFood());
    }

    private IEnumerator GiveFood()
    {
        // You want to turn off the button during this time so users cannot set off too many functions of what are called coroutines (functions that can come back) 
        CanBeFed = false;
        CurrentHunger += m_Pet.m_HungerRemovedWhenFed;

        // If our pet is as full as they can get, set their food points to 0 and tell the game to stop allowing the pet to get hungrier
        if (CurrentHunger > m_Pet.MaxPetStat)
        {
            CurrentHunger = m_Pet.MaxPetStat;
        }

        // This tells the function to wait how ever long m_Pet.TimeBetweenCuddles is before coming back and finishing the rest of the code
        yield return new WaitForSeconds(m_Pet.m_TimeBetweenFeeds);

        CanBeFed = true;
    }

    private void OnDisable()
    {
        // There is a bug with the game - the coroutines disalbe when the pet is turned off, and when we swap between pets, we turn them off, thus not getting to the last part
        // of the coroutine code. This ensures that the game will still work, however, if the player toggles between pets quickly, our game tuning will no longer act as expected. 

        CanBeFed = true;

        if (CurrentHunger <= 0)
        {
            CurrentHunger = 0;
        }
        else
        {
            m_CanGetHungrier = true;
        }
    }
}
