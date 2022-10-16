using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCameraTarget : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject characters;
    CameraTarget[] charactersPos;
    int cont = 0;
    CinemachineVirtualCamera virtualcam;
    void Start()
    {
        charactersPos = characters.GetComponentsInChildren<CameraTarget>();

        virtualcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Setcounter(true);
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {

            Setcounter(false);

        }
        
        virtualcam.LookAt = charactersPos[cont].transform;
        virtualcam.Follow = charactersPos[cont].transform;
        
    }

    void Setcounter(bool plus)
    {
        Debug.Log(cont + "|||||" + charactersPos.Length);
        if (plus)
        {
            cont++;
        }
        else
        {
            cont--;
        }
        if (cont > charactersPos.Length - 1)
        {
            cont = 0;
        }
        else if(cont < 0)
        {
            cont = charactersPos.Length - 1;
        }
    }
}
