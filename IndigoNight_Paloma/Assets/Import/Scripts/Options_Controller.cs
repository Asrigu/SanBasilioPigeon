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
    public Dropdown screenResolutionsDropdown;
    Resolution[] screenResolutions;

    public Dropdown qualityImageDropdown;
    public int qualityImage;

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

        // Revisar la resoluci�n de la pantalla
        CheckScreenResolution();

        #endregion

        // L�gica para poner valores predeterminados de la calidad de imagen
        #region DEFAULT IMAGE QUALITY

        qualityImage = PlayerPrefs.GetInt("qualityImage", 3);
        qualityImageDropdown.value = qualityImage;
        AdjustQualityImage();

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

    // L�gica para cambiar y guardar valores de la pantalla completa y de la resoluci�n de pantalla
    #region Fullscreen & Screen Resolution Logic

    public void ActivateFullscreenMode(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    // M�todo para revisar la resoluci�n de pantalla
    public void CheckScreenResolution()
    {
        // Guardado de todas las resoluciones de cada ordenador
        screenResolutions = Screen.resolutions;

        // Borrar las opciones predeterminadas del dropdown
        screenResolutionsDropdown.ClearOptions();

        // Lista de strings para guardar el tama�o de la resoluci�n
        List<string> options = new List<string>();

        // Variable para iniciar de 0
        int actualResolution = 0;

        // Mostrar y guardar la resoluci�n actual
        for (int i = 0; i < screenResolutions.Length; i++)
        {
            // Mostrar las resoluciones en la barra de opciones del dropdown (Ex: 1920 x 1080)
            string option = screenResolutions[i].width + " x " + screenResolutions[i].height;
            options.Add(option);

            // Revisado de la opci�n guardad para guardar la resoluci�n actual de la pantalla
            if (Screen.fullScreen && screenResolutions[i].width == Screen.currentResolution.width
                && screenResolutions[i].height == Screen.currentResolution.height)
            {
                actualResolution = i;
            }
        }

        // Agregado de opciones guardadas en la lista
        screenResolutionsDropdown.AddOptions(options);

        // Detecci�n de la resoluci�n en la que estamos
        screenResolutionsDropdown.value = actualResolution;

        // Actualizado de la lista
        screenResolutionsDropdown.RefreshShownValue();

        // Valor predeterminado para el primer inicio del juego
        screenResolutionsDropdown.value = PlayerPrefs.GetInt("screenResolutionNum", 0);
    }

    // M�todo para cambiar la resoluci�n en el dropdown
    public void ChangeScreenResolution(int screenResolutionIndex)
    { 
        // Cambiado de valor y guardado de este mismo y mostrado en la pantalla una vez cerrado el juego
        PlayerPrefs.SetInt("screenResolutionNum", screenResolutionsDropdown.value);

        // Creado moment�neo de un valor de resoluci�n
        Resolution resolution = screenResolutions[screenResolutionIndex];

        // Cambiar la resoluci�n solamente en pantalla completa
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    #endregion

    // L�gica para cambiar y guardar valores de la calidad de imagen
    #region Quality Image Logic

    public void AdjustQualityImage()
    {
        QualitySettings.SetQualityLevel(qualityImageDropdown.value);
        PlayerPrefs.SetInt("qualityImage", qualityImageDropdown.value);
        qualityImage = qualityImageDropdown.value;
    }

    #endregion
}
