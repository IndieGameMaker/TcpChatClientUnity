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
}
