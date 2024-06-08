using maui_blazor_biometrics.ViewModels;
using Microsoft.AspNetCore.Components;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace maui_blazor_biometrics.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [CascadingParameter] public INotifyPropertyChanged? ViewModel { get; set; }

        MainViewModel? _mainViewModel => ViewModel as MainViewModel;

        protected override void OnInitialized()
        {
            _mainViewModel!.PropertyChanged += MainViewModel_PropertyChanged!; 
            base.OnInitialized();
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.Title))
            {
                InvokeAsync(() => StateHasChanged());
            }
        }

        private async Task Login()
        {
            var isAvailable = await CrossFingerprint.Current.IsAvailableAsync();

            if (isAvailable)
            {
                var request = new AuthenticationRequestConfiguration
                ("Login using biometrics", "Confirm login with your biometrics");

                var result = await CrossFingerprint.Current.AuthenticateAsync(request);

                if (result.Authenticated)
                {
                    await App.Current!.MainPage!.DisplayAlert("Authenticated!", "Access granted", "OK");
                }
                else
                {
                    await App.Current!.MainPage!.DisplayAlert("Not authenticated!", "Access denied", "OK");
                }

            }
        }
    }
}
