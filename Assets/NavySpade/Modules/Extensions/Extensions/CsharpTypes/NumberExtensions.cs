using System;
using UnityEngine;

namespace Core.Extensions.CsharpTypes
{
    public static class NumberExtensions
    {
        /// <summary>
        /// Used for effectively making a loop for a float, so that if it goes below zero it wraps around back to max, and if it goes above max it wraps around back to zero. </p>
        /// A common use is keeping degree floats between 0 and 360.
        /// </summary>
        public static float CapRange(this float value, float max)
        {
            // This is distinct from using modulus because it prevents negative values.

            while (value < 0)
                value += max;

            while (value > max)
                value -= max;

            return value;
        }

        /// <summary>
        /// Used for effectively making a loop for the number, so that if it goes below min it wraps around back to max, and if it goes above max it wraps around back to min.
        /// </summary>
        public static int CapRange(this int value, int max, int min = 0)
        {
            if (max < min)
            {
                throw new ArgumentException($"{nameof(max)} must be greater than or equal to {nameof(min)}");
            }

            if (min == max)
            {
                return min;
            }

            var diff = max - min + 1;

            while (value < min)
            {
                value += diff;
            }

            while (value > max)
            {
                value -= diff;
            }

            return value;
        }

        public static int Clamp(this int value, int minInclusive, int maxInclusive)
        {
            if (value > maxInclusive)
            {
                return maxInclusive;
            }

            return value < minInclusive ? minInclusive : value;
        }

        public static float Clamp(this float value, float minInclusive, float maxInclusive)
        {
            if (value > maxInclusive)
            {
                return maxInclusive;
            }

            return value < minInclusive ? minInclusive : value;
        }

        public static bool IsBetween(this int value, int minInclusive, int maxInclusive)
        {
            return value >= minInclusive && value <= maxInclusive;
        }

        public static bool IsBetween(this float value, float minInclusive, float maxInclusive)
        {
            return !(value < minInclusive) && !(value > maxInclusive);
        }

        public static bool IsEven(this int value) => value % 2 == 0;

        public static bool IsOdd(this int value) => !value.IsEven();

        /// <summary>
        /// This is usefully disctinct from Math.Pow because it uses integers.
        /// </summary>
        public static int ToThePowerOf(this int @base, int exponent)
        {
            // Use "case < 0:" for C# 9.0
            switch (exponent < 0 ? -1 : exponent)
            {
                case -1:
                    throw new ArgumentOutOfRangeException(nameof(exponent), "must be at least 0");
                case 0:
                    return 1;
                case 1:
                    return @base;
            }

            var result = @base;
            for (var i = 1; i < exponent; i++)
            {
                result *= @base;
            }

            return result;
        }

        /// <summary>
        /// In addition to the nicer syntax, this is significantly faster than Math.Pow
        /// because it doesn't have to account for fractional or negative exponents.
        /// </summary>
        public static float ToThePowerOf(this float @base, int exponent)
        {
            // Use "case < 0:" for C# 9.0
            switch (exponent < 0 ? -1 : exponent)
            {
                case -1:
                    throw new ArgumentOutOfRangeException(nameof(exponent), "must be at least 0");
                case 0:
                    return 1;
                case 1:
                    return @base;
            }

            var result = @base;
            for (var i = 1; i < exponent; i++)
            {
                result *= @base;
            }

            return result;
        }

        /// <summary>
        /// In addition to the nicer syntax, this is significantly faster than Math.Pow
        /// because it doesn't have to account for fractional or negative exponents.
        /// </summary>
        public static double ToThePowerOf(this double @base, int exponent)
        {
            // Use "case < 0:" for C# 9.0
            switch (exponent < 0 ? -1 : exponent)
            {
                case -1:
                    throw new ArgumentOutOfRangeException(nameof(exponent), "must be at least 0");
                case 0:
                    return 1;
                case 1:
                    return @base;
            }

            var result = @base;
            for (var i = 1; i < exponent; i++)
            {
                result *= @base;
            }

            return result;
        }

        public static bool IsPrettyCloseTo(this float number1, float number2, float margin = 0.01f)
        {
            return Math.Abs(number1 - number2) <= margin;
        }

        public static bool IsPrettyCloseTo(this double number1, double number2, double margin = 0.01)
        {
            return Math.Abs(number1 - number2) <= margin;
        }

        #region Some functions for nicer syntax

        public static float ToThePowerOf(this float @base, float exponent)
            => Mathf.Pow(@base, exponent);

        public static double ToThePowerOf(this double @base, double exponent)
            => Math.Pow(@base, exponent);


        public static float AbsoluteValue(this float value)
            => Mathf.Abs(value);

        public static double AbsoluteValue(this double value)
            => Math.Abs(value);


        public static float Sign(this float value)
            => Mathf.Sign(value);

        public static double Sign(this double value)
            => Math.Sign(value);


        public static int RoundToInt(this float value)
            => Mathf.RoundToInt(value);

        #endregion
    }
}