﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Enemy,
        Weapon
    }

    public enum PopupUIGroup
    {
        Unknown,
        GameMenuUI,
        PlayerStatUI,
    }

    public enum SceneUI
    {
        Unknown,
        PlayerUI,
        MainMenuUI,
        ItemBoxUI,
        LevelUpUI
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum UIEvent
    {
        Click,
        Drag,

    }
    public enum SceneType
    {
        Unknown,
        GameScene,
        MainMenuScene
    }

}
