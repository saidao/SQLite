using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SQLite
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            Padding = Device.OnPlatform(
                new Thickness(10, 20, 10, 10),
                new Thickness(10, 10, 10, 10),
                new Thickness(10, 10, 10, 10));

            listaListView.ItemTemplate = new DataTemplate(typeof(EmpleadoCell));
            listaListView.RowHeight = 70;

            agregarButton.Clicked += AgregarButton_Clicked;
            listaListView.ItemSelected += ListaListView_ItemSelected;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (var datos = new DataAccess())
            {
                listaListView.ItemsSource = datos.GetEmpleados();
            }

        }

        private async void ListaListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EditPage((Empleado)e.SelectedItem));
        }

        private async void AgregarButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nombresEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar nombres", "Aceptar");
                nombresEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(apellidosEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar apellidos", "Aceptar");
                apellidosEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(salarioEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar salario", "Aceptar");
                salarioEntry.Focus();
                return;
            }

            var empleado = new Empleado
            {
                Nombres = nombresEntry.Text,
                Apellidos = apellidosEntry.Text,
                FechaContrato = fechaContratoDatePicker.Date,
                Salario = decimal.Parse(salarioEntry.Text),
                Activo = activoSwitch.IsToggled
            };

            using (var datos = new DataAccess())
            {
                datos.InsertEmpleado(empleado);
                listaListView.ItemsSource = datos.GetEmpleados();
            }

            nombresEntry.Text = string.Empty;
            apellidosEntry.Text = string.Empty;
            salarioEntry.Text = string.Empty;
            fechaContratoDatePicker.Date = DateTime.Now;
            activoSwitch.IsToggled = true;
            await DisplayAlert("Confirmación", "Empleado agregado", "Aceptar");
        }
    }
}
