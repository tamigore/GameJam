using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthHealthSystem
{
    public event EventHandler OnDamaged;
    private List<Heart> heartList;

    public HearthHealthSystem(int heartAmount){
        heartList = new List<Heart>();
        for (int i = 0; i < heartAmount; i++){
            Heart heart = new Heart(4);
            heartList.Add(heart);
        }
    }
    public List<Heart> GetHeartList()
    {
        return heartList;
    }

    public void Damage(int damageAmount)
    {
        for (int i = heartList.Count - 1; i >= 0; i--)
        {
            Heart heart = heartList[i];
            if (damageAmount > heart.GetFragmentAmount())
            {
                damageAmount -= heart.GetFragmentAmount();
                heart.Damage(heart.GetFragmentAmount());
            }
            else
            {
                heart.Damage(damageAmount);
                break;
            }
        }
        if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);
    }

    public class Heart{
        public int fragments;

        public Heart (int fragments){
            this.fragments = fragments;
        }

        public int GetFragmentAmount(){
            return fragments;
        }
        public void SetFragments(int fragments) {
            this.fragments = fragments;
        }
        public void Damage(int damageAmount)
        {
            if(damageAmount >= fragments){
                fragments = 0;
            }else{
                fragments -= damageAmount;
            }
        }
    }
}
