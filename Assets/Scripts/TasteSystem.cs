namespace TasteSystem {
    
    // All of elemental is this games
    public enum Taste { BLAND = 0, SOUR = 1, SALTY = 2, FRESHY = 3, SPICY = 4, SWEET = 5, BITTER = 6 };


    /*
        *** Overview of this class ***
            เป็น class ที่ทำการเก็บ table การชนะ และแพ้ทางธาตู โดยมี method IsWin, IsLose ไว้เรียกใช้ได้ว่า
            ธาตูใด ๆ ชนะทาง หรือแพ้ทางธาตูไหน
    */
    public class TasteChart {
        private const int TasteNumber = 7;

        /* 
            the order of taste index is like the order in Taste enum
            true mean the taste index that row win against taste in that column
        */
        private static bool[,] table = new bool[TasteNumber, TasteNumber]{
            {false, false, false, false, false, false, false},
            {false, false, true,  false, false, false, false},
            {false, false, false, true,  false, false,  false},
            {false, false, false, false, true,  false, false},
            {false, false, false, false, false, true,  false},
            {false, false, false, false, false, false, true},
            {false, true,  false, false, false, false, false}
        };

        // Check if the selectedTaste wins against the targetTaste
        public static bool IsWin(Taste selectedTaste, Taste targetTaste) {
            return table[(int) selectedTaste, (int) targetTaste];
        }

        // Check if the selectedTaste loses against the targetTaste
        public static bool IsLose(Taste selectedTaste, Taste targetTaste) {
            return table[(int) targetTaste, (int) selectedTaste];
        }
    }
}
