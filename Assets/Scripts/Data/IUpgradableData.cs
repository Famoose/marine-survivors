namespace Data
{
    public interface IUpgradableData
    {
        string GetName();
        int GetLevel();
        void IncreaseLevel();
        int GetMaxLevel();
        bool IsMaxLevel();
    }
}