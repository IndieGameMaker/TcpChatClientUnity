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

    // 이벤트 저장용 큐(Queue) : FIFO 선입선출
    private readonly Queue<Action> _executeQueue = new Queue<Action>();

    // 스레드에서 작업할(호출할) 이벤트를 큐에 저장하는 메서드
    // lock 잠금 장치 역할을 한다. 이미 잠겨있으면, 다른 스레드는 잠금이 풀릴때까지 대기
    public void Enqueue(Action action)
    {
        lock (_executeQueue)
        {
            _executeQueue.Enqueue(action);
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        lock (_executeQueue)
        {
            while (_executeQueue.Count > 0)
            {
                _executeQueue.Dequeue()?.Invoke();
            }
        }
    }
}