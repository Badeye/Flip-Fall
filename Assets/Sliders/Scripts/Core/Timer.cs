﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Sliders.UI
{
    public static class Timer
    {
        #region Properties

        public static string Format { get; set; }
        public static bool IsPaused { get; private set; }
        public static bool IsStarted { get; private set; }
        public static Text TextObject { get; private set; }
        public static double PassedTime { get; private set; }

        private static MonoBehaviour Behaviour { get; set; }

        #endregion Properties

        #region Public Methods

        public static void Start(MonoBehaviour mono, Text textObject = null)
        {
            IsPaused = true;
            Debug.Log("T - Start");
            if (mono == null)
            {
                Debug.LogError("Behaviour may not be null!");
                return;
            }

            if (textObject != null)
            {
                TextObject = textObject;
            }
            else
            {
                if (TextObject == null)
                {
                    Debug.LogError("No Text object!");
                    return;
                }
            }

            if (!IsStarted)
            {
                PassedTime = 0;
                TextObject = textObject;
                IsStarted = true;
                TextObject = textObject;
                Behaviour = mono;
                Behaviour.StartCoroutine(WaitForTimerUpdate());
            }
            else
            {
                Debug.Log("Timer already started");
            }
        }

        public static void Pause()
        {
            Debug.Log("T - Pause");
            IsPaused = true;
        }

        public static void Reset()
        {
            Debug.Log("T - Pause");
            PassedTime = 0;
        }

        public static void Continue()
        {
            Debug.Log("T - Pause");
            IsPaused = false;
            Behaviour.StartCoroutine(WaitForTimerUpdate());
        }

        public static void Stop()
        {
            Debug.Log("T - Pause");
            IsStarted = false;
        }

        #endregion Public Methods

        private static IEnumerator WaitForTimerUpdate()
        {
            Debug.Log("T - Num - Enter");
            while (!IsPaused && IsStarted)
            {
                Debug.Log("T - Num - While");
                PassedTime += Time.fixedDeltaTime;
                var time = TimeSpan.FromSeconds(PassedTime);
                try
                {
                    TextObject.text = string.Format("{0:D1},{1:D2}",
                                                      time.Seconds,
                                                      time.Milliseconds / 10);
                }
                catch (ArgumentNullException anex)
                {
                    Debug.LogError(anex.Message);
                }
                catch (FormatException fex)
                {
                    Debug.LogError(fex.Message);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
}