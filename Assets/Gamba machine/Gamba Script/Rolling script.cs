


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using UnityEngine.XR;


public class Rollingscript : MonoBehaviour
{
    public bool rolling;
    public List<Sprite> sprites = new();
    public List<GameObject> slots = new();
    public int spins;
    bool reSend;
    public float objVel;

    public int endstep;

    public List<int> predetermined;
    public List<Vector3> Startpos;

    Vector3 pos1;
    Vector3 pos2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(GameObject g in slots)
        {
            Startpos.Add(g.transform.position);
        }
        

    }
  
    public void Ossamabinarray(int[] Numbers)
    {
        

        rolling = true;

        StartCoroutine(Endstep(Numbers));
        
    }

    public IEnumerator Endstep(int[] Numero)
    {
        
        yield return new WaitUntil(() => slots[5].GetComponent<gambachildscript>().rolls == 29);
        slots[5].GetComponent<SpriteRenderer>().sprite = sprites[Numero[0]];
        
        yield return new WaitUntil(() => slots[6].GetComponent<gambachildscript>().rolls == 30);
        slots[6].GetComponent<SpriteRenderer>().sprite = sprites[Numero[1]];
        
        yield return new WaitUntil(() => slots[7].GetComponent<gambachildscript>().rolls == 31);
        slots[7].GetComponent<SpriteRenderer>().sprite = sprites[Numero[2]];
       
        yield return new WaitUntil(() => slots[8].GetComponent<gambachildscript>().rolls == 32);
        slots[8].GetComponent<SpriteRenderer>().sprite = sprites[Numero[3]];
        
        yield return new WaitUntil(() => slots[9].GetComponent<gambachildscript>().rolls == 33);
        slots[9].GetComponent<SpriteRenderer>().sprite = sprites[Numero[4]];
        
        
    }


    public IEnumerator Stuff(int Spins)
    {
        //InstaciateRollers();
        rolling = false;
       

        yield return new WaitForEndOfFrame();

        for (int s = 0; s < slots.Count; s++)
        {

            slots[s].GetComponent<Rigidbody2D>().linearVelocityY = -objVel;

        }
        yield return new WaitForEndOfFrame();
        foreach (GameObject ge in slots)
        {

            ge.GetComponent<gambachildscript>().rolling = true;

        }


        for (int i = 0; i < Spins; i++)
        {
            endstep = i;


            foreach (GameObject gameObject in slots)
            {

                



                if (gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().maxRolls == gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().rolls)
                {
                    
                    Debug.Log(1);
                    slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index +5].GetComponent<Rigidbody2D>().linearVelocityY = 0;
                    slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index +10].GetComponent<Rigidbody2D>().linearVelocityY = 0;
                    slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index].GetComponent<Rigidbody2D>().linearVelocityY = 0;

                    
                    slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index + 5].transform.position = new Vector3(slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index + 5].transform.position.x, 0, 0);
                    slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index + 10].transform.position = new Vector3(slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index + 10].transform.position.x, 2, 0);
                    slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index].transform.position = new Vector3(slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index].transform.position.x, -2, 0);

                    gameObject.GetComponent<gambachildscript>().rolling = false;
                    
                    yield return new WaitForEndOfFrame();
                        
                        continue;
                }
                
               
                yield return new WaitUntil(() => gameObject.transform.position.y <= -4);
                
                if (gameObject.transform.position.y <= -4)
                {
                    gameObject.GetComponent<gambachildscript>().rolls += 1;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, 2, 0);

                    SpriteRenderer spriterenderer = gameObject.GetComponent<SpriteRenderer>();
                    int rand = Random.Range(0, sprites.Count);
                    if (gameObject.GetComponent<gambachildscript>().rolling)
                    {
                        spriterenderer.sprite = sprites[rand];
                    }
                    

                }
                

                
            }

            Debug.Log("e");

            

            
                
              

               
        }
        endstep = 0;
        foreach (GameObject g in slots)
        {

            g.GetComponent<gambachildscript>().rolls = 0;
           
        }
        Debug.Log("fin");
        yield return new();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (rolling)
        {
            StartCoroutine(Stuff(spins));
        }
        
    }
    private void stoprolling(int Rolls)
    {
        foreach (var gameObject in slots)
        {
            if(gameObject.GetComponent<gambachildscript>().rolls >= gameObject.GetComponent<gambachildscript>().maxRolls)
            {
                gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                gameObject.GetComponent<gambachildscript>().rolling = false;
            }
            
        }
    }

    private void InstaciateRollers()
    {

        pos1 = new Vector3(slots[0].transform.position.x, 0,0);
        pos2 = new Vector3(slots[0].transform.position.x, 0, 0);

        Instantiate(slots[0], pos1,Quaternion.identity);


    }



}
