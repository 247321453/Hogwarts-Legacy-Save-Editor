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
            return int.Parse(GetMiscDataDynamicValue(db, "ExperiencePoints", "0"));
        }

        /// <summary>
        /// 获取剩余天赋点
        /// </summary>
        public static int GetTalentPoint(SQLiteHelper db)
        {
            return int.Parse(GetMiscDataDynamicValue(db, "PerkPoints", "0"));
        }

        public static string GetFirstName(SQLiteHelper db)
        {
            return GetMiscDataDynamicValue(db, "PlayerFirstName");
        }
        public static string GetLastName(SQLiteHelper db)
        {
            return GetMiscDataDynamicValue(db, "PlayerLastName");
        }

        public static void SetLastName(SQLiteHelper db, string value)
        {
            SetMiscDataDynamicValue(db, "PlayerLastName", value);
        }

        public static string GetMiscDataDynamicValue(SQLiteHelper db, string key, string def = "")
        {
            object ret = db.ExcuteScalarSQL(string.Format("select DataValue from MiscDataDynamic where DataName = '{0}';", key));
            if(ret == null)
            {
                return def;
            }
            return ret.ToString();
        }

        public static void SetMiscDataDynamicValue(SQLiteHelper db, string key, object value)
        {
            string val = value.ToString().Replace("'", "''");
            db.ExcuteSQL(string.Format("UPDATE MiscDataDynamic SET DataValue = '{0}' where DataName = '{1}';", val, key));
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
            SetMiscDataDynamicValue(db, "PerkPoints", point);
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
