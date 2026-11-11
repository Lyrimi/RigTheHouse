using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    Vector3 camOriginPoint;
    [SerializeField]
    Vector3 camCurrentPoint;
    float percentage;
    Vector3 distanceVector;


    float camOffSetY;
    float camOffSetX;
    void Start()
    {
        camOriginPoint = gameObject.transform.position;
        
    }

    void CameraShaker( Vector3 Endpos, float InterpVal,bool Shaking,float Percent)
    {
        if (Shaking)
        {
            //distanceVector = camCurrentPoint - Endpos;
            camCurrentPoint = Vector3.Lerp(camOriginPoint, Endpos, InterpVal);
            gameObject.transform.position = camCurrentPoint;
            if (VectorDistance(camCurrentPoint, Endpos, Percent) == false)
            {

            }
            
        }
        else 
        {
         gameObject.transform.position = camOriginPoint;
        }
        
        
    }
    bool VectorDistance(Vector3 Vec1, Vector3 Vec2, float Percent)
    {
        if (Vec1.x - Vec2.x > camOriginPoint.x * Percent)
        {
            Debug.Log("I don't understand vectors");
            return true;
        }
        else
            Debug.Log("Falsingit");
        return false;
        
    }

    // Update is called once per frame
    void Update()
    {
        camCurrentPoint = gameObject.transform.position;
        
        CameraShaker(new Vector3(2, 1, camOriginPoint.z), Time.deltaTime,true,percentage);
         
    }
}
