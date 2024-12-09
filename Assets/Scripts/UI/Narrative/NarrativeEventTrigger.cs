using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarrativeEventTrigger : MonoBehaviour
{

    [SerializeField] NarrativeEventSO narrativeEventSO;
    [SerializeField] GameObject narrativeEventPanel;

    [SerializeField] TextMeshProUGUI narrativeEventText;
    [SerializeField] TextMeshProUGUI narrativeEventTitle;

    Collider2D collider2d;

    private void Awake()
    {
        TryGetComponent(out collider2d);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player") return;
        TriggerNarrativeEvent();

    }

    public void TriggerNarrativeEvent()
    {
        narrativeEventPanel.SetActive(true);

        narrativeEventText.text = narrativeEventSO.Text;
        narrativeEventTitle.text = narrativeEventSO.EventName;
        //narrativeEventTitle.art = narrativeEventSO.EventName;

        if (collider2d == null) return; //if it is earthquake, return

        collider2d.enabled = false;
        Destroy(gameObject);
    }




    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireCube(transform.position, new Vector3(1f, 1f, 0));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(1.1f, 1.1f, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(1.2f, 1.2f, 0));
    }
}

