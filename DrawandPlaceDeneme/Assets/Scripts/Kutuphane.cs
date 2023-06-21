using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OlcayKalyoncuoglu
{
    public class GenelYonetim
    {
        public static GameManager _GameManager;

        public GenelYonetim(GameManager _gameManager)
        {
           _GameManager = _gameManager;
        }
    }

    // OTOMATİK LEVEL
    [Serializable]

    public class OtomatikLevel
    {

        public List<BaslangicObjeleri> _BaslangicObjeleri;
        public List<Soketler> _Soketler;
        public List<LineRenderer> _LineRenderer;

    }


    [Serializable]

    public class BaslangicObjeleri
    {
        public GameObject _BaslangicObjesi;
        public GameObject _SoketBaslangicPozisyonu;
    }

    [Serializable]

    public class Soketler
    {
        public Color _SoketRengi;
        public SpriteRenderer _SpriteRenderer;



        [Header("----SOKET SCRİPT İSLEMLERİ")]
        public Soket _Soket;
        public string _SoketRenk;
        public SpriteRenderer _VarisYuvasiSpriteRenderer;
        public GameObject _VarisYuvasiMerkezi;
    }

    // BELLEK YONETİMİ

    public static class BellekYonetimi
    {
        public static void VeriKaydetInt(string Key, int Value)
        {
            PlayerPrefs.SetInt(Key, Value);
        }

        public static int VeriOkuInt(string Key)
        {
            return PlayerPrefs.GetInt(Key);
        }

        public static void VeriKaydetString(string Key, string Value)
        {
            PlayerPrefs.SetString(Key, Value);
        }

        public static string VeriOkuString(string Key)
        {
            return PlayerPrefs.GetString(Key);
        }

        public static bool AnahtarVarmi(string Key)
        {
            return PlayerPrefs.HasKey(Key);
        }
    }
}
