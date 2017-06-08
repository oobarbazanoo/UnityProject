using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfig : MonoBehaviour
{
    public GameObject objToFollow;

    public bool followRabbit;

    // Use this for initialization
    void Start()
    {
        followRabbit = true;
        LevelController.current.cameraWhichLooksForRabbit = this;

        setMusicSource();
        setSoundSources();
        setWonPanelSource();
    }

    public AudioClip music, rabbitWalksSound, rabbitDiesSound, rabbitFallsSound, enemyAttacksSound, soundOfTheVictory;
    private AudioSource musicSrc, rabbitWalksSrc, rabbitDiesSrc, rabbitFallsSrc, enemyAttacksSrc, soundOfTheVictorySrc;

    private void setWonPanelSource()
    { setClipForSrc(ref soundOfTheVictorySrc, soundOfTheVictory, false);}

    private void setMusicSource()
    {setClipForSrc(ref musicSrc, music, true);}

    private void setSoundSources()
    {
        setClipForSrc(ref rabbitWalksSrc, rabbitWalksSound, false);
        setClipForSrc(ref rabbitDiesSrc, rabbitDiesSound, false);
        setClipForSrc(ref rabbitFallsSrc, rabbitFallsSound, false);
        setClipForSrc(ref enemyAttacksSrc, enemyAttacksSound, false);
    }

    private void setClipForSrc(ref AudioSource src, AudioClip clip, bool loop)
    {
        src = gameObject.AddComponent<AudioSource>();
        src.clip = clip;
        src.loop = loop;
    }

    void Update()
    {
        checkMusicAndStopOrStartIfNecessary();

        if (!followRabbit)
        { return; }

        Transform rabit_transform = objToFollow.transform;
        Transform camera_transform = this.transform;

        Vector3 rabit_position = rabit_transform.position;
        Vector3 camera_position = camera_transform.position;

        camera_position.x = rabit_position.x;
        camera_position.y = rabit_position.y;

        camera_transform.position = camera_position;
    }

    public void playSoundRabbitWalks()
    {
        if (!rabbitWalksSrc.isPlaying)
        { rabbitWalksSrc.Play(); }
    }

    public void playSoundRabbitDies()
    {
        if (!rabbitDiesSrc.isPlaying)
        { rabbitDiesSrc.Play(); }
    }

    public void playSoundRabbitFalls()
    {
        if (!rabbitFallsSrc.isPlaying)
        { rabbitFallsSrc.Play(); }
    }

    public void playSoundEnemyAttacks()
    {
        if (!enemyAttacksSrc.isPlaying)
        { enemyAttacksSrc.Play(); }
    }

    public void playSoundOfTheVictory()
    {
        if (!soundOfTheVictorySrc.isPlaying)
        { soundOfTheVictorySrc.Play(); }
    }

    public void stopSoundRabbitWalks()
    {
        if (rabbitWalksSrc.isPlaying)
        { rabbitWalksSrc.Stop(); }
    }

    public void stopSoundRabbitDies()
    {
        if (rabbitDiesSrc.isPlaying)
        { rabbitDiesSrc.Stop(); }
    }

    public void stopSoundRabbitFalls()
    {
        if (rabbitFallsSrc.isPlaying)
        { rabbitFallsSrc.Stop(); }
    }

    public void stopSoundEnemyAttacks()
    {
        if (enemyAttacksSrc.isPlaying)
        { enemyAttacksSrc.Stop(); }
    }

    public void stopSoundOfTheVictory()
    {
        if (soundOfTheVictorySrc.isPlaying)
        { soundOfTheVictorySrc.Stop(); }
    }

    private void checkMusicAndStopOrStartIfNecessary()
    {
        if (SoundManager.Instance.isMusicOn() && !musicSrc.isPlaying)
        { musicSrc.Play(); }
        else if(!SoundManager.Instance.isMusicOn() && musicSrc.isPlaying)
        { musicSrc.Stop(); }
    }
}
