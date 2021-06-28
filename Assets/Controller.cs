using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Material material;
    public Vector2 position;
    public float scale;
    public float angle;

    private Vector2 smoothPosition;
    private float smoothScale;
    private float smoothAngle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        updateShader();
        inputHandler();
    }

    private void updateShader()
    {

        smoothPosition = Vector2.Lerp(smoothPosition, position, 0.03f);
        smoothScale = Mathf.Lerp(smoothScale, scale, 0.03f);
        smoothAngle = Mathf.Lerp(smoothAngle, angle, 0.03f);

        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float scaleX = smoothScale;
        float scaleY = smoothScale;

        if (aspectRatio > 1f)
        {
            scaleY /= aspectRatio;
        }
        else
        {
            scaleX *= aspectRatio;
        }

        material.SetVector("_Area", new Vector4(smoothPosition.x, smoothPosition.y, scaleX, scaleY));
        material.SetFloat("_Angle", smoothAngle);
    }

    private void inputHandler()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            scale *= .99f;
        }

        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            scale *= 1.01f;
        }

        Vector2 direction = new Vector2(0.01f * scale, 0);
        float sine = Mathf.Sin(angle);
        float cosine = Mathf.Cos(angle);
        direction = new Vector2(direction.x * cosine - direction.y * sine, direction.x * sine + direction.y * cosine);

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            position -= direction;
        }
        
        if(Input.GetKey(KeyCode.RightArrow))
        {
            position += direction;
        }

        direction = new Vector2(-direction.y, direction.x);

        if(Input.GetKey(KeyCode.DownArrow))
        {
            position -= direction;
        }
        
        if(Input.GetKey(KeyCode.UpArrow))
        {
            position += direction;
        }
        
        
        if(Input.GetKey(KeyCode.Q))
        {
            angle -= .01f;
        }
        
        if(Input.GetKey(KeyCode.E))
        {
            angle += .01f;
        }
    }
}
