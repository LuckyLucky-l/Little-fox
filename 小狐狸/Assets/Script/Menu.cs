using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer audioMixer;
    public Slider slider;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
 
    public void QuitGame()
    {
        Application.Quit();
    }
    public void UIEnable()
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;//����ʱ���Ϊ0
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;//���������ʱ��������
    }
    /*public void SetVolume(float value)//����slider���Ҳ���SetVolume(),ֻ��SetVolume(float)ʱ�����溯��
    {
        audioMixer.SetFloat("MainVolume", value);
    }*/
    public void SetVolume()
    {
        audioMixer.SetFloat("MainVolume", slider.value);
    }
}
