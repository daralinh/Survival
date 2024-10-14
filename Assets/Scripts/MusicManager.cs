using System;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public Sound[] MusicSounds, SFXSounds;
    public AudioSource MusicSource, SFXSource, MagicBookSoundSource, SpawnDummySource;
    public AudioSource FireBallSource, LightningSource;

    public float coolDownPlaySFX;
    public float coolDownFireBall;
    public float coolDownLightningStrike;

    private float lastTimePlaySFX;
    private float lastTimeFireBall;
    private float lastTimeLightningStrike;
    
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        PlayMusic(EMusic.Theme1);
    }

    public void PlayMusic(EMusic nameSound)
    {
        Sound _sound = Array.Find(MusicSounds, x => x.nameSound == nameSound.ToString());
        MusicSource.clip = _sound.clip;
        MusicSource.Play();
    }

    public void PlaySFX(EMusic nameSound)
    {
        if (Time.time - lastTimePlaySFX < coolDownPlaySFX)
        {
            return;
        }

        Sound _sound = Array.Find(SFXSounds, x => x.nameSound == nameSound.ToString());
        SFXSource.clip = _sound.clip;
        SFXSource.Play();
    }

    public void PlayMagicBookSound(EMusic nameSound)
    {
        Sound _sound = Array.Find(SFXSounds, x => x.nameSound == nameSound.ToString());
        MagicBookSoundSource.clip = _sound.clip;
        MagicBookSoundSource.Play();
    }

    public void PlaySpawnDummySource(EMusic nameSound)
    {
        Sound _sound = Array.Find(SFXSounds, x => x.nameSound == nameSound.ToString());
        SpawnDummySource.clip = _sound.clip;
        SpawnDummySource.Play();
    }

    public void PlayFireBallSource(EMusic nameSound)
    {
        if (Time.time - lastTimeFireBall < coolDownFireBall)
        {
            return;
        }

        Sound _sound = Array.Find(SFXSounds, x => x.nameSound == nameSound.ToString());
        FireBallSource.clip = _sound.clip;
        FireBallSource.Play();
    }

    public void PlayLightningSource(EMusic nameSound)
    {
        if (Time.time - lastTimeLightningStrike < coolDownLightningStrike)
        {
            return;
        }

        Sound _sound = Array.Find(SFXSounds, x => x.nameSound == nameSound.ToString());
        LightningSource.clip = _sound.clip;
        LightningSource.Play();
    }
}
