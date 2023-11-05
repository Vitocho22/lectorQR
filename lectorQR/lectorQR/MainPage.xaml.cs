using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace lectorQR
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_clicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushModalAsync(scan);

            scan.OnScanResult += async (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopModalAsync();

                    // Verificar si el resultado del escaneo es un enlace web
                    if (Uri.TryCreate(result.Text, UriKind.Absolute, out Uri uriResult)
                        && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                    {
                        // Abrir el enlace en el navegador del dispositivo
                        Device.OpenUri(uriResult);
                    }
                    else
                    {
                        // Mostrar una alerta si el escaneo no es un enlace web
                        await DisplayAlert("Valor QRCODE", result.Text, "OK");
                    }
                });
            };
        }
    }
}