using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfig : MonoBehaviour
{
    public GameObject objToFollow;

    public bool followRabbit;

    public AudioClip music, rabbitWalksSound, rabbitDiesSound, rabbitFallsSound,
                     enemyAttacksSound, soundOfTheVictory, soundOfTheLosing;
    private AudioSource musicSrc, rabbitWalksSrc, rabbitDiesSrc, rabbitFallsSrc,
                        enemyAttacksSrc, soundOfTheVictorySrc, soundOfTheLosingSrc;

    void Start()
    {
        followRabbit = true;
        LevelController.current.cameraWhichLooksForRabbit = this;

        setMusicSource();
        setSoundSources();
        setWonPanelSource();
        setLosingPanelSource();
    }

    private void setLosingPanelSource()
    { setClipForSrc(ref soundOfTheLosingSrc, soundOfTheLosing, false); }

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
        if (!rabbitWalksSrc.isPlaying && soundIsOn())
        { rabbitWalksSrc.Play(); }
    }

    private bool soundIsOn()
    {
        return SoundManager.Instance.isSoundOn();
    }

    public void playSoundRabbitDies()
    {
        if (!rabbitDiesSrc.isPlaying && soundIsOn())
        { rabbitDiesSrc.Play(); }
    }

    public void playSoundRabbitFalls()
    {
        if (!rabbitFallsSrc.isPlaying && soundIsOn())
        { rabbitFallsSrc.Play(); }
    }

    public void playSoundEnemyAttacks()
    {
        if (!enemyAttacksSrc.isPlaying && soundIsOn())
        { enemyAttacksSrc.Play(); }
    }

    public void playSoundOfTheVictory()
    {
        if (!soundOfTheVictorySrc.isPlaying && soundIsOn())
        { soundOfTheVictorySrc.Play(); }
    }

    public void playSoundOfTheLosing()
    {
        if (!soundOfTheLosingSrc.isPlaying && soundIsOn())
        { soundOfTheLosingSrc.Play(); }
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

    public void stopSoundOfTheLosing()
    {
        if (soundOfTheLosingSrc.isPlaying)
        { soundOfTheLosingSrc.Stop(); }
    }

    private void checkMusicAndStopOrStartIfNecessary()
    {
        if (SoundManager.Instance.isMusicOn() && !musicSrc.isPlaying)
        { musicSrc.Play(); }
        else if(!SoundManager.Instance.isMusicOn() && musicSrc.isPlaying)
        { musicSrc.Stop(); }
    }
}
