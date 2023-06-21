using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OlcayKalyoncuoglu;

public class OyunSesi : MonoBehaviour
{
    static GameObject Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (BellekYonetimi.VeriOkuInt("OyunSesi") == 0)
        {
            GetComponent<AudioSource>().mute = true;
        }

        DontDestroyOnLoad(gameObject);
        if (Instance == null)
            Instance = gameObject;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
