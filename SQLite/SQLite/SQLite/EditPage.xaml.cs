using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SQLite
{
    public partial class EditPage : ContentPage
    {
        private Empleado empleado;

        public EditPage(Empleado empleado)
        {
            InitializeComponent();

            this.empleado = empleado;

            Padding = Device.OnPlatform(
                new Thickness(10, 20, 10, 10),
                new Thickness(10, 10, 10, 10),
                new Thickness(10, 10, 10, 10));

            nombresEntry.Text = empleado.Nombres;
            apellidosEntry.Text = empleado.Apellidos;
            fechaContratoDatePicker.Date = empleado.FechaContrato;
            salarioEntry.Text = empleado.Salario.ToString();
            activoSwitch.IsToggled = empleado.Activo;

            actualizarButton.Clicked += actualizarButton_Clicked;
            borrarButton.Clicked += borrarButton_Clicked;


        }

        private async void borrarButton_Clicked(object sender, EventArgs e)
        {
            var rta = await DisplayAlert("Confirmación", "¿Desea borrar el empleado?", "Si", "No");
            if (!rta) return;

            using (var datos = new DataAccess())
            {
                datos.DeleteEmpleado(empleado);
            }

            await DisplayAlert("Confirmación", "Empleado borrado correctamente", "Aceptar");
            await Navigation.PopAsync();
        }

        private async void actualizarButton_Clicked(object sender, EventArgs e)
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

            empleado.Nombres = nombresEntry.Text;
            empleado.Apellidos = apellidosEntry.Text;
            empleado.Salario = decimal.Parse(salarioEntry.Text);
            empleado.FechaContrato = fechaContratoDatePicker.Date;
            empleado.Activo = activoSwitch.IsToggled;

            using (var datos = new DataAccess())
            {
                datos.UpdateEmpleado(empleado);
            }

            await DisplayAlert("Confirmación", "Empleado actualizado correctamente", "Aceptar");
            await Navigation.PopAsync();
        }
    }
}
