using Common.Data;
using SQLiteCompare.Data;

namespace HLSE.Game
{
    public class GameFunUtil
    {

        /// <summary>
        /// 获取玩家经验总值
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int GetAllExp(SQLiteHelper db)
        {
            return int.Parse(db.ExcuteScalarSQL("select DataValue from MiscDataDynamic where DataName = 'ExperiencePoints';").ToString());
        }

        /// <summary>
        /// 获取剩余天赋点
        /// </summary>
        public static int GetTalentPoint(SQLiteHelper db)
        {
            return int.Parse(db.ExcuteScalarSQL("select DataValue from MiscDataDynamic where DataName = 'PerkPoints';").ToString());
        }

        /// <summary>
        /// 获取剩余天赋点
        /// </summary>
        public static void SetTalentPoint(SQLiteHelper db, int point)
        {
            if (point < 0)
            {
                point = 0;
            }
            int max = CharacterUtil.GetTalentPoint(CharacterUtil.MAX_LEVEL);
            if (point > max)
            {
                point = max;
            }
            db.ExcuteSQL(string.Format("UPDATE MiscDataDynamic SET DataValue = {0} where DataName = 'PerkPoints';", point));
        }

        /// <summary>
        /// 重置天赋
        /// </summary>
        public static void ResetTalent(SQLiteHelper db)
        {
            db.ExcuteSQL("DELETE FROM PerkDynamic;");
            int exp = GetAllExp(db);
            int point = CharacterUtil.GetTalentPointByExp(exp);
            SetTalentPoint(db, point);
        }


    }
}
