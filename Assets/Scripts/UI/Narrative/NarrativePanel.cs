using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativePanel : MonoBehaviour
{
    private void OnEnable()
    {
        Pause.TogglePause();
    }
    private void OnDisable()
    {
        Pause.TogglePause();
    }

}
