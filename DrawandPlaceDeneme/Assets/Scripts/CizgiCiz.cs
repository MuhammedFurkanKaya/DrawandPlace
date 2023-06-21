using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OlcayKalyoncuoglu;

public class CizgiCiz : MonoBehaviour
{
    public LineRenderer _LineRenderer;
    public Soket _Soket;
    public string _Tag;
    public List<Vector2> ParmakPozisyonListesi = new List<Vector2>();

    int _ParmakPozisyonIndex;
    bool CizmeBasladimi;
    Camera _Kamera;

    RaycastHit2D hit;
    void Start()
    {
        
        _Kamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0) && !CizmeBasladimi)
        {
            CizgiOlustur();
            CizmeBasladimi = true;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 ParmakPozisyonu = _Kamera.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(ParmakPozisyonu, ParmakPozisyonListesi[^1]) > .1f)
            {
                CizgiyiGuncelle(ParmakPozisyonu);
            }
        }

        if(Input.GetMouseButtonUp(0) && CizmeBasladimi)
        {
            this.enabled = false;
        }*/

        hit = Physics2D.Raycast(_Kamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10)),Vector2.zero);

        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag(_Tag) && !CizmeBasladimi && Input.GetMouseButtonDown(0))
            {
                CizgiOlustur();
                CizmeBasladimi = true;
            }

            if (hit.collider.CompareTag("Engel") && CizmeBasladimi)
            {
                CizmeBasladimi = false;
                GenelYonetim._GameManager.CizgiBitti();
            }
        }



        if (Input.GetMouseButton(0) && CizmeBasladimi)
        {
            Vector2 ParmakPozisyonu = _Kamera.ScreenToWorldPoint(Input.mousePosition);

            if (ParmakPozisyonListesi.Count > 0 && Vector2.Distance(ParmakPozisyonu, ParmakPozisyonListesi[^1]) > .1f)
            {
                CizgiyiGuncelle(ParmakPozisyonu);
            }
        }

        if (Input.GetMouseButtonUp(0) && CizmeBasladimi)
        {
            this.enabled = false;
            CizmeBasladimi = false;
            GenelYonetim._GameManager.CizgiBitti();
        }
    }

    void CizgiOlustur()
    {
        ParmakPozisyonListesi.Add(_Kamera.ScreenToWorldPoint(Input.mousePosition));
        ParmakPozisyonListesi.Add(_Kamera.ScreenToWorldPoint(Input.mousePosition));

        _LineRenderer.SetPosition(0, ParmakPozisyonListesi[0]);
        _LineRenderer.SetPosition(1, ParmakPozisyonListesi[1]);
    }

    void CizgiyiGuncelle(Vector2 GelenParmakPozisyonu)
    {
        ParmakPozisyonListesi.Add(GelenParmakPozisyonu);
        _LineRenderer.positionCount++;
        _LineRenderer.SetPosition(_LineRenderer.positionCount - 1, GelenParmakPozisyonu);
    }

    public void Basla()
    {
        _Soket._Yerlestir = true;
    }

    public Vector2 SonPozisyonuVer()
    {
        return ParmakPozisyonListesi[_ParmakPozisyonIndex];
    }

  

    public Vector2 SonrakiPozisyonuVer()
    {

        if(_ParmakPozisyonIndex == ParmakPozisyonListesi.Count-1)
        {
            _Soket._Yerlestir = false;
            return ParmakPozisyonListesi[_ParmakPozisyonIndex];
        }
        else
        {
            _ParmakPozisyonIndex++;
            return ParmakPozisyonListesi[_ParmakPozisyonIndex];
        }
       
    }
}
