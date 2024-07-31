using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelYukleme : MonoBehaviour
{
    public GameObject slider;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Elmas"))
        {
            PlayerPrefs.SetInt("Elmas", 0);
            PlayerPrefs.SetInt("Level", 1);
        }

        Invoke("SahneyiYukle", 4f);
        slider.GetComponent<Slider>().maxValue = 4f;
    }
    private void Update()
    {
        slider.GetComponent<Slider>().value = Time.time;
    }

    void SahneyiYukle()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }
    
    
}
