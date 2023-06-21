using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OlcayKalyoncuoglu;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;




public class GameManager : MonoBehaviour
{
    [Header("----GENEL OBJELER")]
    public List<CizgiCiz> _CizgiCiz;
    [SerializeField] int _ToplamObjeSayisi;
    [SerializeField] GameObject[] _Paneller;
    [SerializeField] AudioSource[] _Sesler;
    [SerializeField] Image[] _ButtonGorselleri;
    [SerializeField] Sprite[] _SpriteObjeleri;
    [SerializeField] TextMeshProUGUI _PuanText;

    [Header("----OTOMATİK LEVEL")]
    [SerializeField] List<OtomatikLevel> _OtomatikLevel;
    


    bool ZamanBasladimi;
    int _ToplamGirmesiSoketSayisi;
    GenelYonetim _GenelYonetim;

    int _SceneIndex;
    AudioSource _OyunSesi;

    private void Awake()
    {
        _GenelYonetim = new(this);
        SahneIlkIslemler();
      
    }
    // Start is called before the first frame update
    void Start()
    {
       
        for (int i=0; i < _OtomatikLevel[0]._BaslangicObjeleri.Count; i++)
        {
            _OtomatikLevel[0]._BaslangicObjeleri[i]._BaslangicObjesi.tag = "Baslangic" + (i + 1);

            _OtomatikLevel[0]._Soketler[i]._Soket.transform.position = _OtomatikLevel[0]._BaslangicObjeleri[i]._SoketBaslangicPozisyonu.transform.position;

            _OtomatikLevel[0]._Soketler[i]._SpriteRenderer.color = _OtomatikLevel[0]._Soketler[i]._SoketRengi;

            _OtomatikLevel[0]._Soketler[i]._Soket._CizgiIndex = i;

            _OtomatikLevel[0]._Soketler[i]._Soket._SoketRenk = _OtomatikLevel[0]._Soketler[i]._SoketRenk;

            //varış noktası işlemleri

            _OtomatikLevel[0]._Soketler[i]._VarisYuvasiSpriteRenderer.gameObject.tag = _OtomatikLevel[0]._Soketler[i]._SoketRenk;

            _OtomatikLevel[0]._Soketler[i]._VarisYuvasiSpriteRenderer.color = _OtomatikLevel[0]._Soketler[i]._SoketRengi;

            _OtomatikLevel[0]._Soketler[i]._Soket._VarisYuvasi = _OtomatikLevel[0]._Soketler[i]._VarisYuvasiMerkezi;

            _OtomatikLevel[0]._LineRenderer[i].startColor = _OtomatikLevel[0]._Soketler[i]._SoketRengi;
            _OtomatikLevel[0]._LineRenderer[i].endColor = _OtomatikLevel[0]._Soketler[i]._SoketRengi;

            CizgiCiz _Cz = gameObject.AddComponent<CizgiCiz>();
            _Cz._LineRenderer = _OtomatikLevel[0]._LineRenderer[i];
            _Cz._Soket = _OtomatikLevel[0]._Soketler[i]._Soket;
            _Cz._Tag = "Baslangic" + (i + 1);

            _CizgiCiz.Add(_Cz);

        }
    }

    // Update is called once per frame

    void PanelAc(int Index)
    {
        _Paneller[Index].SetActive(true);
       
    }

    void PanelKapat(int Index)
    {
        _Paneller[Index].SetActive(false);
    }

   public void SesCal(int Index)
    {
        _Sesler[Index].Play();
    }


    void Kazandin()
    {
        
        PanelAc(1);
        SesCal(2);
        BellekYonetimi.VeriKaydetInt("Level", _SceneIndex + 1);
        BellekYonetimi.VeriKaydetInt("Puan", BellekYonetimi.VeriOkuInt("Puan") + 50);
        _PuanText.text = BellekYonetimi.VeriOkuInt("Puan").ToString();
        Time.timeScale = 0;
    }

    public void Kaybettin()
    {
        
        PanelAc(2);
        SesCal(1);
        Time.timeScale = 0;
    }

