using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace MasterDetail
{
    public partial class MainPage : ReactiveMasterDetailPage<MainViewModel>
    {
        public MainPage(MainViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
            // I used this method to show the Menu Button on iOS when navigating,
            // but it still does not show the Menu when navigating from side menu on iOS
            (Detail as NavigationPage).PoppedToRoot += MainPage_PoppedToRoot;
            
            this.WhenActivated(
                disposables =>
                {
                    this
                        .OneWayBind(ViewModel, vm => vm.MenuItems, v => v.MyListView.ItemsSource)
                        .DisposeWith(disposables);
                    this
                        .Bind(ViewModel, vm => vm.Selected, v => v.MyListView.SelectedItem)
                        .DisposeWith(disposables);
                    this
                        .WhenAnyValue(x => x.ViewModel.Selected)
                        .Where(x => x != null)
                        .Subscribe(
                            _ =>
                            {
                                // Deselect the cell.
                                MyListView.SelectedItem = null;
                                // Hide the master panel.
                                IsPresented = false;
                            })
                        .DisposeWith(disposables);
                });
        }
                
        private void MainPage_PoppedToRoot(object sender, NavigationEventArgs e)
        {
            Icon = (FileImageSource)ImageSource.FromFile("hamburger.png");
            Detail.Icon = (FileImageSource)ImageSource.FromFile("hamburger.png");
            
            foreach (var item in Detail.Navigation.NavigationStack)
            {
                item.Icon = (FileImageSource)ImageSource.FromFile("hamburger.png");
            }
            foreach (var item in Navigation.NavigationStack)
            {
                item.Icon = (FileImageSource)ImageSource.FromFile("hamburger.png");
            }
            OnPropertyChanged("Icon");
        }
    }
}
