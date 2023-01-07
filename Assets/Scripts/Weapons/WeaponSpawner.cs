using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    protected GameObject _player;
    PlayerStat _playerStat;
    Dictionary<int, Data.WeaponData> _weaponData;
    Dictionary<int, Data.WeaponLevelData> _weaponStat;
    Animator _anime;


    public int _weaponID = 0;
    public int _level = 1;
    public int _damage=1;
    public float _movSpeed=1;
    public float _force=1;
    public float _cooldown=1;
    public float _size=1;
    public int _penetrate=1;
    public int _countPerCreate=1;


    
    void Start()
    {
        _player = transform.parent.gameObject;
        _playerStat = _player.GetComponent<PlayerStat>();
        _weaponData = Managers.Data.WeaponData;
        _anime = transform.GetComponent<Animator>();
        _weaponStat = MakeLevelDataDict(_weaponID);

    }

    protected virtual void Spawn()
    {

    }

    protected virtual void SetWeaponStat()
    {
        if(_level > 5)
            _level = 5;

        _damage = _weaponStat[_level].damage * _playerStat.Damage;
        _movSpeed = _weaponStat[_level].movSpeed;
        _force = _weaponStat[_level].force;
        _cooldown = _weaponStat[_level].cooldown;
        _size = _weaponStat[_level].size;
        _penetrate = _weaponStat[_level].penetrate;
        _countPerCreate = _weaponStat[_level].countPerCreate;
    }

    protected Dictionary<int, Data.WeaponLevelData> MakeLevelDataDict(int weaponID)
    {
        Dictionary<int, Data.WeaponLevelData> _weaponLevelData = new Dictionary<int, Data.WeaponLevelData>();
        foreach (Data.WeaponLevelData weaponLevelData in _weaponData[weaponID].weaponLevelData)
            _weaponLevelData.Add(weaponLevelData.level, weaponLevelData);
        return _weaponLevelData;

    }

    

}
