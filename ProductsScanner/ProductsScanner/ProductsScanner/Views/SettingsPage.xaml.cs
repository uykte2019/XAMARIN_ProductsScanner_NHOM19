using ProductsScanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductsScanner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
        private SettingsViewModel viewModel;

		public SettingsPage ()
		{
			InitializeComponent ();
            this.Title = "Settings";
            this.viewModel = new SettingsViewModel();
            this.BindingContext = viewModel;

        }
        private void SwitchOpenURLsDirectly_Toggled(object sender, ToggledEventArgs e)
        {
            this.viewModel.SettingOpenURLsDirectlyCommand.Execute(e.Value);
        }

        private void SwitchSaveToHistory_Toggled(object sender, ToggledEventArgs e)
        {
            this.viewModel.SettingSaveToHistoryCommand.Execute(e.Value);
        }
    }
}