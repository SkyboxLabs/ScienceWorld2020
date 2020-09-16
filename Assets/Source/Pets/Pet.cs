using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PetType
{
    Dog,
    Cat,
}

public enum AnimatorEnums
{
    Happy,
}

public class Pet : MonoBehaviour
{
    // This class will hold all of the variables we need for our pets! Use the editor to adjust the values for each pet.
    // [Advanced] 
    // We use "Attribtutes" to make the edtior values easier for a designer to read. Learn more at docs.unity3d.com/ScriptRefrence/HeaderAttribute.html! 

    // Const means "Constant" or in other words, a value that never changes in the project. Any time we need a number that will not change, we assign
    // it the "Const" 
    private const int k_MinPetNeedStat = 0;
    private const int k_MaxPetNeedStat = 100;

    [Header("Pet Game Design Information")]
    [Header("Edit the following like a Game Designer would to create a pet!")]
    [Header("Hover over each category to learn more.")]
    [SerializeField]
    [Header("Basic Pet Information")]
    private PetType m_PetType;

    [Header("Pet's Hunger System")]

    [SerializeField]
    [Range(k_MinPetNeedStat, k_MaxPetNeedStat)]
    [Tooltip("How hungry is the pet when you start the game? 100 is full, 0 is hungry")]
    private int m_StartingHunger = 100;

    [SerializeField]
    [Range(k_MinPetNeedStat, k_MaxPetNeedStat)]
    [Tooltip("When the pet gets hungrier, how much hungrier do they get? In order to balance this, rememeber that 100 is full and 0 is hungry")]
    private int m_HungerAddedWhenHungry = 5;

    [SerializeField]
    [Range(k_MinPetNeedStat, k_MaxPetNeedStat)]
    [Tooltip("When you feed the pet, how much fuller do they get? In order to balance this, rememeber that 100 is full and 0 is hungry")]
    private int m_HungerRemovedWhenFed = 10;

    [SerializeField]
    [Range(15, 120)]
    [Tooltip("How many seconds does it take before the pet gets hungrier?")]
    private int m_HungerTimerInSeconds = 5;

    [SerializeField]
    [Range(0, 10)]
    [Tooltip("How many seconds does it take before the feed pet button can be pressed again?")]
    private int m_TimeBetweenFeeds = 8;

    [Header("Pet's Happiness System")]

    [SerializeField]
    [Range(k_MinPetNeedStat, k_MaxPetNeedStat)]
    [Tooltip("How happy is the pet when you start the game? 100 is happy 0 is sad")]
    private int m_StartingHappiness = 100;

    [SerializeField]
    [Range(k_MinPetNeedStat, k_MaxPetNeedStat)]
    [Tooltip("When the pet gets sadder, how much sadder do they get? In order to balance this, rememeber that 100 is happy and 0 is sad")]
    private int m_HappinessRemovedOverTime = 5;

    [SerializeField]
    [Range(k_MinPetNeedStat, k_MaxPetNeedStat)]
    [Tooltip("When you pet the pet, how much happier do they get? In order to balance this, rememeber that 100 is happy and 0 is sad")]
    private int m_HappinessAddedWhenPet = 10;

    [SerializeField]
    [Range(15, 120)]
    [Tooltip("How many seconds does it take before the pet gets sadder?")]
    private int m_HappinessDecayInSeconds = 5;

    [SerializeField]
    [Range(0, 10)]
    [Tooltip("How many seconds does it take before the pet - pet button can be pressed again?")]
    private int m_TimeBetweenCuddles = 8;

    // We use get/set so that this information can be accessed by another class, but not edited by another class! 
    public PetType PetType { get; private set; }
    public int StartingHunger { get; private set; }
    public int HungerRemovedWhenHungry { get; private set; }
    public int HungerRemovedWhenFed { get; private set; }
    public int HungerTimerInSeconds { get; private set; }
    public int TimeBetweenFeeds { get; private set; }
    public int StartingHappiness { get; private set; }
    public int HappinessRemovedOverTime { get; private set; }
    public int HappinessAddedWhenPet { get; private set; }
    public int HappinessDecayInSeconds { get; private set; }
    public int TimeBetweenCuddles { get; private set; }

    public int MaxPetStat { get; private set; }

    private Animator m_PetAnimator;

    internal bool m_IsInitilized = false; 

    public void Initilize()
    {
        PetType = m_PetType;
        StartingHunger = m_StartingHunger;
        HungerRemovedWhenHungry = m_HungerAddedWhenHungry;
        HungerRemovedWhenFed = m_HungerRemovedWhenFed;
        HungerTimerInSeconds = m_HungerTimerInSeconds;
        TimeBetweenFeeds = m_TimeBetweenFeeds;
        MaxPetStat = k_MaxPetNeedStat;
        StartingHappiness = m_StartingHappiness;
        HappinessRemovedOverTime = m_HappinessRemovedOverTime;
        HappinessAddedWhenPet = m_HappinessAddedWhenPet;
        HappinessDecayInSeconds = m_HappinessDecayInSeconds;

        m_PetAnimator = GetComponent<Animator>();

        m_IsInitilized = true; 
    }

    private void Awake()
    {
        if (!m_IsInitilized)
        {
            Initilize(); 
        }
       
    }
    public void SetAnimatorTrigger(AnimatorEnums state)
    {
        if (m_PetAnimator)
        {
            m_PetAnimator.SetTrigger(state.ToString());
        }
    }
}
