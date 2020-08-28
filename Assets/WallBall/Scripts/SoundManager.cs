using UnityEngine;
using System.Collections;

public class SoundManager : ManagerBase {

	// to add a new sound effecr,
	// just add a new AudioClip variable here
	public AudioClip shatterSound;
	public AudioClip gemCollectSound;
	public AudioClip fallingSound;
	public AudioSource audioSource;
	public AudioSource playerAudioSource;

	// to add a new music ,
	// just add a new AudioClip variable here
	public AudioClip gameMusic;
	public AudioClip menuMusic;
	public AudioClip gameOverMusic;
	public AudioSource musicSource;

	// thsi is the music played when the script awakes
	public void Start() {
		PlayMenuMusic();
	}

	// this method may be called from the outside 
	// to start the menu music
	public void PlayMenuMusic() {
		PlayMusic (menuMusic, true, 0.175f);
	}

	// this method may be called from the outside 
	// to start the game music
	public void PlayMusicGame() {
		PlayMusic (gameMusic, true, 0.15f);
	}


	// this method may be called from the outside 
	// to start the game over music
	public void PlayGameOverMusic() {
		PlayMusic (gameOverMusic, false, 0.3f);
	}

	// this is the master method which plays the selected audio 
	// via the corresponding AudioSource
	private void PlayMusic(AudioClip a, bool isLooping, float volume)
	{
		if (musicSource != null && musicSource.clip != null) {
			musicSource.Stop ();
		}
			
		musicSource.clip = a;
		musicSource.loop = isLooping;
		musicSource.volume = volume;
		musicSource.Play ();
	}

	
	// this method may be called from the outside 
	// to play the ball falling sound once
	public void playFallingSound() {
		playPlayerSound (fallingSound,0.005f);
	}

	// this method may be called from the outside 
	// to play the shatter sound once
	public void playShatterSound() {
		playSound (shatterSound,0.1f);
	}

	// this method may be called from the outside 
	// to play the gem collect sound once
	public void playGemSound() {
		playSound (gemCollectSound,0.1f);
	}

	// this is the master method which plays the selected sound effect 
	// via the corresponding AudioSource
	public void playSound(AudioClip audioClip, float volume) {
		if (audioSource != null && audioSource != null) {
			audioSource.Stop();
		}
		// play the effect once
		audioSource.volume = volume;
		audioSource.PlayOneShot(audioClip);
	}

	public void playPlayerSound(AudioClip audioClip, float volume) {
		if (playerAudioSource != null && playerAudioSource != null) {
			audioSource.Stop();
		}
		// play the effect once
		playerAudioSource.volume = volume;
		playerAudioSource.PlayOneShot(audioClip);
	}
}
