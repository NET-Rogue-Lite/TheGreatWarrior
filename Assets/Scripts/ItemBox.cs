using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    bool CanOpen;
    public EventDrop eventDrop;
    // Update is called once per frame
    void Awake(){
        CanOpen = false;
    }
    void FixedUpdate()
    {
        if (GameObject.FindWithTag("Monster") == null){
            CanOpen = true;
        }
        else {
            CanOpen = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            if(CanOpen){
                eventDrop.gameObject.transform.position = gameObject.transform.position;
                eventDrop.Drop(gameObject.name);
                Destroy(gameObject);
            }
        }
    }
}
