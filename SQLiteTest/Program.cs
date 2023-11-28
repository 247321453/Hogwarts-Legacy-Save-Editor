using HLSE.Game;
using System;

namespace SQLiteTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = CharacterSaveFile.Read(@"D:\HL-00-03.sav");
            if (data != null)
            {
                CharacterSaveFile.Save(data, @"D:\3.sav");
            }
            //select DataValue from MiscDataDynamic where DataName like '%ExperiencePoints%';
            //PerkDynamic
            //select DataValue from MiscDataDynamic where DataName like '%PerkPoints%';
        }
    }
}
