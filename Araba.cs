using UnityEngine;

public class Araba : MonoBehaviour
{
    public bool git;
    public GameObject parent;
    public GameManager manager;
    bool DuruyorMu;
    public ParticleSystem carpmaEfekt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!DuruyorMu)
        {
            transform.Translate(4f * Vector3.forward * Time.deltaTime);
        }
        else if (git)
        {
            transform.Translate(15f * Vector3.forward * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Duvar"))
        {
            
            git = false;
            manager.dogruParkEdilenArabaSayisi++;
            transform.SetParent(parent.transform);
            manager.DurmaNoktasi.SetActive(true);
         
        }
        else if (collision.gameObject.CompareTag("DurmaNoktasi"))
        {
            DuruyorMu = true;
            manager.DurmaNoktasi.SetActive(false);
        }

        else if (collision.gameObject.CompareTag("OrtaSilindir"))
        {
            manager.sesler[1].Play();
            carpmaEfekt.transform.position = gameObject.transform.position;
            carpmaEfekt.Play();
            manager.DurmaNoktasi.SetActive(true);
            gameObject.SetActive(false);
            
        }
        else if (collision.gameObject.CompareTag("Araba"))
        {
            git = false;
            manager.arabaCarpmaOlduMu = true;
            manager.DurmaNoktasi.SetActive(true);
            manager.sesler[1].Play();
            carpmaEfekt.transform.position = gameObject.transform.position;
            carpmaEfekt.Play();
            gameObject.SetActive(false);
            
        }
        else if (collision.gameObject.CompareTag("Elmas"))
        {
            manager.elmasSayisi++;
            collision.gameObject.SetActive(false);
            manager.sesler[0].Play();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KaybetmeZemini"))
        {
            manager.dogruParkEdilenArabaSayisi--;
            gameObject.SetActive(false);
        }
    }
}
