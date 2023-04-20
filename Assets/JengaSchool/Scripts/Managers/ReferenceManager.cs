using TMPro;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance { get; private set; }

    [SerializeField] private GameObject[] _blockPrefabs;
    [SerializeField] private ApiManager _apiManager;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private Transform _jengasParent;
    
    [Header("UI")]
    [SerializeField] private GameObject _blockInfoPanel;
    [SerializeField] private TMP_Text _blockInfoPanelText;
    
    public GameObject[] BlockPrefabs => _blockPrefabs;
    public ApiManager ApiManager => _apiManager;
    public Camera MainCamera => _mainCamera;
    public CameraMovement CameraMovement => _cameraMovement;
    public Transform JengasParent => _jengasParent;
    
    public GameObject BlockInfoPanel => _blockInfoPanel;
    public TMP_Text BlockInfoPanelText => _blockInfoPanelText;
    
    private void Awake()
    {
        Instance = this;
    }
}