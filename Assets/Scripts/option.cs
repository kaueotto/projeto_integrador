using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class option : MonoBehaviour {

	private SoundController soundController;
	public GameObject painel1, painel2;
	public Toggle onOffMusica, onOffEfetios;
	public Slider volumeM, volumeE;

	void Start()
	{
		soundController = FindObjectOfType (typeof(SoundController)) as SoundController;
		carregarPreferencias ();
		painel1.SetActive (true);
		painel2.SetActive (false);
	}
	
	//---------------------------------------------------------------------------------
	public void configuracoes(bool onOff)
	{
		soundController.playButton ();
		painel1.SetActive(!onOff);
		painel2.SetActive(onOff);
	}

	//---------------------------------------------------------------------------------
	public void zerarProgresso()
	{
		soundController.playButton();

		int onOffM = PlayerPrefs.GetInt ("onOffMusica"); 
		int onOffE = PlayerPrefs.GetInt ("onOffEfetios");
		float volumeMusica = PlayerPrefs.GetFloat ("volumeMusica");
		float volumeEfeitos = PlayerPrefs.GetFloat ("volumeEfeitos");

		PlayerPrefs.DeleteAll ();

		PlayerPrefs.SetInt ("valoresDefault", 1);
		PlayerPrefs.SetInt ("onOffMusica", onOffM);
		PlayerPrefs.SetInt ("onOffEfetios", onOffE);
		PlayerPrefs.SetFloat ("volumeMusica", volumeMusica);
		PlayerPrefs.SetFloat ("volumeEfeitos", volumeEfeitos);
	
	}

	//---------------------------------------------------------------------------------
	public void mutarMusica()
	{
		soundController.audioMusic.mute = !onOffMusica.isOn;
		if (onOffMusica == true) {
			PlayerPrefs.SetInt ("onOffMusica", 1);
		} else {
			PlayerPrefs.SetInt ("onOffMusica", 0);
		}
	}

	//---------------------------------------------------------------------------------
	public void mutarEfeitos()
	{
		soundController.audioFX.mute = !onOffEfetios.isOn;
		if (onOffEfetios == true) {
			PlayerPrefs.SetInt ("onOffEfetios", 1);
		} else {
			PlayerPrefs.SetInt ("onOffEfetios", 0);
		}
	}

	//---------------------------------------------------------------------------------
	public void volumeMusica()
	{
		soundController.audioMusic.volume = volumeM.value;
		PlayerPrefs.SetFloat ("volumeMusica", volumeM.value);
	}

	//---------------------------------------------------------------------------------
	public void volumeEfeitos()
	{
		soundController.audioFX.volume = volumeE.value;
		PlayerPrefs.SetFloat ("volumeEfeitos", volumeE.value);
	}

	//---------------------------------------------------------------------------------
	void carregarPreferencias()
	{

		int onOffM = PlayerPrefs.GetInt ("onOffMusica"); // carrega os valores de configuração de volume e musica
		int onOffE = PlayerPrefs.GetInt ("onOffEfetios");
		float volumeMusica = PlayerPrefs.GetFloat ("volumeMusica");
		float volumeEfeitos = PlayerPrefs.GetFloat ("volumeEfeitos");

		bool tocarMusica = false;
		bool tocarEfeitos = false;

		if (onOffM == 1) {
			tocarMusica = true;
		}

		if (onOffE == 1) {
			tocarEfeitos = true;
		}

		onOffMusica.isOn = tocarMusica;
		onOffEfetios.isOn = tocarEfeitos;

		volumeM.value = volumeMusica;
		volumeE.value = volumeEfeitos;
	}
}
