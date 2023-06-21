using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OlcayKalyoncuoglu;
public class Soket : MonoBehaviour
{
    public bool _Yerlestir;
    public int _CizgiIndex;
    public string _SoketRenk;
    public GameObject _VarisYuvasi;

    bool _YuvayaOtur;
    Vector2 _YuvaPozisyonu;
    
    void Update()
    {
        if (_Yerlestir)
        {
            if (Vector2.Distance(transform.position, GenelYonetim._GameManager._CizgiCiz[_CizgiIndex].SonPozisyonuVer()) > .1f)
                transform.position = Vector2.Lerp(transform.position, GenelYonetim._GameManager._CizgiCiz[_CizgiIndex].SonPozisyonuVer(),30*Time.deltaTime);
            else
                transform.position = Vector2.Lerp(transform.position, GenelYonetim._GameManager._CizgiCiz[_CizgiIndex].SonrakiPozisyonuVer(), 30 * Time.deltaTime);
        }

        if (_YuvayaOtur)
        {
            if (Vector2.Distance(transform.position, _YuvaPozisyonu) > .1f)
                transform.position = Vector2.Lerp(transform.position, _YuvaPozisyonu, 30 * Time.deltaTime);
            else
            {
                transform.position = _YuvaPozisyonu;
                _YuvayaOtur = false;
            }
               
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_SoketRenk))
        {
            //Debug.Log("Doğru Renge Geldi" + _SoketRenk);
            _YuvayaOtur = true;
            _YuvaPozisyonu = _VarisYuvasi.transform.position;
            GenelYonetim._GameManager.SoketOturdu();
           // GetComponent<CircleCollider2D>().enabled = false;
        }
        else if (collision.CompareTag("Soket"))
        {
            _Yerlestir = false;
            GenelYonetim._GameManager.Kaybettin();
            GenelYonetim._GameManager.SesCal(3);
        }

        else
        {
            if (!collision.CompareTag(GenelYonetim._GameManager._CizgiCiz[_CizgiIndex]._Tag))
            {
                // Debug.Log("Game Over " + collision.gameObject.name);
                _Yerlestir = false;
                GenelYonetim._GameManager.Kaybettin();
                GenelYonetim._GameManager.SesCal(3);
            }


                
        }
    }
}
