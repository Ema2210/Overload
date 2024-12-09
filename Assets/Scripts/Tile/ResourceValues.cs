using System.Collections.Generic;
using UnityEngine;

public class ResourceValues : MonoBehaviour
{
    public static ResourceValues Instance = null;
    [field: SerializeField] public Diggable[] LowValue { get; private set; }
    [field: SerializeField] public Diggable[] MediumValue { get; private set; }
    [field: SerializeField] public Diggable[] HighValue { get; private set; }
    [field: SerializeField] public Diggable[] IncredibleValue { get; private set; }
    [field: SerializeField] public Diggable[] INVALUABLEValue { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //diggable assets from the Resources/Diggables folder
        Diggable[] allDiggables = Resources.LoadAll<Diggable>("Diggables");

        //temp lists to hold categorized diggables
        List<Diggable> lowValueList = new List<Diggable>();
        List<Diggable> mediumValueList = new List<Diggable>();
        List<Diggable> highValueList = new List<Diggable>();
        List<Diggable> incredibleValueList = new List<Diggable>();
        List<Diggable> invaluableValueList = new List<Diggable>();


        foreach (Diggable dig in allDiggables)
        {
            switch (dig.ValuePerUnit)
            {
                case <= 60:
                    lowValueList.Add(dig);
                    break;
                case > 60 and <= 150:
                    mediumValueList.Add(dig);
                    break;
                case > 150 and <= 1000:
                    highValueList.Add(dig);
                    break;
                case > 1000 and <= 5000:
                    incredibleValueList.Add(dig);
                    break;
                case > 5000:
                    invaluableValueList.Add(dig);
                    break;
            }
        }

        //convert the lists to arrays and assign them
        LowValue = lowValueList.ToArray();
        MediumValue = mediumValueList.ToArray();
        HighValue = highValueList.ToArray();
        IncredibleValue = incredibleValueList.ToArray();
        INVALUABLEValue = invaluableValueList.ToArray();
    }
}

