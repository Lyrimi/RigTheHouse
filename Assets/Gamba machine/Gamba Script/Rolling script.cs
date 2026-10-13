
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
            Mathf.Lerp(0.01f, 2, 0.1f);
            yield return new WaitForSeconds(i);
            Debug.Log("Donestuff");
            foreach(var gameObject in slots)
            {
                SpriteRenderer spriterenderer = gameObject.GetComponent<SpriteRenderer>();
                int rand = Random.Range(0, sprites.Count);
                spriterenderer.sprite = sprites[rand];
                
            }
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
