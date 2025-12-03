using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    int i = 1081;
    public GameObject PBgm;
    private GameObject PVgm;
    void Awake()
    {
        PVgm = Instantiate(PBgm);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
