using System.Collections;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    bool temp;
    bool kazanma = true;
    public bool arabaCarpmaOlduMu;
    [Header("^^^^Araba Ayarları^^^^")]
    public GameObject[] arabalar;
    public int arabaIndex;
    public int kacArabaOlsun;
    public int dogruParkEdilenArabaSayisi;
    public int hedefParkSayisi;

    [Header("^^^^Platform Ayarları^^^^")]
    public GameObject platform1;
    public GameObject DurmaNoktasi;
    public float[] donusHizlari;

    [Header("^^^^Canvas Ayarları^^^^")]
    public Sprite parkEdildi;
    public TextMeshProUGUI[] textler;
    public GameObject[] paneller;
    public ParticleSystem efekt;

    [Header("^^^^Level Ayarları^^^^")]
    public int elmasSayisi;

    [Header("^^^^Ses Ayarları^^^^")]
    public AudioSource[] sesler;

    void Start()
    {
        
        VarsayilanDegerleriKontrolEt();
        
    }
    void tempDegis()
    {
        temp = !temp;
    }

    public void YeniArabaGetirButonu()
    {
        if (temp)
        {
            YeniArabaGetir();
            Invoke("tempDegis", 0.5f);
            if (arabaCarpmaOlduMu)
            {
                dogruParkEdilenArabaSayisi--;
                arabaCarpmaOlduMu = !arabaCarpmaOlduMu;
            }


        }
    }

    public void GazaBasButonu()
    {
        if (!temp)
        {
            arabalar[arabaIndex].GetComponent<Araba>().git = true;
            arabaIndex++;
            Invoke("tempDegis", 0.5f);

        }
    }

    public void OyunaBaslaButonu()
    {
        paneller[0].SetActive(false);
        paneller[3].SetActive(true);
    }
    void Update()
    {
      
        
        textler[8].text = (kacArabaOlsun - arabaIndex).ToString();
        textler[9].text = hedefParkSayisi.ToString();
        if (dogruParkEdilenArabaSayisi == hedefParkSayisi)
        {
            if (kazanma)
            {
                Invoke("KazandinMi", 1.5f);

            }
        }
        if (paneller[0].activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
       
       
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            platform1.transform.Rotate(new Vector3(0, 0, donusHizlari[0]) * Time.deltaTime, Space.Self);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            platform1.transform.Rotate(new Vector3(0, 0, donusHizlari[0] * 1f) * Time.deltaTime, Space.Self);

        }

    }

    public void YeniArabaGetir(bool a = false)
    {
        if(arabaIndex < kacArabaOlsun)
        {
            arabalar[arabaIndex].SetActive(true);
        }
        if (a)
        {
            dogruParkEdilenArabaSayisi--;
        }
        if (hedefParkSayisi-dogruParkEdilenArabaSayisi > kacArabaOlsun-arabaIndex)
        {
            
            Kaybettin();
        }
    }

    void VarsayilanDegerleriKontrolEt()
    {
        if (!PlayerPrefs.HasKey("Elmas"))
        {
            PlayerPrefs.SetInt("Elmas", 0);
            PlayerPrefs.SetInt("Level", 1);
        }
        textler[0].text = PlayerPrefs.GetInt("Elmas").ToString();
        textler[1].text = SceneManager.GetActiveScene().name; 
    }

    public void TekrarOyna()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void BirSonrakiLevelGit()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex); // En son kalınan level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Kaybettin()
    {
        
        PlayerPrefs.SetInt("Elmas", PlayerPrefs.GetInt("Elmas") + elmasSayisi);
        textler[5].text = PlayerPrefs.GetInt("Elmas").ToString();
        textler[6].text = SceneManager.GetActiveScene().name;
        textler[7].text = elmasSayisi.ToString();
        paneller[2].SetActive(true);
        paneller[3].SetActive(false);

    }

    void Kazandin()
    {
        
        PlayerPrefs.SetInt("Elmas", PlayerPrefs.GetInt("Elmas") + elmasSayisi);
        textler[2].text = PlayerPrefs.GetInt("Elmas").ToString();
        textler[3].text = SceneManager.GetActiveScene().name;
        textler[4].text = elmasSayisi.ToString();
        paneller[1].SetActive(true);
        paneller[3].SetActive(false);
        dogruParkEdilenArabaSayisi--;
    }

    void KazandinMi()
    {
        
        if (dogruParkEdilenArabaSayisi == hedefParkSayisi)
        {
            

            Kazandin();
        }
    }
    public void BastanBasla()
    {
        SceneManager.LoadScene(1);
    }
}
