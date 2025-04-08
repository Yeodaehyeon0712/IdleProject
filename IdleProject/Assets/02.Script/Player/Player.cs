public static class Player 
{
    #region Fields
    //public static IngameDataProperty InGameData => ingameDataProperty;
    //static IngameDataProperty ingameDataProperty;
    public static bool IsLoad = false;
    public static Character PlayerCharacter;

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
}
