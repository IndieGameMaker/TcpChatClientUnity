using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
    
    // 종료 토큰 선언
    private CancellationTokenSource _cts = new CancellationTokenSource();
    
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
            // TCP 소켓 초기화
            _tcpClient = new TcpClient();
            // 서버 접속
            await _tcpClient.ConnectAsync(_serverIp, _serverPort);
            // 스트림 초기화
            _stream = _tcpClient.GetStream();
            _reader = new StreamReader(_stream, new UTF8Encoding(false)); // BOM 사용하지 않음
            _writer = new StreamWriter(_stream, new UTF8Encoding(false)) {AutoFlush = true};
            _isConnected = true;
            
            // TODO: 서버 연결 이벤트 발생
            
            // 수신 루프 시작(백그라운드 스레드 가동)
            _ = Task.Run( () => ReceiveMessageAsync(_cts.Token) );
            
            return true;
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
    public async Task ReceiveMessageAsync(CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                string message = await _reader.ReadLineAsync();

                if (message == null)
                {
                    // 서버가 종료된 경우
                    Debug.Log("서버와 연결이 종료되었습니다.");
                    _isConnected = false;
                    
                    // TODO: 종료 이벤트 발생
                    break;
                }

                Debug.Log("메시지 수신:" + message);
                // TODO: 메시지 수신 이벤트 발생
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("수신 작업 취소");
        }
        finally
        {
            Dispose();
        }
    }
    #endregion
    
    #region 메시지 송신
    public async Task SendMessageAsync(string message)
    {
        if (!_isConnected || _writer == null)
        {
            Debug.LogError("서버에 연결되지 않았습니다.");
            // TODO: 에러 이벤트 발생
        }

        try
        {
            await _writer.WriteLineAsync(message);
        }
        catch (Exception e)
        {
            Debug.LogError("메시지 전송 실패: "+e.Message);
            // TODO: 에러 이벤트 발생
            // 연결 종료
            Dispose();
        }
    }
    #endregion
    
    
    
    public void Dispose()
    {
        // 릴리즈 작업
        if (_isDisposed) return;
        _isDisposed = true;
        _isConnected = false;
        
        _reader?.Dispose();
        _writer?.Dispose();
        _stream?.Dispose();
        
        _tcpClient?.Dispose();
    }
}
