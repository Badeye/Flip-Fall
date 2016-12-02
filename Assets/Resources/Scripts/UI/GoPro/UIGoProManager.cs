﻿using FlipFall.Audio;
using FlipFall.Levels;
using FlipFall.Progress;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages all UI Elements of the respective scene
/// </summary>

namespace FlipFall.UI
{
    public class UIGoProManager : MonoBehaviour
    {
        //public enum UIState { levelSelection, home, settings, game, title, shop, editor, credits, buyPro }
        //public static UIState uiState;
        public static UIGoProManager _instance;

        public Animator animator;
        //private buy

        private void Start()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            _instance = this;

            Main.onSceneChange.AddListener(SceneChanging);
        }

        private static bool IsProUnlocked()
        {
            //checks
            return true;
        }

        private void SceneChanging(Main.Scene scene)
        {
            animator.SetTrigger("fadeout");
        }

        public void HomeButtonClicked()
        {
            SoundManager.ButtonClicked();
            Main.SetScene(Main.Scene.home);
        }

        public void ProButtonClicked()
        {
            if (!IsProUnlocked())
            {
                // buy
                // if buy successfull add pro notice to progress
            }
            SoundManager.ButtonClicked();
            Main.SetScene(Main.Scene.home);
        }
    }
}