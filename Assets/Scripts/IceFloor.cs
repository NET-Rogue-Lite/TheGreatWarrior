using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFloor : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Rigidbody2D>().velocity *= 1.8f;
            other.gameObject.GetComponent<PlayerMove>().maxSpeedx = 15;
        }
    }
}
