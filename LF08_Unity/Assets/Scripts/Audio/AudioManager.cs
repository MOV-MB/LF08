using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager main;
    private void Awake() {
        if (main == null) {
            main = this;
        } else {
            Destroy(gameObject);
        }
    }
    #endregion
    
    public HashSet<Sound> sounds =
       new HashSet<Sound> ();

    public enum type{ // Not sure if we need this anymore...
        Music,
        Effects,
        Voice
    }

    public AudioMixerGroup music;
    public AudioMixerGroup effects;
    public AudioMixerGroup voice;

    public Sound currentMusic;
    public Sound currentVoiceLine;

    /// Creates a new sound, registers it, gives it the properties specified, and starts playing it
    public Sound PlayMusic(string soundName, bool loop=false, bool interrupts=false, Action<Sound> callback=null) {
        Sound sound = NewSound(soundName, loop, interrupts, callback);
        sound.playing = true;
        currentMusic = sound;
        sound.source.outputAudioMixerGroup = music;
        return sound;
    }

    public IEnumerator StartMusic(){
        PlayMusic("Intro");
        while (currentMusic.playing){
            yield return null;
        }
        PlayMusic("Loop", loop: true);
    }

    public Sound PlaySFX(string soundName, float pitch = 1f, bool loop=false, bool interrupts=false, Action<Sound> callback=null) {
        Sound sound = NewSound(soundName, loop, interrupts, callback);
        sound.playing = true;
        sound.source.outputAudioMixerGroup = effects;
        sound.source.pitch = pitch;
        return sound;
    }
    
    public Sound PlayVoice(string soundName, bool loop=false, bool interrupts=false, Action<Sound> callback=null) {
        Sound sound = NewSound(soundName, loop, interrupts, callback);
        sound.playing = true;
        sound.source.outputAudioMixerGroup = voice;
        return sound;
    }

    /// Creates a new sound, registers it, and gives it the properties specified
    public Sound NewSound(string soundName, bool loop=false, bool interrupts=false, Action<Sound> callback=null) {
        Sound sound = new Sound(soundName);
        RegisterSound(sound);
        sound.loop = loop;
        sound.interrupts = interrupts;
        sound.callback = callback;
        return sound;
    }

    /// Registers a sound with the AudioManager and gives it an AudioSource if necessary
    /// You should probably avoid calling this function directly and just use 
    /// NewSound and PlayNewSound instead
    public void RegisterSound(Sound sound) {
        sounds.Add(sound);
        sound.audioManager = this;
        if (sound.source == null) {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.clip;
            sound.source = source;
        }
    }

    private void Update() {
        sounds.ToList().ForEach(sound => {
            sound.Update();                 
        });
    }
}