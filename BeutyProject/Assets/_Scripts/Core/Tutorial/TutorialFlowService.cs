using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFlowService
{
    private readonly TutorialScreenView _screen;
    private readonly MakeupCatalogConfig _catalog;
    private readonly List<TutorialStage> _orderedStages = new();

    private int _currentStageIndex;
    private bool _isAnimating;
    private bool _isCompleted;
    private float _slideDuration = 0.35f;
    private Vector2 _slideRestPosition;
    private Vector2 _slideRestSizeDelta;
    private float _slideHiddenPosX;

    public TutorialFlowService(TutorialScreenView screen, MakeupCatalogConfig catalog)
    {
        _screen = screen;
        _catalog = catalog;
        _screen.Bind(this);
    }

    public void StartFlow()
    {
        if (_isCompleted)
        {
            return;
        }

        _screen.SetParticipantName(_catalog.PlayerDisplayName);
        _orderedStages.Clear();
        _orderedStages.AddRange(_screen.Stages.OrderBy(stage => stage.StageIndex));
        _screen.NextButton.onClick.RemoveListener(OnNextClicked);
        _screen.NextButton.onClick.AddListener(OnNextClicked);
        CacheSlideLayout();
        ShowStage(0, false);
    }

    public void Dispose() => _screen.NextButton.onClick.RemoveListener(OnNextClicked);

    private void OnNextClicked()
    {
        if (_isAnimating)
        {
            return;
        }

        if (_currentStageIndex >= _orderedStages.Count - 1)
        {
            CompleteTutorial();
            return;
        }

        PlaySlideToStage(_currentStageIndex + 1);
    }

    private void CompleteTutorial()
    {
        _isCompleted = true;
        _screen.NextButton.onClick.RemoveListener(OnNextClicked);
        _screen.Hide();

        if (_screen.MainScreenRoot == _screen.gameObject)
        {
            return;
        }

        _screen.MainScreenRoot.SetActive(true);
    }

    private void PlaySlideToStage(int targetStageIndex)
    {
        _isAnimating = true;
        _screen.NextButton.interactable = false;

        TutorialStage targetStage = _orderedStages[targetStageIndex];

        ApplySlideLayout(_screen.NextSlide);
        _screen.NextSlideImage.sprite = targetStage.BackgroundImage;
        _screen.NextSlide.anchoredPosition = new Vector2(_slideHiddenPosX, _slideRestPosition.y);
        _screen.NextSlide.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.Join(_screen.CurrentSlide.DOAnchorPosX(_slideRestPosition.x - _slideHiddenPosX, _slideDuration).SetEase(Ease.OutCubic));
        sequence.Join(_screen.NextSlide.DOAnchorPosX(_slideRestPosition.x, _slideDuration).SetEase(Ease.OutCubic));
        sequence.OnComplete(() => CompleteSlide(targetStageIndex));
    }

    private void CompleteSlide(int targetStageIndex)
    {
        RectTransform previousSlide = _screen.CurrentSlide;
        Image previousSlideImage = _screen.CurrentSlideImage;

        _screen.SetCurrentSlide(_screen.NextSlide);
        _screen.SetCurrentSlideImage(_screen.NextSlideImage);
        _screen.SetNextSlide(previousSlide);
        _screen.SetNextSlideImage(previousSlideImage);

        ApplySlideLayout(_screen.CurrentSlide);
        _screen.CurrentSlide.anchoredPosition = _slideRestPosition;
        ApplySlideLayout(_screen.NextSlide);
        _screen.NextSlide.anchoredPosition = new Vector2(_slideHiddenPosX, _slideRestPosition.y);
        _screen.NextSlide.gameObject.SetActive(false);

        ShowStage(targetStageIndex, true);
        _isAnimating = false;
        _screen.NextButton.interactable = true;
    }

    private void CacheSlideLayout()
    {
        RectTransform currentSlide = _screen.CurrentSlide;
        _slideRestPosition = currentSlide.anchoredPosition;
        _slideRestSizeDelta = currentSlide.sizeDelta;
        _slideHiddenPosX = _screen.SlideViewport.rect.width;
        ApplySlideLayout(_screen.NextSlide);
        _screen.NextSlide.anchoredPosition = new Vector2(_slideHiddenPosX, _slideRestPosition.y);
    }

    private void ApplySlideLayout(RectTransform slide)
    {
        RectTransform currentSlide = _screen.CurrentSlide;
        slide.anchorMin = currentSlide.anchorMin;
        slide.anchorMax = currentSlide.anchorMax;
        slide.pivot = currentSlide.pivot;
        slide.sizeDelta = _slideRestSizeDelta;
    }

    private void ShowStage(int stageIndex, bool animateProgress)
    {
        _currentStageIndex = stageIndex;
        TutorialStage stage = _orderedStages[stageIndex];

        ApplySlideLayout(_screen.CurrentSlide);
        _screen.CurrentSlideImage.sprite = stage.BackgroundImage;
        _screen.CurrentSlide.anchoredPosition = _slideRestPosition;
        _screen.ButtonText.text = stage.ButtonText;
        UpdateProgressBars(stageIndex);

        if (!animateProgress)
        {
            _screen.NextSlide.gameObject.SetActive(false);
        }
    }

    private void UpdateProgressBars(int activeStageIndex)
    {
        for (int index = 0; index < _screen.ProgressBars.Count; index++)
        {
            _screen.ProgressBars[index].sprite = index == activeStageIndex
                ? _screen.CurrentProgressSprite
                : _screen.DefaultProgressSprite;
        }
    }
}
