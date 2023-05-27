using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessingController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!Utils.SHADER_ON)
        {
            gameObject.SetActive(false);
        }
    }

}
