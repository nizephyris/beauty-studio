using System;
using UnityEngine;

public class MakeupStageIndicatorView : MonoBehaviour
{
    [SerializeField] private MakeupStageIconSlot[] _stageIcons;

    private Action<int> _onStageSelected;

    public void Bind(Action<int> onStageSelected)
    {
        _onStageSelected = onStageSelected;

        for (int index = 0; index < _stageIcons.Length; index++)
        {
            int stageIndex = index;
            _stageIcons[index].Button.onClick.AddListener(() => _onStageSelected(stageIndex));
        }
    }

    public void SetActiveStage(int stageIndex)
    {
        for (int index = 0; index < _stageIcons.Length; index++)
        {
            if (index == stageIndex)
            {
                _stageIcons[index].SetCurrent();
            }
            else
            {
                _stageIcons[index].SetDefault();
            }
        }
    }
}
