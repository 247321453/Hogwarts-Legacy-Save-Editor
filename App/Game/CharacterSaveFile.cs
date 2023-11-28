using System;
using System.Collections.Generic;
using System.IO;

namespace HLSE.Game
{
    public class CharacterSaveFile
    {
        private static int FindArray(byte[] source, byte[] pattern, int start = 0)
        {

            for (int i = start; i < source.Length; i++)
            {
                if (IsMatch(source, i, pattern))
                {
                    return i;
                }
            }
            return -1;
        }

        private static readonly int[] Empty = new int[0];

        private static bool IsMatch(byte[] array, int position, byte[] candidate)
        {
            if (candidate.Length > (array.Length - position))
            {
                return false;
            }

            for (int i = 0; i < candidate.Length; i++)
            {
                if (array[position + i] != candidate[i])
                {
                    return false;
                }
            }

            return true;
        }

        //52 61 77 44
        //61 74 61 62
        //61 73 65 49
        //6D 61 67 65
        private static byte[] RAW_DB_IMAGE_BYTES = {
            0x52, 0x61, 0x77, 0x44,
            0x61, 0x74, 0x61, 0x62,
            0x61, 0x73, 0x65, 0x49,
            0x6D, 0x61, 0x67, 0x65
        };

        public static CharacterSaveFileData Read(string saveFile, string tempDir = "./temp")
        {
            if (File.Exists(saveFile))
            {
                var bytes = File.ReadAllBytes(saveFile);

                var rawDbImageStartIndex = FindArray(bytes, RAW_DB_IMAGE_BYTES);
                if(rawDbImageStartIndex < 0)
                {
                    Console.WriteLine("not foudn sqlite head:" + rawDbImageStartIndex +" not in "+ bytes.Length);
                    return null;
                }
               // Console.WriteLine("rawDbImageStartIndex=" + rawDbImageStartIndex.ToString("X"));
                var dbSizeOffset = rawDbImageStartIndex + 61;
                var dbSize = BitConverter.ToInt32(bytes, dbSizeOffset);
                var dbStartOffset = dbSizeOffset + 4;
                var dbEndOffset = dbStartOffset + dbSize;
                string dbFile = Path.Combine(tempDir, Path.GetFileNameWithoutExtension(saveFile) + ".db3");
                var dbBytes = new byte[dbEndOffset - dbStartOffset];
                Array.Copy(bytes, dbStartOffset, dbBytes, 0, dbBytes.Length);
                if (!Directory.Exists(tempDir))
                {
                    Directory.CreateDirectory(tempDir);
                }
                //dbStartOffset, dbEndOffset
                File.WriteAllBytes(dbFile, dbBytes);
                return new CharacterSaveFileData(saveFile, dbFile, bytes, rawDbImageStartIndex, dbEndOffset);
            }
            return null;
        }

        public static bool Save(CharacterSaveFileData data, string file = null) {
            if(data == null)
            {
                Console.WriteLine("data is empty");
                return false;
            }
            if (file == null)
            {
                file = data.SaveFilePath;
            }
            string bak = data.SaveFilePath + ".bak";
            File.Copy(data.SaveFilePath, bak, true);
            using (var fs = File.OpenWrite(file))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    // Put original save file initial padding
                    writer.Write(data.SaveFileBytes, 0, data.RawDbImageStartIndex + 35);

                    var tempSqliteBytes = File.ReadAllBytes(data.TempSqliteFilePath);
                    // Write sqlite size + 4 in little endian
                    writer.Write(BitConverter.GetBytes(tempSqliteBytes.Length + 4));
                    // Write padding bytes from original file
                    writer.Write(data.SaveFileBytes, data.RawDbImageStartIndex + 39, 61 - 39);
                    // Write sqlite size in little endian
                    writer.Write(BitConverter.GetBytes(tempSqliteBytes.Length));
                    writer.Write(tempSqliteBytes);
                    // Save end padding
                    writer.Write(data.SaveFileBytes, data.DbEndOffset, data.SaveFileBytes.Length - data.DbEndOffset);
                }
            }
            return File.Exists(bak);
        }
    }
}
