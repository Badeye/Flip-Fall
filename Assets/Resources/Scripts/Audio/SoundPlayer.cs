﻿using Impulse.Cam;
using Impulse.Progress;
using Impulse.UI;
using System.Collections;
using UnityEngine;

/// <summary>
/// Sound playing component of the SoundManager
/// </summary>

namespace Impulse.Audio
{
    public class SoundPlayer : MonoBehaviour
    {
        public static SoundPlayer _instance;     //Allows other scripts to call functions from SoundManager.

        public AudioSource sfxSource;                   //Drag a reference to the audio source which will play the sound effects.
        public AudioSource sfxSourceRdmPitch;
        public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.

        public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
        public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

        public AudioListener audioListener;

        //storage, values gathered from progress.settings on each scene change
        private float fxVolume = 1;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
            Main.onSceneChange.AddListener(SceneChanged);
            UISettingsManager.onFXVolumeChange.AddListener(AdjustFXVolume);
            UISettingsManager.onMusicVolumeChange.AddListener(AdjustMusicVolume);
        }

        private void OnEnable()
        {
            musicSource.volume = ProgressManager.GetProgress().settings.musicVolume;
            fxVolume = ProgressManager.GetProgress().settings.fxVolume;
        }

        public void AdjustMusicVolume(float newVolume)
        {
            musicSource.volume = newVolume;
        }

        public void AdjustFXVolume(float newVolume)
        {
            fxVolume = newVolume;
        }

        public void SceneChanged(Main.Scene s)
        {
            musicSource.volume = ProgressManager.GetProgress().settings.musicVolume;
            fxVolume = ProgressManager.GetProgress().settings.fxVolume;
        }

        // Used to play single sound clips.
        public void PlaySingle(AudioClip clip)
        {
            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            sfxSource.pitch = 1;

            //Play the clip.
            sfxSource.PlayOneShot(clip, fxVolume);
        }

        public AudioSource PlaySingleAt(AudioClip clip, Vector3 pos, float distanceToPlayer)
        {
            float maxDistance = 135;
            if (distanceToPlayer < maxDistance)
            {
                GameObject tempGO = new GameObject("TempFXAudio"); // create the temp object
                tempGO.transform.position = pos; // set its position
                tempGO.transform.parent = transform;
                AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
                aSource.clip = clip; // define the clip

                float volumeMulti = Mathf.InverseLerp(0, maxDistance, distanceToPlayer);

                aSource.volume = fxVolume * volumeMulti;
                aSource.maxDistance = 135;
                aSource.spatialBlend = 0F; // without thi the audio will be hearable everywhere
                aSource.dopplerLevel = 0F;
                aSource.reverbZoneMix = 0F;
                // set other aSource properties here, if desired
                aSource.Play(); // start the sound
                Destroy(tempGO, clip.length); // destroy object after clip duration
                return aSource; // return the AudioSource reference
            }
            return null;
        }

        // Used to play the background music
        public void PlayMusic(AudioClip clip)
        {
            musicSource.volume = ProgressManager.GetProgress().settings.musicVolume;

            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            musicSource.pitch = 1;

            //Play the clip.
            musicSource.clip = clip;
            musicSource.Play();
        }

        //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
        public void RandomizeSfx(params AudioClip[] clips)
        {
            //Generate a random number between 0 and the length of our array of clips passed in.
            int randomIndex = Random.Range(0, clips.Length);

            //Choose a random pitch to play back our clip at between our high and low pitch ranges.
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);

            //Set the pitch of the audio source to the randomly chosen pitch.
            sfxSourceRdmPitch.pitch = randomPitch;

            //Set the clip to the clip at our randomly chosen index.
            sfxSourceRdmPitch.PlayOneShot(clips[randomIndex], fxVolume);
        }
    }
}