using Syncfusion.Maui.ListView;
using System.Globalization;

namespace Mobile.Common.Convertors
{
    public class ListViewItemDoubleTappedConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = (value as ItemDoubleTappedEventArgs);
            return eventArgs!.DataItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
