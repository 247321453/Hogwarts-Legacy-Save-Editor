namespace HLSE.Game
{
    public class CharacterUtil
    {
        public static int[] LevelExps = {
            0,//1
            500,
            1030,
            1595,
            2195,
            2835,
            3515,
            4240,
            5015,
            5840,
            6715,
            7650,
            8650,
            9700,
            10825,
            12025,
            13300,
            14660,
            16110,
            17650,
            19290,
            21035,
            22885,
            24865,
            26965,
            29205,
            31590,
            34130,
            36830,
            39710,
            42750,
            46000,
            49500,
            53000,
            56500,
            60000,
            63500,
            67000,
            70500,
            74000//40
        };

        public static int MAX_LEVEL = 40;

        /// <summary>
        /// 根据经验总值获取等级
        /// </summary>
        /// <param name="AllExp">经验总值</param>
        /// <returns></returns>
        public static int GetLevel(int AllExp)
        {
            for(int i = LevelExps.Length - 1; i >=0; i--)
            {
                if(AllExp >= LevelExps[i])
                {
                    return i + 1;
                }
            }
            return -1;
        }

        /// <summary>
        /// 根据等级计算天赋点
        /// </summary>
        /// <returns></returns>
        public static int GetTalentPoint(int level)
        {
            return level - 4;
        }

        /// <summary>
        /// 根据经验总值计算天赋点
        /// </summary>
        /// <returns></returns>
        public static int GetTalentPointByExp(int exp)
        {
            return GetLevel(exp) - 4;
        }
    }
}
