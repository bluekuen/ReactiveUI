using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LetterStreamPage : ReactiveContentPage<LetterStreamViewModel>
    {
        public LetterStreamPage()
        {
			InitializeComponent();

            this.WhenActivated(
                disposables =>
                {
                    this
                        .OneWayBind(ViewModel, vm => vm.CurrentLetter, v => v.LetterLabel.Text)
                        .DisposeWith(disposables);
                         
                    Disposable
                       .Create(() => handleDeactivation())
                       .DisposeWith(disposables);
                });                
        }
        private void handleDeactivation()
        {
        //ViewModel here is null

           if (ViewModel != null)
           {
               Debug.WriteLine("ViewModel Property is null here");
               //ViewModel
           }
       }
    }
}
