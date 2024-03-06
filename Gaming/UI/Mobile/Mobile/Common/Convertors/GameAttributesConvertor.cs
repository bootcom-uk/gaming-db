using System.Globalization;
using System.Text;

namespace Mobile.Common.Convertors
{
    public class GameAttributesConvertor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values.All(record => record == null)) return null;

            var hasGame = (values[0] as bool?) ?? false;
            var hasManual = (values[1] as bool?) ?? false;
            var hasCase = (values[2] as bool?) ?? false;
            var isCollectorsEdition = (values[3] as bool?) ?? false;
            var isCopy = (values[4] as bool?) ?? false;
            var isNonPalRegion = (values[5] as bool?) ?? false;

            var output = new StringBuilder();

            if(hasGame && hasManual && hasCase) output.Append("Complete in box");
            if (hasGame && !hasManual && !hasCase) output.Append("Game only");
            if (hasGame && hasManual && !hasCase) output.Append("Game and manual");
            if (hasGame && !hasManual && hasCase) output.Append("Game and case");

            if (isCollectorsEdition) output.Append(", collectors edition");
            if (isCopy) output.Append(", non-genuine game");
            if (isNonPalRegion) output.Append(", non-PAL region game");

            return output.ToString();

        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
