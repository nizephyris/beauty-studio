using System;
using UnityEngine;

[Serializable]
public class TutorialStage
{
    [SerializeField] private int _stageIndex;
    [SerializeField] private string _buttonText;
    [SerializeField] private Sprite _backgroundImage;

    public int StageIndex => _stageIndex;
    public string ButtonText => _buttonText;
    public Sprite BackgroundImage => _backgroundImage;
}
