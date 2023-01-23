namespace NavySpade.pj77.Extension{
    internal static partial class NumbersExtension{
        public static int GetNormalHalf(ref this int delta){
            if (delta == 1)
                return 1;
            if (delta % 17 == 0){
                delta /= 17;
                return delta;
            }
            if (delta % 13 == 0){
                delta /= 13;
                return delta;
            }
            if (delta % 11 == 0){
                delta /= 11;
                return delta;
            }
            if (delta % 7 == 0){
                delta /= 7;
                return delta;
            }
            if (delta % 5 == 0){
                delta /= 5;
                return delta;
            }
            if (delta % 3 == 0){
                delta /= 3;
                return delta;
            }
            if (delta % 2 == 0){
                delta /= 2;
                return delta;
            }

            return delta;
        }
    }
}