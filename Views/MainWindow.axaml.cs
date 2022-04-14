using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Gallery.ViewModels;

namespace Gallery.Views
{
    public partial class MainWindow : Window
    {
        private Carousel _carouselImages;
        private Button _leftButton;
        private Button _rightButton;

        public MainWindow()
        {
            this.InitializeComponent();

            _leftButton.Click += (s, e) => _carouselImages.Previous();
            _rightButton.Click += (s, e) => _carouselImages.Next();

            _leftButton.IsEnabled = false;
            _rightButton.IsEnabled = false;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _carouselImages = this.FindControl<Carousel>("CarouselImages");
            _leftButton = this.FindControl<Button>("leftButton");
            _rightButton = this.FindControl<Button>("rightButton");
        }

        private void OnTreeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;

            context.UpdateShowingImages(e.AddedItems[0]);

            try
            {
                if (context.ShowingImages.Count < 2)
                {
                    _leftButton.IsEnabled = false;
                    _rightButton.IsEnabled = false;
                }
                else
                {
                    _leftButton.IsEnabled = true;
                    _rightButton.IsEnabled = true;
                }
            }
            catch
            {

            }
        }
    }
}
