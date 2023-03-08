using CarListApp.Models;
using CarListApp.Services;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Google.Crypto.Tink.Proto;

namespace CarListApp.ViewModels
{
    public partial class CarListViewModel : BaseViewModel
    {
        private readonly CarService _carService;
        public ObservableCollection<Car> Cars { get; private set; } = new ();
        public CarListViewModel(CarService carService)
        {
            Title = "Car List";
            this._carService = carService;
        }

        [RelayCommand]
        async Task GetCarList()
        {
            if (IsLoading) return;
            try
            {
                IsLoading= true;
                if (Cars.Any()) Cars.Clear();

                var cars = _carService.GetCars();
                foreach (var car in cars)
                {
                    Cars.Add(car);
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get cars: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to retrive list of cars.", "Ok");
            }
            finally
            {
                IsLoading= false;
            }
            
        }
    }
}
