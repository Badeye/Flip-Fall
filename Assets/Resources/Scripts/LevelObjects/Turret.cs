﻿using Impulse;
using Impulse.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private ParticleSystem shotPS;
    private Animation shotAnimation;

    // implement thoe by changing the particle system accordingly.
    public float shotDelay = 1F;
    public float startupDelay = 0F;

    // not yet implemented
    private float shotSpeed = 1F;

    public bool constantFire = true;

    // Use this for initialization
    private void Start()
    {
        shotAnimation = GetComponent<Animation>();
        shotPS = GetComponent<ParticleSystem>();

        Player.onPlayerStateChange.AddListener(PlayerStateChanged);
        StartCoroutine(cFire());
    }

    private void PlayerStateChanged(Player.PlayerState playerState)
    {
        switch (playerState)
        {
            case Player.PlayerState.alive:
                break;

            case Player.PlayerState.dead:
                break;

            case Player.PlayerState.win:
                break;

            default:
                break;
        }
    }

    private IEnumerator cFire()
    {
        yield return new WaitForSeconds(startupDelay);
        while (constantFire)
        {
            yield return new WaitForSeconds(shotDelay);
            Fire();
        }
        yield break;
    }

    private void Fire()
    {
        //shotPS.Stop();
        SoundManager.TurretShot(new Vector3(transform.position.x, transform.position.y, Constants.playerZ));
        shotPS.Play();
        shotAnimation["turretShooting"].time = shotDelay / 1.5F;
        shotAnimation.Play("turretShooting");
    }
}