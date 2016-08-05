using System;
using System.Collections.Generic;
using System.Linq;
using SQLite.Net;
using Xamarin.Forms;

namespace SQLite
{
    public class DataAccess : IDisposable
    {
        private SQLiteConnection connection;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DirectorioDB, "Empleados.db3"));
            connection.CreateTable<Empleado>();
        }

        public void InsertEmpleado(Empleado empleado)
        {
            connection.Insert(empleado);
        }

        public void UpdateEmpleado(Empleado empleado)
        {
            connection.Update(empleado);
        }

        public void DeleteEmpleado(Empleado empleado)
        {
            connection.Delete(empleado);
        }

        public Empleado GetEmpleado(int IDEmpleado)
        {
            return connection.Table<Empleado>().FirstOrDefault(c => c.IDEmpleado == IDEmpleado);
        }

        public List<Empleado> GetEmpleados()
        {
            return connection.Table<Empleado>().OrderBy(c => c.Apellidos).ToList();
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}