using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public bool IsHit;
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    GameObject Player;
    public bool IsRain;
    void Awake(){
        Player = GameObject.FindGameObjectWithTag("Player");
        bool flip = Player.GetComponent<SpriteRenderer>().flipX;
        IsHit = false;
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.flipX = flip;
        rigid.velocity = IsRain == false ? (Vector2.right * 24 * (flip == true? -1 : 1) + Vector2.up * 1.5f) : Vector2.down * 1.5f;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag== "Player" || other.gameObject.layer ==LayerMask.NameToLayer("PlayerAttack")){
            return;
        }
        Debug.Log("Name" + other.gameObject.name);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag== "Player" || other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack")){
            return;
        }
        Debug.Log("Name" + other.gameObject.name);
        Destroy(gameObject);
    }
    
    // Update is called once per frame
}
