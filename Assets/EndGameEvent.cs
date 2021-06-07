using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameEnd());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(6.0f);
        SceneManager.LoadScene("DemoEnd");
        Debug.Log("test");
    }
}
