using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    
    Rigidbody2D myRigidbody;

    BoxCollider2D myBoxCollider;

    CapsuleCollider2D myCapsuleCollider;

    Bullet bullet;

   


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        bullet = FindObjectOfType<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
       
    }

    void OnTriggerExit2D(){
        FlipEnemy();
        Debug.Log("Quay đầu");    
    }

 
    void FlipEnemy(){
            transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
            moveSpeed = -moveSpeed;
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }


}

