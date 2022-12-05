namespace Data
{
    public interface IUpgradableData
    {
        string GetName();
        int GetLevel();
        string GetCurrentLevelDescription();
        string GetNextLevelDescription();
        void IncreaseLevel();
        int GetMaxLevel();
        bool IsMaxLevel();
    }
}