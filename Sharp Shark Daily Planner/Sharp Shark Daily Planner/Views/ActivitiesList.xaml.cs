using Sharp_Shark_Daily_Planner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sharp_Shark_Daily_Planner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitiesList : ContentPage
    {
        ActivitiesViewModel activitiesViewModel;
        public ActivitiesList()
        {
            InitializeComponent();
            activitiesViewModel = new ActivitiesViewModel();
            BindingContext = activitiesViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await activitiesViewModel.GetAllActivitiesForToday(false); //all unfinished activities
        }

        protected async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //when an item is tapped
            await Task.Delay(10);
        }
    }
}