
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TMPro;
using UnityEngine;


public class Rollingscript : MonoBehaviour
{
    public bool rolling;
    public List<Sprite> sprites = new();
    public List<GameObject> slots = new();
    public int spins;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       

    }
    public void Spin()
    {
        
    }
    public IEnumerator Stuff(int Spins)
    {
        rolling = false;
        for(int i = 0; i < Spins; i++)
        {
           
            yield return new WaitForSeconds(0.1f);
            Debug.Log("Donestuff");
            foreach(var gameObject in slots)
            {
                SpriteRenderer spriterenderer = gameObject.GetComponent<SpriteRenderer>();
                int rand = Random.Range(0, sprites.Count);
                if (gameObject.GetComponent<gambachildscript>().rolling)
                {
                    spriterenderer.sprite = sprites[rand];
                }
                
                if (i > 80)
                {
                    slots[0].GetComponent<gambachildscript>().rolling = false;
                    Debug.Log("80");
                }
                if (i > 90)
                {
                    slots[0].GetComponent<gambachildscript>().rolling = false;
                    Debug.Log("90");
                }
                if (i >= 100)
                {
                    slots[0].GetComponent<gambachildscript>().rolling = false;
                    Debug.Log("100");
                }
            }
            float random = Random.Range(1, 3);


        }
        
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

}
