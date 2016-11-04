﻿using FlipFall;
using FlipFall.Audio;
using FlipFall.Editor;
using FlipFall.Levels;

using FlipFall.Levels;

using FlipFall.Progress;
using FlipFall.Theme;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// The UI version of a custom level selectable in the EditorSelection scene
/// </summary>

[SerializeField]
public class UIEditorLevel : MonoBehaviour
{
    public enum LevelType { recieved, created };

    public enum LevelStatus { playable, nonPlayable };

    public LevelData levelData;

    [Header("UIEditorLevel Components")]
    public Text titleText;
    public Button editButton;
    public Button playButton;

    // prevent changes to toogle.isOn via script from invoking the tooglechange call and executing the Equip mothod
    private bool unvalidToogleCall = false;

    // corresponding product information data, stored in progress save-file
    private ProductInfo productInfo;

    public void Edit()
    {
        LevelEditor.editLevel = levelData;
        Main.SetScene(Main.Scene.editor);
    }

    // set texts to fit inspector variables
    public void UpdateTexts()
    {
        titleText.text = levelData.title;
        if (levelData.title == "")
        {
            titleText.text = "Custom Level";
        }
    }

    // set toggles to fit values gathered from the progress file
    public void UpdateButtons()
    {
        if (IsPlayable())
        {
            //playButton.gameObject.SetActive(true);
        }
    }

    // the level should be playable in theory, since it contains all elements a level needs for playing
    // ... check if impossible by simulating it by pathfinding
    private bool IsPlayable()
    {
        bool hasSpawnOnMoveArea = false;
        bool hasFinishOnMoveArea = false;

        //if (level.GetComponentInChildren<Spawn>(true))
        //{
        //    hasSpawnOnMoveArea = true;
        //}
        //if (level.GetComponentInChildren<Finish>(true))
        //{
        //    hasFinishOnMoveArea = true;
        //}

        return hasSpawnOnMoveArea && hasFinishOnMoveArea;
    }
}