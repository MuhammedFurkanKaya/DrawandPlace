using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OlcayKalyoncuoglu;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnaMenuManager : MonoBehaviour
{
    [SerializeField] Slider _Slider;
    [SerializeField] GameObject _LoadingPanel;

    private void Awake()
    {
        if (!BellekYonetimi.AnahtarVarmi("Level"))
        {
            BellekYonetimi.VeriKaydetInt("Level", 1);
            BellekYonetimi.VeriKaydetInt("Puan", 50);
            BellekYonetimi.VeriKaydetInt("OyunSesi", 1);
            BellekYonetimi.VeriKaydetInt("EfektSesi", 1);


        }
    }

    public void Basla()
    {
        StartCoroutine(SahneYukle(BellekYonetimi.VeriOkuInt("Level")));
    }

    IEnumerator SahneYukle(int SahneIndex)
    {
        AsyncOperation Op = SceneManager.LoadSceneAsync(SahneIndex);
        _LoadingPanel.SetActive(true);
        while (!Op.isDone)
        {
            float prog = Mathf.Clamp01(Op.progress / .9f);
            _Slider.value = prog;
            yield return null;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