    public void ButonlarinTeknikIslemleri(string Islem)
    {
        switch (Islem)
        {
            case "Durdur":
                SesCal(0);
                PanelAc(0);
                Time.timeScale = 0;
                break;

            case "DevamEt":
                SesCal(0);
                PanelKapat(0);
                Time.timeScale = 1;
                break;

            case "Tekrar":
                SesCal(0);
                SceneManager.LoadScene(_SceneIndex);
                Time.timeScale = 1;
                break;

            case "SonrakiLevel":
                SesCal(0);
                SceneManager.LoadScene(_SceneIndex+1);
                Time.timeScale = 1;
                break;

            case "Cikis":
                SesCal(0);
                PanelAc(3);
                break;

            case "Yes":
                SesCal(0);
                Application.Quit();
                break;

            case "No":
                SesCal(0);
                PanelKapat(3);
                break;

            case "OyunSesiAyar":
                SesCal(0);
                if(BellekYonetimi.VeriOkuInt("OyunSesi") == 0)
                {
                    BellekYonetimi.VeriKaydetInt("OyunSesi", 1);
                    _ButtonGorselleri[0].sprite = _SpriteObjeleri[0];
                    _OyunSesi.mute = false;
                }
                else
                {
                    BellekYonetimi.VeriKaydetInt("OyunSesi", 0);
                    _ButtonGorselleri[0].sprite = _SpriteObjeleri[1];
                    _OyunSesi.mute = true;
                }
                break;


            case "EfektSesiAyar":
                SesCal(0);
                if (BellekYonetimi.VeriOkuInt("EfektSesi") == 0)
                {
                    BellekYonetimi.VeriKaydetInt("EfektSesi", 1);
                    _ButtonGorselleri[1].sprite = _SpriteObjeleri[2];
                    foreach (var item in _Sesler)
                    {
                        item.mute = false;
                    }
                }
                else
                {
                    BellekYonetimi.VeriKaydetInt("EfektSesi", 0);
                    _ButtonGorselleri[1].sprite = _SpriteObjeleri[3];
                    foreach (var item in _Sesler)
                    {
                        item.mute = true;
                    }
                }
                break;

        }
      
    }

    void Basla()
    {
        foreach (var item in _CizgiCiz)
        {
            item.Basla();
        }

        Invoke("SoketleriKontrolEt", 5f);
    }
    public void CizgiBitti()
    {
        _ToplamObjeSayisi--;
        if(_ToplamObjeSayisi == 0)
        {
            Basla();
        }
    }

    public void SoketOturdu()
    {
        SesCal(4);
        _ToplamGirmesiSoketSayisi--;
        if (!ZamanBasladimi)
        {
            Invoke("SoketleriKontrolEt", 2f);
            ZamanBasladimi = true;
        }

        if (_ToplamGirmesiSoketSayisi == 0)
            Kazandin();
    }

    void SoketleriKontrolEt()
    {
        if(_ToplamGirmesiSoketSayisi != 0)
        {
            Kaybettin();
        }
    }

    void SahneIlkIslemler()
    {
        _ToplamGirmesiSoketSayisi = _ToplamObjeSayisi;
        _PuanText.text = BellekYonetimi.VeriOkuInt("Puan").ToString();
        _OyunSesi = GameObject.FindWithTag("OyunSesi").GetComponent<AudioSource>();
        _SceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (BellekYonetimi.VeriOkuInt("OyunSesi") == 0)
        {
            
            _ButtonGorselleri[0].sprite = _SpriteObjeleri[1];
            _OyunSesi.mute = true;
        }
        else
        {
            
            _ButtonGorselleri[0].sprite = _SpriteObjeleri[0];
            _OyunSesi.mute = false;
        }
      
        if (BellekYonetimi.VeriOkuInt("EfektSesi") == 0)
        {
           
            _ButtonGorselleri[1].sprite = _SpriteObjeleri[3];
            foreach (var item in _Sesler)
            {
                item.mute = true;
            }
        }
        else
        {
            
            _ButtonGorselleri[1].sprite = _SpriteObjeleri[2];
            foreach (var item in _Sesler)
            {
                item.mute = false;
            }
        }
    }
}
