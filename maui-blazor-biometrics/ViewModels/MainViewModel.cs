using Plugin.Fingerprint.Abstractions; 
using System.ComponentModel; 
using System.Runtime.CompilerServices; 
using System.Windows.Input;

namespace maui_blazor_biometrics.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IFingerprint _fingerprint;
         
        string _title;
       
         
        public MainViewModel(IFingerprint fingerprint)
        {
            _fingerprint = fingerprint;

            FingerprintLoginAsync().GetAwaiter().GetResult();

            _title = "Hello World 👋";

            Console.WriteLine("MainViewModel created: " + this.GetHashCode());
            Registrations.ViewModelRegistrations.Add(DateTime.Now + " HashCode: " + this.GetHashCode());
            FingerprintCommand = new Command(async () => await FingerprintLoginAsync());
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }
 
        public ICommand FingerprintCommand { get; }

        async Task FingerprintLoginAsync()
        {
            var isBiometricsAvailable = await _fingerprint.IsAvailableAsync();

            if (isBiometricsAvailable)
            {
                var dialogConfig = new AuthenticationRequestConfiguration
                ("Login using biometrics", "Confirm login with your biometrics")
                {
                    FallbackTitle = "Use Password",
                    AllowAlternativeAuthentication = true,
                };

                var result = await _fingerprint.AuthenticateAsync(dialogConfig);

                if (result.Authenticated)
                {
                    //grant access
                }
                else
                {
                    //deny access
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
