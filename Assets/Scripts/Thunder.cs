using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.name != gameObject.name)
            Destroy(this.gameObject, 0.1f);
    }
}
