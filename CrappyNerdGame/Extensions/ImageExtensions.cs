using System.Windows.Controls;
using System.Windows.Media;

namespace CrappyNerdGame.Extensions;

 public static class ImageExtensions
 {
     public static Image Clone(this Image image, Stretch stretch = Stretch.Fill) =>
         new()
         {
             Source = image.Source,
             Width = image.Width,
             Height = image.Height,
             Stretch = stretch
         };
 }