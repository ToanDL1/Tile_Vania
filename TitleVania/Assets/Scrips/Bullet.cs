using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speedMove = 10f;
    float xSpeed;

    Rigidbody2D bulletRigidBody;

    CapsuleCollider2D bulletCollider;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<CapsuleCollider2D>();
        player = FindObjectOfType<Player>();
        xSpeed = player.transform.localScale.x * speedMove;
        DirectionBullet();
    }

    // Update is called once per frame
    void Update()
    {
        // DirectionBullet();
        BulletMove();
        
    }

    void BulletMove(){
        
        bulletRigidBody.velocity = new Vector2(xSpeed, 0f);
    }
    
    void DirectionBullet(){
        transform.localScale = new Vector3(transform.localScale.x * player.transform.localScale.x,transform.localScale.y,transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D other){
          if(other.tag == "Enemies"){
            Destroy(other.gameObject);
          }
        Destroy(gameObject);
    }
    

}
