using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkills : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.name != gameObject.name)
            Destroy(this.gameObject, 0.05f);
    }
}
