using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    [Header("Server Settings")]
    [SerializeField] private string _serverIp = "127.0.0.1";
    [SerializeField] private int _serverPort = 7777;

    [Header("UI")] 
    [SerializeField] private TMP_InputField _nickNameInput;
    [SerializeField] private TMP_InputField _messageInput;
    [SerializeField] private Button _connectButton;
    [SerializeField] private Button _sendButton;
    [SerializeField] private TextMeshProUGUI _chatText;
    
    // TCP 클라이언트 선언
    private TcpChatClient _tcpChatClient;
    
    // 닉네임 저장 변수
    private string _nickName;

    #region 유니티 콜백 메서드

    private async Task Awake()
    {
        // TCP 클라이언 인스턴스 생성
        _tcpChatClient = new TcpChatClient(_serverIp, _serverPort);
        await _tcpChatClient.ConnectServerAsync();
    }

    private void OnDestroy()
    {
        _tcpChatClient?.Dispose();
    }

    private void OnApplicationQuit()
    {
        _tcpChatClient?.Dispose();
    }

    private void OnEnable()
    {
        // TCP 클라이언트 이벤트 콜백 연결
        _tcpChatClient.Connected += OnConnected;
        _tcpChatClient.Disconnected += OnDisconnected;
        _tcpChatClient.MessageReceived += OnMessageReceived;
        _tcpChatClient.ErrorReceived += OnErrorReceived;
        
        // UI 이벤트 연결
        _connectButton.onClick.AddListener(OnConnectButtonClicked);
        _sendButton.onClick.AddListener(OnSendButtonClicked);
        _messageInput.onSubmit.AddListener(OnMessageSummit);
    }
    #endregion


    #region UI 이벤트 핸들러
    private void OnConnectButtonClicked()
    {
        
    }

    private void OnSendButtonClicked()
    {
        
    }

    private void OnMessageSummit(string arg0)
    {
        
    }
    #endregion

    #region 이벤트 핸들러
    private void OnConnected()
    {
        
    }

    private void OnDisconnected()
    {
        
    }

    private void OnMessageReceived(string obj)
    {
        
    }

    private void OnErrorReceived(string obj)
    {
        
    }

    #endregion
}
