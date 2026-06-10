using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MakeupCatalog", menuName = "BeautyWorkshop/Makeup Catalog")]
public class MakeupCatalogConfig : ScriptableObject
{
    [SerializeField] private string _playerDisplayName;
    [SerializeField] private List<string> _botDisplayNames;
    [SerializeField] private List<MakeupStageEntry> _stages;
    [SerializeField] private List<CouponEntry> _coupons;
    [SerializeField] private Color _defaultBackgroundColor = Color.white;
    [SerializeField] private int _makeupDurationSeconds = 150;
    [SerializeField] private List<MakeupThemeEntry> _makeupThemes;

    public string PlayerDisplayName => _playerDisplayName;

    public IReadOnlyList<string> BotDisplayNames => _botDisplayNames;

    public IReadOnlyList<MakeupStageEntry> Stages => _stages;

    public IReadOnlyList<CouponEntry> Coupons => _coupons;

    public Color DefaultBackgroundColor => _defaultBackgroundColor;

    public int MakeupDurationSeconds => _makeupDurationSeconds;

    public IReadOnlyList<MakeupThemeEntry> MakeupThemes => _makeupThemes;

    public Color GetBackgroundColorFromSnapshot(BotMakeupSnapshot snapshot)
    {
        for (int stageIndex = 0; stageIndex < _stages.Count; stageIndex++)
        {
            int optionIndex = snapshot.OptionIndicesByStage[stageIndex];

            if (optionIndex == BotMakeupSnapshot.NoSelectionOptionIndex)
            {
                continue;
            }

            MakeupStageEntry stage = _stages[stageIndex];

            if (stage.Type != MakeupType.Hair)
            {
                continue;
            }

            return stage.ColorOptions[optionIndex].BackgroundColor;
        }

        return _defaultBackgroundColor;
    }
}
