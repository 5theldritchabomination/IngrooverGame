using UnityEngine;

public class TestExit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnClick()
    {
        Application.Quit();
    }
}
