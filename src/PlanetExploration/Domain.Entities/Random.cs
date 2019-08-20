namespace Domain.Entities
{
    public static class Random
    {
        public static int GetRandom(int min, int max)
        {
            return new System.Random().Next(min, max + 1);
        }

        public static bool GetRandom()
        {
            var random = GetRandom(1, 100);

            return random < 50;
        }

        public static bool GetRandom(int trueWeight)
        {
            var random = GetRandom(1, 100);

            if (random < trueWeight) return true;

            return false;
        }
    }
}
