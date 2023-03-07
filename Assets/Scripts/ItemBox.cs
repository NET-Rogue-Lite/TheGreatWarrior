using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    bool CanOpen;
    EventDrop eventDrop;
    // Update is called once per frame
    void Awake(){
        eventDrop = GameObject.Find("EventDrop").GetComponent<EventDrop>();
        CanOpen = false;
        if(gameObject.name=="HiddenItemBox"){
            CanOpen = true;
        }
    }
    void FixedUpdate()
    {
        if (GameObject.FindWithTag("Monster") == null){
            CanOpen = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            if(CanOpen){
                eventDrop.gameObject.transform.position = gameObject.transform.position;
                eventDrop.Drop(gameObject.name,gameObject.transform.position);
                Destroy(gameObject);
            }
        }
    }
}
