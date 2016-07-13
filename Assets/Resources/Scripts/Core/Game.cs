﻿using Sliders;
using Sliders.Levels;
using Sliders.Progress;
using Sliders.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sliders
{
    public class Game : MonoBehaviour
    {
        public enum GameState { titlescreen, tutorial, ready, playing, pause, deathscreen, finishscreen, editor, settingsscreen }

        public static GameState gameState;
        public static GameStateChangeEvent onGameStateChange = new GameStateChangeEvent();

        public class GameStateChangeEvent : UnityEvent<GameState> { }

        public static CameraMovement cm;

        public static Game instance;

        private int switchDelay = 2;

        private void Awake()
        {
            instance = this;
            ProgressManager.ClearProgress();
            ProgressManager.LoadProgressData();
            SetGameState(GameState.ready);
        }

        private void Start()
        {
        }

        private void OnApplicationQuit()
        {
            Debug.Log("Game Closed down");
            ProgressManager.SaveProgressData();
        }

        // Only set the GameState through this. All other classes will be able to use GameState listeners.
        public static void SetGameState(GameState gs)
        {
            gameState = gs;

            //Executed before event is fired!
            switch (gs)
            {
                case GameState.deathscreen:
                    CameraShake.Shake();
                    instance.StartCoroutine(DelayedGameStateSwitch());
                    //Delay for delayTime
                    //execute UI animationset in Time delayTime
                    //SetReady
                    break;

                case GameState.finishscreen:
                    onGameStateChange.Invoke(gameState);
                    break;

                case GameState.playing:
                    onGameStateChange.Invoke(gameState);
                    break;

                case GameState.ready:
                    onGameStateChange.Invoke(gameState);
                    break;

                default:
                    onGameStateChange.Invoke(gameState);
                    break;
            }
        }

        public static IEnumerator DelayedGameStateSwitch()
        {
            yield return new WaitForSeconds(1);
            onGameStateChange.Invoke(gameState);
        }

        public void Edit()
        {
            SetGameState(GameState.editor);
        }

        public void CloseGame()
        {
        }

        public void LoadLevel(int levelID)
        {
            //player.
            //currentlevel = levelloader.load(levelID);
        }

        public static void RestartLevel()
        {
            //cam to beginning
            //player to beginning
            //restart timer
        }
    }
}