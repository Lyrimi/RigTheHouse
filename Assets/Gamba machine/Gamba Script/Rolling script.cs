
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR;


public class Rollingscript : MonoBehaviour
{
    public bool rolling;
    public List<Sprite> sprites = new();
    public List<GameObject> slots = new();
    public int spins;
    bool reSend;
    public float objVel;

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
        Spin();

    }
    public void Spin()
    {
        for (int s = 0; s < 3; s++) 
        {

            int rand = Random.Range(0, sprites.Count);
            predetermined.Add(rand);
        }
        

    }
    




    public IEnumerator Stuff(int Spins)
    {
        //InstaciateRollers();
        rolling = false;




        for (int s = 0; s < slots.Count; s++)
        {
            slots[s].GetComponent<Rigidbody2D>().linearVelocityY = -objVel;

        }
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < Spins; i++)
        {



            foreach (GameObject gameObject in slots)
            {





                if (gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().maxRolls == gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().rolls)
                {
                        Debug.Log(1);
                    slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index +5].GetComponent<Rigidbody2D>().linearVelocityY = 0;
                    slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index +10].GetComponent<Rigidbody2D>().linearVelocityY = 0;
                    slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index].GetComponent<Rigidbody2D>().linearVelocityY = 0;

                    
                    slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index + 5].transform.position = new Vector3(slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index + 5].transform.position.x, 2, 0);
                    slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index + 10].transform.position = new Vector3(slots[gameObject.GetComponent<gambachildscript>().Decider.GetComponent<gambachildscript>().index + 10].transform.position.x, 0, 0);
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

            

            
                
                
                /*
                while (gameObject.transform.position.y > -4 && gameObject.GetComponent<gambachildscript>().rolling&& i < 4)
                {



                    Debug.Log("something");
                    yield return null;
                        
                    


                    if (gameObject.transform.position.y <= -4)
                    {
                        gameObject.GetComponent<gambachildscript>().rolls += 1;

                        SpriteRenderer spriterenderer = gameObject.GetComponent<SpriteRenderer>();
                        int rand = Random.Range(0, sprites.Count);
                        if (gameObject.GetComponent<gambachildscript>().rolling)
                        {
                            spriterenderer.sprite = sprites[rand];
                        }
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 2, 0);
                    }
                }

                



                


                

                
                
               


                if (i > 80)
                {

                    stoprolling(i);

                }

                 */
            
            

               
        }
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
