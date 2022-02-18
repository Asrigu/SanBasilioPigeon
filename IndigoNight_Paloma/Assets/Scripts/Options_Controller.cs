using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_Controller : MonoBehaviour
{
    // Variables
    public Slider brightnessSlider;
    public float brightnessSliderValue;
    public Image brightnessPanel;

    public Slider volumeSlider;
    public float volumeSliderValue;
    public Image muteImage;
    public Image unmuteImage;

    public Toggle fullscreenToggle;

    // Start is called before the first frame update
    void Start()
    {
        // L�gica para poner valores predeterminados del brillo de la pantalla
        #region DEFAULT BRIGHTNESS
        
        // Crear valor al iniciar el juego
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 0.5f);

        // Color del panel del alfa igual al del valor del alpha (slider value)
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, brightnessSliderValue);

        #endregion

        // L�gica para poner valores predeterminados del volumen
        #region DEFAULT VOLUME

        // Crear valor al iniciar el juego
        volumeSlider.value = PlayerPrefs.GetFloat("audioVolume", 0.5f);

        // Volumen del juego de 0 a 1 (muted or max volume)
        AudioListener.volume = volumeSlider.value;

        // Revisar muteado
        AmIMuted();

        #endregion

        // L�gica para poner valores predeterminados de la pantalla completa y de la resoluci�n de pantalla
        #region DEFAULT FULLSCREEN & SCREEN RESOLUTION

        // Detecci�n de modo ventana o modo pantalla completa cuando entramos al men� del juego
        if (Screen.fullScreen)
        {
            fullscreenToggle.isOn = true;
        }

        else
        {
            fullscreenToggle.isOn = false;
        }
        #endregion
    }

    // L�gica para cambiar y guardar valores del brillo de la pantalla
    #region Brightness Logic

    public void ChangeBrightnessSlider(float value)
    {
        brightnessSliderValue = value;

        // Guardado del valor al mover el slider para cuando se cierren las options o el juego
        PlayerPrefs.SetFloat("Brightness", brightnessSliderValue);

        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, brightnessSliderValue);
    }

    #endregion

    // L�gica para cambiar y guardar valores del volumen
    #region Volume Logic

    public void ChangeVolumeSlider(float value)
    {
        volumeSliderValue = value;

        // Guardado del valor al mover el slider para cuando se cierren las options o el juego
        PlayerPrefs.SetFloat("audioVolume", volumeSliderValue);
        AudioListener.volume = volumeSlider.value;

        // Revisar muteado
        AmIMuted();
    }

    public void AmIMuted()
    {
        // Si est� muteado, mostrar imagen de muteado
        if (volumeSliderValue == 0)
        {
            muteImage.enabled = true;
            unmuteImage.enabled = false;
        }

        // Si no est� muteado, no mostrar imagen de muteado
        else
        {
            muteImage.enabled = false;
            unmuteImage.enabled = true;
        }
    }

    #endregion

    // L�gica para cambiar y guardar valores de la pantalla completa
    #region Fullscreen Logic

    public void ActivateFullscreenMode(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
    #endregion

}
