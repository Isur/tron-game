using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    public GameObject mainMenu;
    public Slider slider;
    public Text sliderText;
    int Players;

    void Start() {
        slider.value = PlayerPrefs.GetInt("numOfPlayers", 2);
        sliderText.text = $"Players: {slider.value}";
    }
    public void playGame() {
        SceneManager.LoadScene("game");
    }

    public void exitGame() {
        Application.Quit();
    }

    public void onSliderChange() {
        PlayerPrefs.SetInt("numOfPlayers", (int)(slider.value));
        sliderText.text = $"Players: {slider.value}";
    }

    public void Plus() {
        slider.value += 1;
    }
    public void Minus() {
        slider.value -= 1;
    }
}