using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    enum Weapons
    {
        Knife,
        fireball,
        spin,
        poison,
        lightning,
        shotgun
    }
    //1 : knife, 2 : firebal, 3: spin, 4: posion 5: lightning 6: shotgun
    private List<int> _autoWeaponList = new List<int>();
    private List<int> _cursorWeaponList = new List<int>();
    private int _exp;

    private bool isLevelUp = false;
    public int Exp
    {
        get { return _exp;}
        set
        {
            _exp = value;
            while (_exp >= MaxExp)
            {
                LevelUpEvent();
            }
        }
    }
    private int _maxExp = 1;

    public int MaxExp
    {
        get => _maxExp;
        set => _maxExp = value;
    }

    void Awake()
    {
        Init();
    }

    void Init()
    {
        Level = 1;
        HP = 50;
        MaxHP = 50;
        MoveSpeed = 5.0f;
        Attack = 1;
        Defense = 0;
        Exp = 0;
        MaxExp = 10;
    }

    void LevelUpEvent()
    {
        if (!isLevelUp)
        {
            //EventController's Event(if player level up, create Random Stat Selector UI)
            //when select is done, despawn UI, and check level up is over.
            
            //LevelUp
            _level += 1;
            _exp -= _maxExp;
            _maxExp *= 2;
        }
    }

}
