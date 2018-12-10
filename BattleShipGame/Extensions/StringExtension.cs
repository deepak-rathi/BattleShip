namespace BattleShipGame.Extensions
{
    internal static class StringExtension
    {
        public static char FirstChar(this string stringValue)
        {
            return stringValue[0];
        }

        public static char FirstCharUpper(this string stringValue)
        {
            return char.ToUpper(stringValue.FirstChar());
        }

        public static string ReplaceUnderscoreWithBlankSpace(this string stringValue)
        {
            return stringValue.Replace('_', ' ');
        }
    }
}