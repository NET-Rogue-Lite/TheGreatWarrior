using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    Animator anim;
    private float Hp = 100;
    public float Ad;
    private float Speed;
    public float Idle_speed;
    public float Chase_speed;

    public float Attack_range;
    public float Close_range;

    private bool isPlayer_close = false;
    private bool isAttack = false;

    private float playerDistance;
    private GameObject Player;

    Rigidbody2D rigid;
    public int nextMove;
    public int currentMove;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capuslecollider;
    

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capuslecollider = GetComponent<CapsuleCollider2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer.flipX = (nextMove == -1);
        Think();
        //Invoke("Think", 5);
    }

    void FixedUpdate()
    {
        playerDistance = Vector3.Distance(transform.position, Player.transform.position);
        CheckPlayerClose();
        Attack();
        Move(); 
    }

    private void CheckPlayerClose(){
        if (playerDistance <= Attack_range){
            CancelInvoke();
            isPlayer_close = true;
            isAttack = true;
        }
        else if (playerDistance <= Close_range){
            CancelInvoke();
            Speed = Chase_speed;
            isPlayer_close = true;
            isAttack = false;
        }
        else {
            if (isPlayer_close){
                Invoke("Think", 1);
            }
            Speed = Idle_speed;
            isPlayer_close = false;
            isAttack = false;
        }
    }
    
    private void Attack(){
        anim.SetBool("PlayerClosetoAttack", isAttack);
        if (isAttack){

            Hp -= 0.1f;
            if (Hp <= 0){
                anim.SetBool("IsDied", true);
                Destroy(gameObject);
            }
        }
    }

    private void Move(){
        if (!isPlayer_close){
            rigid.velocity = new Vector2(Speed * nextMove, rigid.velocity.y);
            MapCheck();
        }
        else if (!isAttack){
            rigid.velocity = (Player.transform.position - transform.position).normalized * Speed;
            spriteRenderer.flipX = (rigid.velocity.x < 0);
            MapCheck();
        }
        else{
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            CancelInvoke();
        }
    }

    private void MapCheck(){
        Debug.DrawRay(rigid.position + Vector2.right * (nextMove) * 0.7f + Vector2.down, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position + Vector2.right * (nextMove) * 0.7f + Vector2.down * 0.5f, Vector3.down, 1, LayerMask.GetMask("Map"));
        if (rayHit.collider == null)
        {
            CancelInvoke();
            Invoke("Think", 1);
            nextMove = -nextMove;
            currentMove *= nextMove;
            rigid.velocity = new Vector2(nextMove*Speed, rigid.velocity.y);
            spriteRenderer.flipX = (nextMove == -1);
            Debug.Log("Warning");
        }
    }

    void Think()
    {
        Debug.Log("Thinking...");
        nextMove = Random.Range(-1, 2);

        //쳐다보는 방향에 대한 코드
        if(!isPlayer_close){
            spriteRenderer.flipX = (nextMove == -1);
            if (nextMove != 0)
                currentMove *= nextMove;
        }
        
        float nextThinkTime = Random.Range(1f, 5f);
        Invoke("Think", nextThinkTime);
    }

    public void OnDamaged()
    {
        Debug.Log("OnDamaged");
        nextMove = 0;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        spriteRenderer.flipY = true;

        capuslecollider.enabled = false;

        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        Invoke("DeActive", 2.0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Skill")
        {
            OnDamaged();
            Destroy(other.gameObject);
        }
    }
}
