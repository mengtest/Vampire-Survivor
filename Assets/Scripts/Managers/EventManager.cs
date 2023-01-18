using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EventManager
{
    public enum PlayerStats
    {
        MaxHP,
        MoveSpeed,
        Damage,
        Defense,
        Cooldown,
        Amount
    }


    public List<string[]> SetRandomItem(PlayerStat player, int Maxcount)
    {
        int i = 0;
        List<string[]> PoolList = new List<string[]>();
        while(i < Maxcount)
        {
            string[] selected = new string[2];
            float random = Random.Range(0, 100);
            int rd = 0;

            if (random < 30)
                rd = 0;
            else if (random < 50)
                rd = 1;
            else
                rd = 2;

            if (rd== 0)
            {
                if (player.GetWeaponDict()[player.playerStartWeapon] >= 5)
                    continue;
                selected[0] = "0";
                selected[1] = player.playerStartWeapon.ToString();
            }
            if (rd == 1)
            {
                selected[0] = "1";
                PlayerStats stat = SetRandomStat();
                selected[1] = stat.ToString();
            }
            else
            {
                selected[0] = "2";
                Define.Weapons weapon = SetRandomWeapon();
                if (player.GetWeaponDict().GetValueOrDefault<Define.Weapons, int>(weapon) >= 5)
                    continue;
                if (player.GetWeaponDict().Count >= 4 && !player.GetWeaponDict().ContainsKey(weapon))
                    continue;
                selected[1] = weapon.ToString();
            }

            bool isContains = false;
            foreach (string[] type in PoolList)
            {
                if(selected[1] == type[1])
                {
                    isContains = true;
                    break;
                }
            }
            if (isContains)
                continue;
            PoolList.Add(selected);
            i++;
        }
        return PoolList;
    }

    public PlayerStats SetRandomStat()
    {
        int _statNum = Random.Range(0, System.Enum.GetValues(typeof(PlayerStats)).Length);
        PlayerStats playerStats = (PlayerStats)_statNum;
        return playerStats;
    }

    public Define.Weapons SetRandomWeapon()
    {
        int weaponNum = Random.Range(1, System.Enum.GetValues(typeof(Define.Weapons)).Length+1 - System.Enum.GetValues(typeof(Define.PlayerStartWeapon)).Length);
        Define.Weapons playerWeapon = (Define.Weapons)weaponNum;

        return playerWeapon;
    }

    public Define.Weapons SetRandomWeaponInItem()
    {
        int weaponNum = Random.Range(1, System.Enum.GetValues(typeof(Define.Weapons)).Length - System.Enum.GetValues(typeof(Define.PlayerStartWeapon)).Length);
        Define.Weapons playerWeapon = (Define.Weapons)weaponNum;
        return playerWeapon;
    }

    public void LevelUpEvent()
    {
        Managers.UI.ShowPopupUI<UI_LevelUp>();
        Managers.GamePause();
    }

    public void LevelUpOverEvent(int itemType, string itemName)
    {
        //PlayerStatorWeaponUp
        PlayerStat player = Managers.Game.getPlayer().GetComponent<PlayerStat>();
        if (itemType == 1)
        {
            switch (itemName)
            {
                case "MaxHP":
                    player.MaxHP += 10;
                    player.HP = player.MaxHP;
                    break;
                case "MoveSpeed":
                    player.MoveSpeed += 1;
                    break;
                case "Damage":
                    player.Damage += 10;
                    break;
                case "Defense":
                    player.Defense += 1;
                    break;
                case "Cooldown":
                    player.Cooldown += 10;
                    break;
                case "Amount":
                    player.Amount += 1;
                    break;
            }
            player.AddOrSetWeaponDict(player.playerStartWeapon, 0);
        }
            
        else
        {
            Define.Weapons weaponType = (Define.Weapons)System.Enum.Parse(typeof(Define.Weapons), itemName);
            player.AddOrSetWeaponDict(weaponType, 1);
        }
            

        Managers.UI.ClosePopupUI(Define.PopupUIGroup.UI_LevelUp);
    }

    public void ShowItemBoxUI()
    {
        Managers.UI.ShowPopupUI<UI_ItemBoxOpen>();
        Managers.GamePause();
    }
    public List<Define.Weapons> SetRandomWeaponfromItemBox(PlayerStat player)
    {
        bool weaponFull = true;
        if (player.GetWeaponDict().Count >= 4)
        {
            foreach(KeyValuePair<Define.Weapons, int> weapon in player.GetWeaponDict())
            {
                if (weapon.Key == player.playerStartWeapon)
                    continue;
                if(weapon.Value < 5)
                {
                    weaponFull = false;
                    break;
                }
            }
            if (weaponFull)
            {
                return null;
            }
        }
        int maxCount = 3;
        int rd = Random.Range(1, maxCount+1);
        int i = 0;
        List<Define.Weapons> weaponList = new List<Define.Weapons>();
        while(i < rd)
        {
            Define.Weapons wp = SetRandomWeaponInItem();
            int weaponlevel = player.GetWeaponDict().GetValueOrDefault<Define.Weapons, int>(wp);
            if (weaponlevel >= 5 || (player.GetWeaponDict().Count == 4 && weaponlevel == 0))
                continue;
            weaponList.Add(wp);
            i++;
        }

        return weaponList;
    }

    public void SetLevelUpWeaponfromItemBox(List<Define.Weapons> weaponList, PlayerStat player)
    {
        if(weaponList == null)
        {
            player.HP += 30;
            return;
        }
        foreach(Define.Weapons weaponType in weaponList)
        {
            player.AddOrSetWeaponDict(weaponType, 1);
        }
        
    }

    public void PlayHitEnemyEffectSound()
    {
        int rd = Random.Range(1, 3);
        switch (rd)
        {
            case 1:
                Managers.Sound.Play("Hit0");
                break;
            case 2:
                Managers.Sound.Play("Hit1");
                break;
        }
    }

    public void PlayHitPlayerEffectSound()
    {
        Managers.Sound.Play("Hit_01");
    }

}
