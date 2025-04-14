using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;

    private Vector3 tempPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindWithTag("Player") != null){
            player = GameObject.FindWithTag("Player").transform;
        }

    }
    void LateUpdate()
    {
        if(player != null){    
            if (transform.position.y >= 0 && transform.position.y <= 32){    
                tempPos = transform.position;
                tempPos.y = player.position.y;
                if(tempPos.y <= 32 && tempPos.y >= 0){
                    transform.position = tempPos;
                }
                
            }
            else if(transform.position.y < 0){
                tempPos.x = 0f;
            }
            else if(transform.position.y > 32){
                tempPos.x = 32f;
            }
        }
        
    }
}
