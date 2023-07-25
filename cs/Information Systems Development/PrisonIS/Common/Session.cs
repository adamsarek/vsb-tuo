namespace Common
{
    public static class Session
    {
        public enum Storage { SQL, XML }

        public static Storage DataStorage
        {
            get
            {
                return Storage.SQL;
            }
        }
    }
}
