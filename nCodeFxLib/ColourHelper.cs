using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media;

namespace nCodeFxLib
{
    /// <summary>
    /// Provides an IEnumerable for the Colour Names in the Windows.Media Namespace.
    /// </summary>
    /// <remarks>This class allows you to create an ObjectDataProvider for binding controls to the colour names 
    /// <example>
    /// <code>
    /// &lt;Window.Resources&gt;
    ///     &lt;ObjectDataProvider x:Key="colors"
    ///            ObjectType="{x:Type nCodeFxLib:ColorHelper}"
    ///            MethodName="GetColorNames"/&gt;
    /// &lt;/Window.Resource&gt;
    /// </code>
    /// 
    /// Then bind to the ObjectDataProvider in an appropriate ItemsControl, eg a ComboBox.
    /// 
    /// <code>
    ///     &lt;ComboBox x:Name="colorChoice" VerticalAlignment="Center"
    ///         ItemsSource="{Binding Mode=OneWay, Source={StaticResource colors}}" SelectedIndex="0"&gt;
    ///         &lt;ComboBox.ItemTemplate&gt;
    ///             &lt;DataTemplate&gt;
    ///                 &lt;StackPanel Margin="1" Orientation="Horizontal" &gt;
    ///                     &lt;Rectangle Fill="{Binding}" Height="16" Width="16"
    ///                         Margin="2"/&gt;
    ///                     &lt;TextBlock Text="{Binding}" VerticalAlignment="Center" Margin="2 0 0 0"/&gt;
    ///                 &lt;/StackPanel&gt;
    ///             &lt;/DataTemplate&gt;
    ///         &lt;/ComboBox.ItemTemplate&gt;
    ///     &lt;/ComboBox&gt;
    /// </code>
    /// 
    /// </example>
    /// 
    /// </remarks>
    public static class ColorHelper
    {
        /// <summary>
        /// Provides an IEnumerable for easy Binding of colour names in XAML
        /// </summary>
        /// <returns>an IEnumerable for the colour names in Windows.Media.Colours</returns>
        public static IEnumerable<string> GetColorNames()
        {
            foreach (PropertyInfo p
              in typeof(Colors).GetProperties(
              BindingFlags.Public | BindingFlags.Static))
            {
                yield return p.Name;
            }
        }
    }
}
