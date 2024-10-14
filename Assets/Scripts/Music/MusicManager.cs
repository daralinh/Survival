using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public Sound[] MusicSounds, SFXSounds;
    public AudioSource MusicSource, SFXSource, MagicBookSoundSource, SpawnDummySource;
    public AudioSource AudioSourcePrefab;

    public float coolDownPlaySFX;

    private float lastTimePlaySFX;
    private Queue<AudioSource> BulletAudioSourceQueue = new Queue<AudioSource>();

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
        SpawnDummySource.loop = true;
        SpawnDummySource.clip = _sound.clip;
        SpawnDummySource.Play();
    }

    public void PlayBulletSFX(EMusic nameSound)
    {
        if (BulletAudioSourceQueue.Count == 0)
        {
            BulletAudioSourceQueue.Enqueue(Instantiate(AudioSourcePrefab, transform.position ,Quaternion.identity));
        }

        AudioSource _audioSource = BulletAudioSourceQueue.Dequeue();
        _audioSource.clip = Array.Find(SFXSounds, x => x.nameSound == nameSound.ToString()).clip;
        _audioSource.Play();
        StartCoroutine(ReturnToPool(_audioSource));
    }

    private IEnumerator ReturnToPool(AudioSource _audioSource)
    {
        yield return new WaitForSeconds(_audioSource.clip.length);
        _audioSource.Stop();
        _audioSource.clip = null;
        BulletAudioSourceQueue.Enqueue(_audioSource);
    }
}
