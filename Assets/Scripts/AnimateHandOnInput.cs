using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pellisco, agarrar;
    public Animator handAnimator;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //Obtenemos el valor de la propiedad pellisco en valor de tipo float
        float triggerValue = pellisco.action.ReadValue<float>();

        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = agarrar.action.ReadValue<float>();

        handAnimator.SetFloat("Grip", gripValue);

    //    rotar el cubo hacia izquierda o derecha y arriba o abajo dependiendo del valor de la propiedad pellisco

        // Girar el cubo hacia la izquierda
        if(triggerValue == 1)
        {
            target.transform.Rotate(0, 90, 0, Space.World);
        }
        // Girar el cubo hacia la derecha
        if(triggerValue < 1)
        {
            target.transform.Rotate(0, -90, 0, Space.World);
        }
        // Girar el cubo hacia arriba
        if(gripValue == 1)
        {
            target.transform.Rotate(90, 0, 0, Space.World);
        }
        // Girar el cubo hacia abajo
        if(gripValue < 1)
        {
            target.transform.Rotate(-90, 0, 0, Space.World);
        }


    }
}
