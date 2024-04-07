using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopHareket : MonoBehaviour
{
    public UnityEngine.UI.Button btn;
    public UnityEngine.UI.Text zaman, can, durum;
    private Rigidbody rg;
    public float Hiz = 1.5f;
    float zamanSayaci = 15;
    int canSayaci = 3;
    bool oyunDevam = true;
    bool oyunTamam = false;

    // Start is called before the first frame update
    void Start()
    {
        can.text = canSayaci + "";
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oyunDevam && !oyunTamam) {
            zamanSayaci -= Time.deltaTime;
            zaman.text = (int)zamanSayaci + "";
        } else if(!oyunTamam)
        {
            durum.text = "Oyun Tamamlanamadı";
            btn.gameObject.SetActive(true);
        }
        if (zamanSayaci < 0)
            oyunDevam = false;
       
    }

    void FixedUpdate()
    {
        if (oyunDevam && !oyunTamam) {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(-dikey, 0, yatay);
            rg.AddForce(kuvvet * Hiz);
        }
        else
        {
            rg.velocity = Vector3.zero; //hareket etmesini, hızını sıfırlıyoruz
            rg.angularVelocity = Vector3.zero; //dönmesini, döngüsel hızını sıfırlıyoruz
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        string objName = collision.gameObject.name;
        if (objName.Equals("Bitis")){
            //print("Oyun tamamlandı.");
            oyunTamam = true;
            durum.text = "Oyun Tamamlandı, Tebrikler.";
            btn.gameObject.SetActive(true);
        }
        else if(!objName.Equals("LabirentZemini") && !objName.Equals("Zemin"))
        {
            canSayaci -= 1;
            can.text = canSayaci + "";
            if(canSayaci == 0)
                oyunDevam = false;
        }
    }

}

