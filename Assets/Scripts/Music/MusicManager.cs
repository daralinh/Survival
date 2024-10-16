using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : Singleton<MusicManager>
{
    public AudioMixerGroup sfxMixerGroup;
    public AudioMixerGroup backgroundMixerGroup;
    public Sound[] MusicSounds, SFXSounds;
    public AudioSource MusicSource, SFXSource, MagicBookSoundSource, SpawnDummySource, ChestSource, PlayerWeaponSource;
    public AudioSource AudioSourcePrefab;
    [SerializeField] private int maxQuantity;

    public float coolDownPlaySFX;
    public float coolDownPlayChestSource;
    public float coolDownPlayerWeaponSource;

    private float lastTimePlaySFX = 0;
    private float lastTimePlayChestSource = 0;
    private float lastTimePlayerWeaponSource = 0;
    private Queue<AudioSource> BulletAudioSourceQueue = new Queue<AudioSource>();

    protected override void Awake()
    {
        base.Awake();
        SFXSource.outputAudioMixerGroup = sfxMixerGroup;
        SpawnDummySource.outputAudioMixerGroup = sfxMixerGroup;
        MusicSource.outputAudioMixerGroup = backgroundMixerGroup;
        SpawnDummySource.outputAudioMixerGroup = sfxMixerGroup;
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

        lastTimePlaySFX = Time.time;
        Sound _sound = Array.Find(SFXSounds, x => x.nameSound == nameSound.ToString());
        SFXSource.clip = _sound.clip;
        SFXSource.Play();
    }

    public void PlayPlayerWeapon(EMusic nameSound)
    {
        if (Time.time - lastTimePlayerWeaponSource < coolDownPlayerWeaponSource)
        {
            return;
        }

        lastTimePlayerWeaponSource = Time.time;
        Sound _sound = Array.Find(SFXSounds, x => x.nameSound == nameSound.ToString());
        PlayerWeaponSource.clip = _sound.clip;
        PlayerWeaponSource.Play();
    }

    public void PlayMagicBookSound(EMusic nameSound)
    {
        Sound _sound = Array.Find(SFXSounds, x => x.nameSound == nameSound.ToString());
        MagicBookSoundSource.clip = _sound.clip;
        MagicBookSoundSource.Play();
    }

    public void PlayChestSource(EMusic nameSound)
    {
        if (Time.time - lastTimePlayChestSource < coolDownPlayChestSource)
        {
            return;
        }

        lastTimePlayChestSource = Time.time;
        Sound _sound = Array.Find(SFXSounds, x => x.nameSound == nameSound.ToString());
        ChestSource.clip = _sound.clip;
        ChestSource.Play();
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
        if (maxQuantity < 1)
        {
            return;
        }

        if (BulletAudioSourceQueue.Count == 0)
        {
            maxQuantity--;
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
        maxQuantity++;
    }
}
