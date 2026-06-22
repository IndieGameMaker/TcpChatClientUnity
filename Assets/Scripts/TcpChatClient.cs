using System;
using System.IO;
using System.Net.Sockets;

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
    
    public void Dispose()
    {
        // TODO: 릴리즈 작업
    }
}
