﻿using Impulse.Objects;
using Impulse.Progress;
using Impulse.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Impulse.Levels
{
    [Serializable]
    public class Level : MonoBehaviour
    {
        public static LevelUpdateEvent onGameStateChange = new LevelUpdateEvent();

        public class LevelUpdateEvent : UnityEvent<Level> { }

        //id is unique, no doubles allowed!
        public int id;
        public double presetTime = -1;

        public string title;

        //private Ghost ghost;
        [HideInInspector]
        public Spawn spawn;

        [HideInInspector]
        public Finish finish;

        // used for collision detection in Player class
        public List<Bounds> moveBounds = new List<Bounds>();

        private void Awake()
        {
            //ghost = gameObject.GetComponentInChildren<Ghost>();
            spawn = gameObject.GetComponentInChildren<Spawn>();
            finish = gameObject.GetComponentInChildren<Finish>();

            int sortingcount = -5;
            MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in meshRenderers)
            {
                if (mr.transform.tag == "MoveArea")
                {
                    // should be worldspace, dont use the meshfilter.bounds, it'll be local space
                    Bounds b = mr.gameObject.GetComponent<Renderer>().bounds;

                    moveBounds.Add(b);
                    Debug.Log(b + " -- id --- " + moveBounds.Count);

                    mr.sortingOrder = sortingcount;
                    sortingcount--;
                }
            }
            //sprite.sortingOrder = 2;
        }

        public int GetID()
        {
            return id;
        }

        public void AddObject(GameObject go)
        {
        }

        public void RemoveObject(GameObject go)
        {
        }

        public void SetTitle(String newTitle)
        {
            title = newTitle;
        }

        public string GetTitle()
        {
            return title;
        }
    }
}