using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PetDebugUI : MonoBehaviour
{
    public PetManager m_PetManager;

    [Header("Pet's Hunger System Sliders")]

    public PetDebugUIScroll m_HungerAddedWhenHungrySlider;
    public PetDebugUIScroll m_HungerRemovedWhenFedSlider;
    public PetDebugUIScroll m_HungerTimerInSeconds;
    public PetDebugUIScroll m_TimeBetweenFeedsSlider;

    [Header("Pet's Happiness System Sliders")]
    public PetDebugUIScroll m_HappinessRemovedOverTimeSlider;
    public PetDebugUIScroll m_HappinessAddedWhenPetSlider;
    public PetDebugUIScroll m_HappinessDecayInSecondsSlider;
    public PetDebugUIScroll m_TimeBetweenCuddlesSlider;

    //Don't have this
    private const int k_MaxPetNeedStat = 100;
    private const int k_MinPetTimerStat = 15;
    private const int k_MaxPetTimerStat = 120;
    private const int k_MinPetTimeBetweenStat = 1;
    private const int k_MaxPetTimeBetweenStat = 10;

    private bool m_IsInitialized;
    public void Initialize()
    {
        m_HungerAddedWhenHungrySlider.SetMaxValue(k_MaxPetNeedStat);
        m_HungerRemovedWhenFedSlider.SetMaxValue(k_MaxPetNeedStat);
        m_HungerTimerInSeconds.SetMinValue(k_MinPetTimerStat);
        m_HungerTimerInSeconds.SetMaxValue(k_MaxPetTimerStat);
        m_TimeBetweenFeedsSlider.SetMinValue(k_MinPetTimeBetweenStat);
        m_TimeBetweenFeedsSlider.SetMaxValue(k_MaxPetTimeBetweenStat);

        m_HappinessRemovedOverTimeSlider.SetMaxValue(k_MaxPetNeedStat);
        m_HappinessAddedWhenPetSlider.SetMaxValue(k_MaxPetNeedStat);
        m_HappinessDecayInSecondsSlider.SetMinValue(k_MinPetTimerStat);
        m_HappinessDecayInSecondsSlider.SetMaxValue(k_MaxPetTimerStat);
        m_TimeBetweenCuddlesSlider.SetMinValue(k_MinPetTimeBetweenStat);
        m_TimeBetweenCuddlesSlider.SetMaxValue(k_MaxPetTimeBetweenStat);
        m_IsInitialized = true;
    }

    //Unity calls this when the object is turned on
    public void OnEnable()
    {
        if(!m_IsInitialized)
        {
            Initialize();
        }

        //setting up inital values
        m_HungerAddedWhenHungrySlider.SetValue(m_PetManager.GetActivePet().m_HungerAddedWhenHungry);
        m_HungerRemovedWhenFedSlider.SetValue(m_PetManager.GetActivePet().m_HungerRemovedWhenFed);
        m_HungerTimerInSeconds.SetValue(m_PetManager.GetActivePet().m_HungerTimerInSeconds);
        m_TimeBetweenFeedsSlider.SetValue(m_PetManager.GetActivePet().m_TimeBetweenFeeds);

        m_HappinessRemovedOverTimeSlider.SetValue(m_PetManager.GetActivePet().m_HappinessRemovedOverTime);
        m_HappinessAddedWhenPetSlider.SetValue(m_PetManager.GetActivePet().m_HappinessAddedWhenPet);
        m_HappinessDecayInSecondsSlider.SetValue(m_PetManager.GetActivePet().m_HappinessDecayInSeconds);
        m_TimeBetweenCuddlesSlider.SetValue(m_PetManager.GetActivePet().m_TimeBetweenCuddles);
    }

    private void UpdateSlider(float value, ref int petValue, PetDebugUIScroll scroll)
    {
        if(!m_IsInitialized)
        {
            return;
        }

        petValue = (int)value;
        scroll.SetText(value);
    }

    public void UpdateHungerAddedWhenHungry(float value)
    {
        UpdateSlider(value, ref m_PetManager.GetActivePet().m_HungerAddedWhenHungry, m_HungerAddedWhenHungrySlider);
    }

    public void UpdateHungerRemovedWhenFed(float value)
    {
        UpdateSlider(value, ref m_PetManager.GetActivePet().m_HungerRemovedWhenFed, m_HungerRemovedWhenFedSlider);
    }

    public void UpdateHungerTimerInSeconds(float value)
    {
        UpdateSlider(value, ref m_PetManager.GetActivePet().m_HungerTimerInSeconds, m_HungerTimerInSeconds);
    }

    public void UpdateTimeBetweenFeeds(float value)
    {
        UpdateSlider(value, ref m_PetManager.GetActivePet().m_TimeBetweenFeeds, m_TimeBetweenFeedsSlider);
    }

    public void UpdateHappinessRemovedOverTime(float value)
    {
        UpdateSlider(value, ref m_PetManager.GetActivePet().m_HappinessRemovedOverTime, m_HappinessRemovedOverTimeSlider);
    }

    public void UpdateHappinessAddedWhenPet(float value)
    {
        UpdateSlider(value, ref m_PetManager.GetActivePet().m_HappinessAddedWhenPet, m_HappinessAddedWhenPetSlider);
    }

    public void UpdateHappinessDecayInSeconds(float value)
    {
        UpdateSlider(value, ref m_PetManager.GetActivePet().m_HappinessDecayInSeconds, m_HappinessDecayInSecondsSlider);
    }

    public void UpdateTimeBetweenCuddles(float value)
    {
        UpdateSlider(value, ref m_PetManager.GetActivePet().m_TimeBetweenCuddles, m_TimeBetweenCuddlesSlider);
    }
}
