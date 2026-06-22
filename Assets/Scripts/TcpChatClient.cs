using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public class TcpChatClient : IDisposable
{
    private string _serverIp;
    private int _serverPort;
    
    // TCP 관련한 변수 선언
    private TcpClient _tcpClient;
    private NetworkStream _stream;
    private StreamReader _reader;
    private StreamWriter _writer;
    
    // 서버 상태 변수
    private bool _isConnected;
    public bool IsConnected => _isConnected;
    
    // 릴리즈 여부 변수
    private bool _isDisposed;
    
    // Dispatcher Event 선언
    public event Action Connected;
    public event Action Disconnected;
    public event Action<string> MessageReceived;
    public event Action<string> ErrorReceived;
    
    // 생성자
    public TcpChatClient(string serverIp, int serverPort)
    {
        _serverIp = serverIp;
        _serverPort = serverPort;
        _isConnected = false;
        _isDisposed = false;
    }

    #region 서버 연결

    public async Task<bool> ConnectServerAsync()
    {
        if (_isConnected)
        {
            Debug.Log("이미 서버에 연결되어 있습니다.");
            // TODO: 에러 이벤트 발생
            return false;
        }

        try
        {

        }
        catch (Exception e)
        {
            Debug.LogError("서버 연결 실패:" + e.Message);
            // TODO: 에러 이벤트 발생
            return false;
        }
    }
    #endregion
    
    #region 메시지 수신
    #endregion
    
    #region 메시지 송신
    #endregion
    
    
    
    public void Dispose()
    {
        // TODO: 릴리즈 작업
    }
}
