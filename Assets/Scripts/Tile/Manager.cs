using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }
    public static GridSystem gridSystem { get; private set; }

    void Awake()
    {
        Instance = this;
    }




}
