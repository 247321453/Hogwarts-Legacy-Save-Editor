using HLSE.Game;
using System;

namespace SQLiteTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = CharacterSaveFile.Read(@"D:\HL-00-01.sav");
            if (data != null)
            {
                CharacterSaveFile.Save(data, @"D:\1.sav");
            }
            int start = 2;
            int end = 8;
            int c1 = 0;
            for(int i = start; i < end; i++)
            {
                c1++;
            }
            int c2 = end - start;
            Console.WriteLine("c1=" + c1 + ", c2=" + c2);
            //select DataValue from MiscDataDynamic where DataName like '%ExperiencePoints%';
            //PerkDynamic
            //select DataValue from MiscDataDynamic where DataName like '%PerkPoints%';
        }
    }
}
