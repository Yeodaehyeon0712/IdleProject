
#region Actor
public enum eActorType:uint
{
    Character,
    Enemy,
}
public enum eActorState
{
    None,
    Spawn,
    Active,
    Death,
    Clean,
}
public enum eComponent
{
    SkinComponent,
    ControllerComponent,
    StatComponent,
    SkillComponent,
}
public enum eCharacterAnimState
{
    Idle ,
    Move ,
    Hit ,
    Death ,
}
public enum eSkillState
{
    None,
    Using,
    Cooltime,
    Inactive,
}
public enum eStatusType
{
    MaxHP,
    Recovery,
    Armor,
    MoveSpeed,
    Might,
    ProjectileSpeed,
    Duration,
    Area,
    CoolTime,
    Amount,
    Revival,
    Magnet,
    Luck,
    Growth,
    Greed,
    Curse,
}
public enum eCalculateType
{
    Flat,
    Percentage
}
public enum eModifierType
{
    Increase,
    Decrease
}
public enum eAttachmentTarget
{
    OverHead,    
    End,
}
#endregion

#region Data
[System.Flags]
public enum eTableName
{
    LocalizingTable = 1 << 0,
    StageTable=1<<1,
    All = ~0,
}

enum eAddressableState
{
    None,
    Initialized,
    FindPatch,
    DownloadDependencies,
    LoadMemory,
    TableMemory,
    Complete,
}
#endregion

#region Localizing
public enum eLanguage
{
    EN,     //English
    KR,     //Korean
    End
}
#endregion

#region Effect
public enum eEffectType
{
    Projectile,
    Crash,
    Shape,//To Do :추후 더 좋은 이름으로 변경 하자 
}
public enum eEffectChainCondition
{
    None,
    Enable,
    Disable,
    Overlap,
}
[System.Flags]
public enum eEffectAttribute
{
    Duration = 1 << 0,
    Velocity = 1 << 1,
    Overlap = 1 << 2,
    PostEffect = 1 << 3,
}
public enum ePostEffectType
{
    None,
    KnockBack,
    Stun
}
#endregion

#region Manager
#endregion

#region Pattern
#endregion

#region Scene
#endregion

#region Stage
public enum eContentsType
{
    Normal,
    End,
}
public enum eStageType
{
    Race,
    Boss,
    Loop
}
public enum eStageFrameworkState
{
    None,
    SetUp,
    InProgress,
    Victory,
    Defeat,
    Clean,
}
#endregion

#region System
#region Background
#endregion

#region Camera
public enum eCameraType
{
    MainCamera,
    UICamera,
}
public enum eCameraFadeType
{
    BandWipe,
    BarnDoorOpenVertical,
    BarnDoorStretch,
    BattleWipe,
    BattleWipeExtreme,
    Blur,
    Fade,
    Rubicks,
    RubicksHorizontal,
    RubicksVertical,
    Zoom,
    ZoomCustom,
    ZoomSmall,
    WhiteFade,
    End,
}
#endregion

#region Reposition
#endregion

#region Preference
public enum ePreference
{
    BGM,
    SFX,
    Alram,
    JoyStick,
    Vibration,
    Effect
}
#endregion
#endregion

#region Item
public enum eItemType
{
    Gem,
    Heart,
}
#endregion

#region UI
[System.Flags]//31 is Maximum Value
public enum eUI
{
    MenuButton=1<<0,
    Main=1<<1,
    PlayerInfo=1<<2,
    Stage=1<<3,
}
public enum eMovableUIDir
{
    None,
    LeftToRight,
    RightToLeft,
    TopToBottom,
    BottomToTop
}
enum LetterBoxDirection
{
    Top,
    Bottom,
}
public enum eFieldUI
{
    HPBar,
    DamageText,
}
public enum eMainUI
{
    Status,
    Item,
    Skill,
    Partner,
    Store,
}
#endregion