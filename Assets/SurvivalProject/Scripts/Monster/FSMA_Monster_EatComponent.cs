using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FSMA_Monster_EatComponent : MonoBehaviour
{
    public event Action<FSMA_Food> OnSelect = null;
    [SerializeField] FSMA_Food target = null;
    [SerializeField] FSMA_Monster_DetectionComponent detectionComponent = null;
    bool notCollected = false;

    public FSMA_Food Target => target;

    // Start is called before the first frame update
    void Start()
    {
        detectionComponent = GetComponent<FSMA_Monster_DetectionComponent>();
    }

    // Update is called once per frame
    void Update()
    {



    }


    public void Eat(FSMA_Food _food)
    {
        target = _food;
        if (!target) return;
        Destroy(target.gameObject);
        target = null;
    }

}
