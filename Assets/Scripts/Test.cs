using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class Test : MonoBehaviour
{
    private event MyEventHandler myEvent;
    
    [SerializeField]
    private GameObject dimMamisLiecht;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Marine Survivors");
        Light2D light2D = dimMamisLiecht.GetComponent<Light2D>();
        light2D.color = Color.red;
        
        myEvent += OnmyEvent;
    }

    private void OnmyEvent(MyEventArgs args)
    {
        Console.WriteLine(args.SenderType + args.MyArg);
    }

    // Update is called once per frame
    void Update()
    {
        myEvent?.Invoke(new MyEventArgs("Blabla", this.GetType()));
    }
}
