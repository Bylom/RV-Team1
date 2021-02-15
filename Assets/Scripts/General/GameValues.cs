namespace General
{
    public class GameValues
    {
        private static int _unlockedMission;

        public void SetUnlockedMission(int value)
        {
            if(value > _unlockedMission)
                _unlockedMission = value;
        }
        
        public int GetUnlockedMission()
        {
            return _unlockedMission;
        }
    }
}
