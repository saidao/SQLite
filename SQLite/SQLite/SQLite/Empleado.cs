using SQLite.Net.Attributes;
using System;

namespace SQLite
{
    public class Empleado
    {
        [PrimaryKey, AutoIncrement]
        public int IDEmpleado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaContrato { get; set; }
        public decimal Salario { get; set; }
        public bool Activo { get; set; }

        public string NombreCompleto
        {
            get
            {
                return string.Format("{0} {1}", this.Nombres, this.Apellidos);
            }
        }

        public string FechaContratoEdited
        {
            get
            {
                return string.Format("{0:yy-MM-dd}", FechaContrato);
            }
        }

        public string SalarioEdited
        {
            get
            {
                return string.Format("{0:C2}", Salario);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", IDEmpleado, NombreCompleto,
                FechaContratoEdited, SalarioEdited, Activo);
        }

    }
}
