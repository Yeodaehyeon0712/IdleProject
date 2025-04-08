public static class Player 
{
    #region Fields
    //public static IngameDataProperty InGameData => ingameDataProperty;
    //static IngameDataProperty ingameDataProperty;
    public static bool IsLoad = false;
    public static Character PlayerCharacter;
    static System.Random _random = new System.Random();
    #endregion

    #region Init Method
    public static void Initialize()
    {
        //ingameDataProperty = new IngameDataProperty();       
        IsLoad = true;
    }
    public static void RegisterPlayer(Character actor)
    {
        //Player
        PlayerCharacter = actor;
        //ingameDataProperty.InitializeData();
    }
    public static void ActivePlayer()
    {
        PlayerCharacter.FSM.State=eFSMState.Move;

    }
    public static void UnRegisterPlayer()
    {
        PlayerCharacter = null;     
    }
    #endregion

    public static double ComputeCritical(out bool isCritical)
    {
        isCritical = false;
        double criticalProbability = PlayerCharacter.Status.GetStatus(eStatusType.CriticalChance);
        if (criticalProbability < 1f)
        {
            if (criticalProbability > _random.NextDouble())
                isCritical = true;
        }
        else
            isCritical = true;

        return isCritical ? PlayerCharacter.Status.GetStatus(eStatusType.CriticalDamage) : 1f;
    }
}
