using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BattleShips
{
    public static class CoordinatesReader
    {
        public static bool IsValidCoordinate(string coord)
        {
            return Regex.IsMatch(coord, @"^[A-Ja-j]\d\z") || Regex.IsMatch(coord, @"^[A-Ja-j]10\z");
        }

        public static bool IsValidCoordinatesPair(string coords)
        {
            var pair = coords.Split(' ');
            if (pair.Length != 2)
                return false;
            return IsValidCoordinate(pair[0]) && IsValidCoordinate(pair[1]);
        }

        public static (int X, int Y) ConvertToCoordinates(string coord)
        {
            int x = int.Parse((coord.ToLower().ToCharArray()[0] - 'a').ToString());
            int y = int.Parse(coord.Substring(1)) - 1;
            return (x, y);
        }

        public static (int X1, int Y1, int X2, int Y2) ConvertToPairOfCoordinates(string coords)
        {
            int x1, y1, x2, y2;
            var pair = coords.Split(' ');
            (x1, y1) = ConvertToCoordinates(pair[0]);
            (x2, y2) = ConvertToCoordinates(pair[1]);
            return (x1, y1, x2, y2);
        }
    }
}
