using System.Collections.Generic;

namespace HLSE.Game
{
    public class CharacterSaveFileData
    {
        public string SaveFilePath { get; }
        public string TempSqliteFilePath { get; }
        public byte[] SaveFileBytes { get; }
        public int RawDbImageStartIndex { get; }
        public int DbEndOffset { get; }

        public CharacterSaveFileData(string filePath, string tempSqliteFilePath, 
            byte[] saveFileBytes, int rawDbImageStartIndex, int dbEndOffset)
        {
            this.SaveFilePath = filePath;
            this.TempSqliteFilePath = tempSqliteFilePath;
            this.SaveFileBytes = saveFileBytes;
            this.RawDbImageStartIndex = rawDbImageStartIndex;
            this.DbEndOffset = dbEndOffset;
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
