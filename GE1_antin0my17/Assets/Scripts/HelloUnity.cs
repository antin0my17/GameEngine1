using UnityEngine;

public class HelloUnity : MonoBehaviour
{
    void Start()
    {
        Debug.Log("안녕하세요, Unity와 C#의 세계의 오신 것을 환영합니다.");
        Debug.Log("week5");
        Debug.Log("10 + 20 = " + (10 + 20));
        Debug.Log("제가 붙어있는 오브젝트 이름" + gameObject.name);
    }

    void Update()
    {
        
    }
}