using System.Collections.Generic;
using UnityEngine;

public class EmblemMovementScript : MonoBehaviour
{

    
    static float speed = 13;

    [SerializeField]
    float speedMaxVar;
    [SerializeField]
    float speedMinVar;


    float restartThreshold = -4;
    float restartPosY= 2;
   
    float stopY;
    
    int rolls = 0;
    [SerializeField]
    int maxrolls = 5;


    RollState state = RollState.Rolling;


    void Start()
    {
        stopY = transform.position.y;
        speed = 13;
    }

    void Update()
    {
        switch (state)
        {
            case RollState.Rolling:
                Move();
                break;
            case RollState.Done:
                break;
        }
        
    }

    private void Move()
    {
        if (rolls >= maxrolls && transform.position.y <= stopY)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = stopY;
            transform.position = newPosition;
            speed += Random.Range(speedMinVar,speedMaxVar);
            state = RollState.Done;
            return;
        }
        transform.position += Vector3.down * speed * Time.deltaTime;
        if(transform.position.y <= restartThreshold)
        {
            float y = transform.position.y - restartThreshold;
            Vector3 newPosition = transform.position;
            newPosition.y = restartPosY + y;
            transform.position = newPosition;
            NewRandomSprite();
            rolls++;
        }
        
    }

    private void NewRandomSprite()
    {
        int rand = Random.Range(0, SlotSpriteManager.Instance.Sprites.Count);
        gameObject.GetComponent<SpriteRenderer>().sprite = SlotSpriteManager.Instance.Sprites[rand];
        
    }
     enum RollState { Rolling, Done };
}

