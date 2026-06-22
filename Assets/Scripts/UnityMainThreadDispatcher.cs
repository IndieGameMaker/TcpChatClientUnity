using System;
using System.Collections.Generic;
using UnityEngine;

public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static UnityMainThreadDispatcher _instance;

    public static UnityMainThreadDispatcher Instance
    {
        get
        {
            // 싱글턴에 처음 접근하는 경우 (아직 인스턴스가 생성되기 이전일 경우)
            if (_instance == null)
            {
                var go = new GameObject("UnityMainThreadDispatcher");
                _instance = go.AddComponent<UnityMainThreadDispatcher>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    
    // 이벤트 저장용 큐(Queue)
    private readonly Queue<Action> _executeQueue = new Queue<Action>();
    

}
