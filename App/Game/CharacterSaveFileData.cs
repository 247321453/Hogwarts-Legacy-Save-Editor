using SQLiteCompare.Data;
using System;
using System.Collections.Generic;
using System.IO;

namespace HLSE.Game
{
    public class CharacterSaveFileData
    {
        public string SaveFilePath { get; }
        public string TempSqliteFilePath { get; }
        public byte[] SaveFileBytes { get; }
        public int RawDbImageStartIndex { get; }
        public int DbEndOffset { get; }

        public SQLiteHelper DB { get; private set; }

        public CharacterSaveFileData(string filePath, string tempSqliteFilePath, 
            byte[] saveFileBytes, int rawDbImageStartIndex, int dbEndOffset)
        {
            this.SaveFilePath = filePath;
            this.TempSqliteFilePath = tempSqliteFilePath;
            this.SaveFileBytes = saveFileBytes;
            this.RawDbImageStartIndex = rawDbImageStartIndex;
            this.DbEndOffset = dbEndOffset;
        }

        public bool OpenSQLite()
        {
            Close();
            var db = new SQLiteHelper(TempSqliteFilePath);
            if (!db.Open())
            {
                db.Close();
                return false;
            }
            DB = db;
            return true;
        }

        public bool IsOpen()
        {
            return DB != null;
        }

        public void Close()
        {
            if(DB != null)
            {
                DB.Close();
            }
            DB = null;
        }

        public bool ResetSQLite()
        {
            Close();
            var dbSizeOffset = RawDbImageStartIndex + 61;
            var dbSize = BitConverter.ToInt32(SaveFileBytes, dbSizeOffset);
            var dbStartOffset = dbSizeOffset + 4;
            var dbEndOffset = dbStartOffset + dbSize;
            var dbBytes = new byte[dbEndOffset - dbStartOffset];
            Array.Copy(SaveFileBytes, dbStartOffset, dbBytes, 0, dbBytes.Length);
            File.WriteAllBytes(TempSqliteFilePath, dbBytes);
            return OpenSQLite();
        }

        public override bool Equals(object obj)
        {
            return obj is CharacterSaveFileData data &&
                   SaveFilePath == data.SaveFilePath &&
                   TempSqliteFilePath == data.TempSqliteFilePath &&
                   EqualityComparer<byte[]>.Default.Equals(SaveFileBytes, data.SaveFileBytes) &&
                   RawDbImageStartIndex == data.RawDbImageStartIndex &&
                   DbEndOffset == data.DbEndOffset;
        }

        public override int GetHashCode()
        {
            int hashCode = -160357670;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SaveFilePath);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TempSqliteFilePath);
            hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(SaveFileBytes);
            hashCode = hashCode * -1521134295 + RawDbImageStartIndex.GetHashCode();
            hashCode = hashCode * -1521134295 + DbEndOffset.GetHashCode();
            return hashCode;
        }
    }
}
