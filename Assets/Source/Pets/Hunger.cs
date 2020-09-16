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
    private bool m_CanBeFed = true;
    private double m_HungerTimer;
    private double m_CurrentHunger;

    private Pet m_Pet;

    private void Awake()
    {
        m_Pet = GetComponent<Pet>();

        // [Advanced] If the pet controller is not found, print this error to the Consle window. This way we know why the code is breaking!
        if (m_Pet == null)
        {
            print("Failed to initalize pet. Missing " + gameObject.name + "'s pet controller!");
        }

        if (!m_Pet.m_IsInitilized)
        {
            m_Pet.Initilize();
        }

        m_CurrentHunger = m_Pet.StartingHunger;
        m_CanBeFed = true;
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
        yield return new WaitForSeconds(m_Pet.HungerTimerInSeconds);

        m_CurrentHunger -= m_Pet.HungerRemovedWhenHungry;
        m_UIViewManager.m_HungerView.UpdatePetNeedsFillBar(m_CurrentHunger, m_Pet.MaxPetStat, "Hunger");

        // If our pet is as hungry as they can get, set their food points to 0 and tell the game to stop allowing the pet to get hungrier
        if (m_CurrentHunger <= 0)
        {
            m_CurrentHunger = 0;
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
        m_UIViewManager.m_FeedPetButton.interactable = false;
        m_CurrentHunger += m_Pet.HungerRemovedWhenFed;

        // If our pet is as full as they can get, set their food points to 0 and tell the game to stop allowing the pet to get hungrier
        if (m_CurrentHunger > m_Pet.MaxPetStat)
        {
            m_CurrentHunger = m_Pet.MaxPetStat;
        }

        UpdatePetHunger();

        yield return new WaitForSeconds(m_Pet.TimeBetweenFeeds);
        
        m_UIViewManager.m_FeedPetButton.interactable = true;
    }

    public void UpdatePetHunger()
    {
        m_UIViewManager.m_HungerView.UpdatePetNeedsFillBar(m_CurrentHunger, m_Pet.MaxPetStat, "Hunger");
    }
}
