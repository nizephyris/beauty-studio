using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TutorialScreenView : MonoBehaviour
{
    [SerializeField] private GameObject _mainScreenRoot;
    [SerializeField] private TMP_Text _participantNameText;
    [SerializeField] private RectTransform _slideViewport;
    [SerializeField] private RectTransform _currentSlide;
    [SerializeField] private RectTransform _nextSlide;
    [SerializeField] private Image _currentSlideImage;
    [SerializeField] private Image _nextSlideImage;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private Button _nextButton;
    [SerializeField] private List<TutorialStage> _stages;
    [SerializeField] private List<Image> _progressBars;
    [FormerlySerializedAs("defaultProgressSprite")]
    [SerializeField] private Sprite _defaultProgressSprite;
    [FormerlySerializedAs("currentProgressSprite")]
    [SerializeField] private Sprite _currentProgressSprite;

    private TutorialFlowService _flowService;

    public GameObject MainScreenRoot => _mainScreenRoot;

    public RectTransform SlideViewport => _slideViewport;

    public RectTransform CurrentSlide => _currentSlide;

    public RectTransform NextSlide => _nextSlide;

    public Image CurrentSlideImage => _currentSlideImage;

    public Image NextSlideImage => _nextSlideImage;

    public TMP_Text ButtonText => _buttonText;

    public Button NextButton => _nextButton;

    public IReadOnlyList<TutorialStage> Stages => _stages;

    public IReadOnlyList<Image> ProgressBars => _progressBars;

    public Sprite DefaultProgressSprite => _defaultProgressSprite;

    public Sprite CurrentProgressSprite => _currentProgressSprite;

    public void Bind(TutorialFlowService flowService) => _flowService = flowService;

    public void SetParticipantName(string name) => _participantNameText.text = name;

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    public void SetCurrentSlide(RectTransform slide) => _currentSlide = slide;

    public void SetNextSlide(RectTransform slide) => _nextSlide = slide;

    public void SetCurrentSlideImage(Image image) => _currentSlideImage = image;

    public void SetNextSlideImage(Image image) => _nextSlideImage = image;

    private void OnEnable() => _flowService.StartFlow();

    private void OnDestroy() => _flowService.Dispose();
}
