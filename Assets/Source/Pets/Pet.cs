using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enum is short for enumeration
//An enum variable type can be found in C, C++ and C#. We are currently using C# (pronounced C Sharp) which is why all the scripting files end in .cs (.CSharp)
//The idea is that instead of using an int(integer values like 0,1,2,3...) to represent a set of values, a type with a restricted set of values is used instead.
//TLDR: Basically in code Dog == 0 and Cat == 1, but using an enum lets us show those values in words makeing the code more readable 
public enum PetType
{
    Dog,
    Cat,
}

public enum AnimatorTriggers
{
    Cuddled,
    Fed,
}

public enum AnimatorBools
{
    IsSad, 
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

    [Header("Basic Pet Information")]
    public PetType m_PetType;

    [Header("Pet's Hunger System")]

    [Range(k_MinPetNeedStat, k_MaxPetNeedStat)]
    [Tooltip("How hungry is the pet when you start the game? 100 is full, 0 is hungry")]
    public int m_StartingHunger = 100;

    [Range(k_MinPetNeedStat, k_MaxPetNeedStat)]
    [Tooltip("When the pet gets hungrier, how much hungrier do they get? In order to balance this, rememeber that 100 is full and 0 is hungry")]
    public int m_HungerAddedWhenHungry = 5;

    [Range(k_MinPetNeedStat, k_MaxPetNeedStat)]
    [Tooltip("When you feed the pet, how much fuller do they get? In order to balance this, rememeber that 100 is full and 0 is hungry")]
    public int m_HungerRemovedWhenFed = 10;

    [Range(15, 120)]
    [Tooltip("How many seconds does it take before the pet gets hungrier?")]
    public int m_HungerTimerInSeconds = 5;

    [Range(1, 10)]
    [Tooltip("How many seconds does it take before the feed pet button can be pressed again?")]
    public int m_TimeBetweenFeeds = 8;

    [Header("Pet's Happiness System")]

    [Range(k_MinPetNeedStat, k_MaxPetNeedStat)]
    [Tooltip("How happy is the pet when you start the game? 100 is happy 0 is sad")]
    public int m_StartingHappiness = 100;

    [Range(k_MinPetNeedStat, k_MaxPetNeedStat)]
    [Tooltip("When the pet gets sadder, how much sadder do they get? In order to balance this, rememeber that 100 is happy and 0 is sad")]
    public int m_HappinessRemovedOverTime = 5;

    [Range(k_MinPetNeedStat, k_MaxPetNeedStat)]
    [Tooltip("When you pet the pet, how much happier do they get? In order to balance this, rememeber that 100 is happy and 0 is sad")]
    public int m_HappinessAddedWhenPet = 10;

    [Range(15, 120)]
    [Tooltip("How many seconds does it take before the pet gets sadder?")]
    public int m_HappinessDecayInSeconds = 5;

    [Range(1, 10)]
    [Tooltip("How many seconds does it take before the pet - pet button can be pressed again?")]
    public int m_TimeBetweenCuddles = 8;

    //This is called a Property
    //Properties are getter and setter functions. You can either have a property control another variable or be like MaxPetStat below and be its own variable
    public int HungerRemovedWhenHungry { get { return m_HungerAddedWhenHungry; } }

    public int MaxPetStat { get; private set; }

    private Animator m_PetAnimator;

    internal bool m_IsInitilized = false;

    public void Initilize()
    {
        MaxPetStat = k_MaxPetNeedStat;
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

    public void SetAnimatorTrigger(AnimatorTriggers state)
    {
        if (m_PetAnimator)
        {
            m_PetAnimator.SetTrigger(state.ToString());
        }
    }

    public void SetAnimatorBool(AnimatorBools state, bool IsActive)
    {
        if (m_PetAnimator)
        {
            m_PetAnimator.SetBool(state.ToString(), IsActive);
        }
    }
}
