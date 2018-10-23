using Newtonsoft.Json;
using ProyectoPaises.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoPaises
{
    public partial class BusquedasTabPage
    {
        public ObservableCollection<object> ItemsNombre { get; set; } = new ObservableCollection<object>();
        public ObservableCollection<object> ItemsCodigo { get; set; } = new ObservableCollection<object>();
        public ObservableCollection<object> ItemsMoneda { get; set; } = new ObservableCollection<object>();

        public Command BntBuscarNombreCommando { get; set; }
        public Command BntBuscarCodigoCommando { get; set; }
        public Command BntBuscarMonedaCommando { get; set; }
        public BusquedasTabPage ()
        {
            BntBuscarNombreCommando = new Command(async () => await CargarItemsNombre());
            BntBuscarCodigoCommando = new Command(async () => await CargarItemsCodigo());
            BntBuscarMonedaCommando = new Command(async () => await CargarItemsMoneda());
            InitializeComponent();
        }

        private async Task CargarItemsNombre()
        {

            IsBusy = true;

            ItemsNombre.Clear();

            var paises = await CargarPaises();

            if(paises != null && paises?.Length > 0)
            {
                foreach (var item in paises)
                    {
                        ItemsNombre.Add(item);
                    }
            }
            else
            {
                await DisplayAlert("Error", "No se encontraron paises", "Aceptar");
            }
            IsBusy = false;
        }

        private async Task CargarItemsCodigo()
        {

            IsBusy = true;

            ItemsNombre.Clear();

            var pais = await CargarPaisesCodigo();

            if (pais != null)
            {
                lblPaisNombre.Text = pais.name;
                lblPaisRegion.Text = pais.region;
                lblPaisSubRegion.Text = pais.subregion;
                lblPaisPoblacion.Text = pais.population.ToString();
                lblPaisArea.Text = pais.area.ToString();
                if(pais.flag != null)
                {
                    Uri bandera = new Uri(pais.flag);
                    imageBandera.Source = FileImageSource.FromUri(bandera);
                }
                

            }
            else
            {
                await DisplayAlert("Error", "No se encontraron paises", "Aceptar");
            }
            IsBusy = false;
        }

        private async Task CargarItemsMoneda()
        {

            IsBusy = true;

            ItemsNombre.Clear();

            var pais = await CargarPaisesMoneda();

            if (pais != null)
            {
                lblPaisNombreM.Text = pais.name;
                lblPaisRegionM.Text = pais.region;
                lblPaisSubRegionM.Text = pais.subregion;
                lblPaisPoblacionM.Text = pais.population.ToString();
                lblPaisAreaM.Text = pais.area.ToString();
                if(pais.currencies != null)
                {
                    foreach(var item in pais.currencies)
                    {
                        ItemsMoneda.Add(item);
                    }
                }


            }
            else
            {
                await DisplayAlert("Error", "No se encontraron paises", "Aceptar");
            }
            IsBusy = false;
        }

        private async Task<Paises.ListaPaises[]> CargarPaises()
        {
            try
            {
                var cliente = new HttpClient();
                string json = "";
                cliente.BaseAddress = new Uri(App.WebServiceUrl);

                HttpResponseMessage response = await cliente.GetAsync("name/" + TxtNombre.Text);
                response.EnsureSuccessStatusCode();
                json = await response.Content.ReadAsStringAsync();


                var resultado = JsonConvert.DeserializeObject<Paises.ListaPaises[]>(json);

                return resultado;

            }
            catch (Exception ex)
            {
                // Log 
                Debug.Write(ex.ToString());
                return new Paises.ListaPaises[0];
            }
        }

        private async Task<DetallePais.PaisItem> CargarPaisesCodigo()
        {
            try
            {
                var cliente = new HttpClient();
                string json = "";
                cliente.BaseAddress = new Uri(App.WebServiceUrl);

                HttpResponseMessage response = await cliente.GetAsync("alpha/" + TxtCodigo.Text);
                response.EnsureSuccessStatusCode();
                json = await response.Content.ReadAsStringAsync();


                var resultado = JsonConvert.DeserializeObject<DetallePais.PaisItem>(json);

                return resultado;

            }
            catch (Exception ex)
            {
                // Log 
                Debug.Write(ex.ToString());
                return new DetallePais.PaisItem { };
            }
        }

        private async Task<DetallePais.PaisItem> CargarPaisesMoneda()
        {
            try
            {
                var cliente = new HttpClient();
                string json = "";
                cliente.BaseAddress = new Uri(App.WebServiceUrl);

                HttpResponseMessage response = await cliente.GetAsync("currency/" + TxtMoneda.Text);
                response.EnsureSuccessStatusCode();
                json = await response.Content.ReadAsStringAsync();


                var resultado = JsonConvert.DeserializeObject<DetallePais.PaisItem>(json);

                return resultado;

            }
            catch (Exception ex)
            {
                // Log 
                Debug.Write(ex.ToString());
                return new DetallePais.PaisItem { };
            }
        }
    }
}