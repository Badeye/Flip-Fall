﻿using Impulse.Progress;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Main Application Controller. Switches scenes upon calling SetScene() after a delay, giving other classes a timeframe
/// to save or initiate fade-in and fade-outs, since the scene switch is called after a delay post-event invoke.
/// </summary>

namespace Impulse
{
    public class Main : MonoBehaviour
    {
        public static Main _instance;

        /// <summary>
        /// Indicates which scene is curretly active -  switch with SetScene()
        /// </summary>
        public enum Scene { welcome, home, levelselection, tutorial, game, settings, editor, shop }
        public float sceneSwitchDelay = 0.5F;

        public static SceneChangeEvent onSceneChange = new SceneChangeEvent();

        private void Awake()
        {
            ProgressManager.ClearProgress();
            ProgressManager.LoadProgressData();

            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public static void SetScene(Scene newScene)
        {
            ProgressManager.SaveProgressData();
            onSceneChange.Invoke(newScene);
            switch (newScene)
            {
                case Scene.welcome:
                    if (SceneManager.GetActiveScene().name != "Welcome")
                        _instance.StartCoroutine(_instance.cSetScene("Welcome"));
                    break;

                case Scene.home:
                    if (SceneManager.GetActiveScene().name != "Home")
                        _instance.StartCoroutine(_instance.cSetScene("Home"));
                    break;

                case Scene.levelselection:
                    if (SceneManager.GetActiveScene().name != "Levelselection")
                        _instance.StartCoroutine(_instance.cSetScene("Levelselection"));
                    break;

                case Scene.tutorial:
                    if (SceneManager.GetActiveScene().name != "Tutorial")
                        _instance.StartCoroutine(_instance.cSetScene("Tutorial"));
                    break;

                case Scene.game:
                    if (SceneManager.GetActiveScene().name != "Game")
                        _instance.StartCoroutine(_instance.cSetScene("Game"));
                    break;

                case Scene.settings:
                    if (SceneManager.GetActiveScene().name != "Settings")
                        _instance.StartCoroutine(_instance.cSetScene("Settings"));
                    break;
            }
        }

        private IEnumerator cSetScene(string sceneName)
        {
            yield return new WaitForSeconds(sceneSwitchDelay);
            SceneManager.LoadScene(sceneName);
            yield break;
        }

        public class SceneChangeEvent : UnityEvent<Scene> { }
    }
}