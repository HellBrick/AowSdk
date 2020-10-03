using System.Windows;
using System.Windows.Controls;

namespace Utils.WPF
{
	public class ContentResourceHelper: DependencyObject
	{
		public static readonly DependencyProperty ContentKeyProperty = DependencyProperty.RegisterAttached(
			   "ContentKey",
			   typeof( string ),
			   typeof( ContentResourceHelper ),
			   new PropertyMetadata( ( s, e ) => OnPropertyChanged( s, e ) ) );

		public static object GetContentKey( DependencyObject obj ) => obj.GetValue( ContentKeyProperty );

		public static void SetContentKey( DependencyObject obj, object value ) => obj.SetValue( ContentKeyProperty, value );

		private static void OnPropertyChanged( DependencyObject s, DependencyPropertyChangedEventArgs e )
		{
			if ( e.NewValue != null )
			{
				object key = e.NewValue;
				ContentControl contentPresenter = s as ContentControl;

				if ( contentPresenter != null && key != null )
				{
					object content = contentPresenter.TryFindResource( key );
					if ( content != null )
						contentPresenter.Content = content;
				}
			}
		}
	}
}
