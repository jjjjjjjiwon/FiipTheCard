using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
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
