using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCode
{
    public const int MAX_HEART_FRAGMENTS = 2;

    public event EventHandler onDamaged;
    public event EventHandler onHealed;
    public event EventHandler onDead;
    public event EventHandler onAddNewHeart;

    private List<Heart> totalHearts;

    public HeartCode(int heartAmount)
    {
        totalHearts = new List<Heart>();
        for (int i = 0; i < heartAmount; i++)
        {
            Heart tempHeart = new Heart(2);
            totalHearts.Add(tempHeart);
        }
    }

    public void AddNewHeart()
    {
        Heart tempHeart = new Heart(2);
        totalHearts.Insert(0, tempHeart);

        if (onAddNewHeart != null) onAddNewHeart(this, EventArgs.Empty);
    }

    public List<Heart> GetHeartList()
    {
        return totalHearts;
    }

    public void Damage(int damageAmount)
    {
        for (int i = totalHearts.Count - 1; i >= 0; i--)
        {
            Heart heart = totalHearts[i];
            if(damageAmount > heart.GetFragmentAmount())
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
            if(onDamaged != null) onDamaged(this, EventArgs.Empty);

        if (isDead())
        {
            if (onDead != null) onDead(this, EventArgs.Empty);
        }
    }

    public void Heal(int healAmount)
    {
        for (int i = 0; i < totalHearts.Count; i++)
        {
            Heart heart = totalHearts[i];
            int missingFragments = MAX_HEART_FRAGMENTS - heart.GetFragmentAmount();

            if(healAmount > missingFragments)
            {
                healAmount -= missingFragments;
                heart.Heal(missingFragments);
            }
            else
            {
                heart.Heal(healAmount);
                break;
            }

        }
        if (onHealed != null) onHealed(this, EventArgs.Empty);
    }

    public bool isDead()
    {
        return totalHearts[0].GetFragmentAmount() <= 0;
    }

    public class Heart
    {
        private int fragments;

        public Heart(int fragments)
        {
            this.fragments = fragments;
        }

        public int GetFragmentAmount()
        {
            return fragments;
        }

        public void SetFragmentAmount(int fragments)
        {
            this.fragments = fragments;
        }

        public void Damage(int damageAmount)
        {
            if(damageAmount >= fragments)
            {
                fragments = 0;
            }
            else
            {
                fragments -= damageAmount;
            }
        }

        public void Heal(int healAmount)
        {
            if(fragments + healAmount > MAX_HEART_FRAGMENTS)
            {
                fragments = MAX_HEART_FRAGMENTS;
            }
            else
            {
                fragments += healAmount;
            }
        }
    }
}
