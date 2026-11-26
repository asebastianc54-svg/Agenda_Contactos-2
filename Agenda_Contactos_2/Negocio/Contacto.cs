namespace Agenda_Contactos.Negocio
{
    public class Contacto
    {
        public string Dni { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Calle { get; set; } = string.Empty;
        public string Depto { get; set; } = string.Empty;
        public string Piso { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CuilCuit { get; set; } = string.Empty;
        public DateTime FechaAlta { get; set; } = DateTime.Today;
        public string EstadoCivil { get; set; } = string.Empty;
        public string Nacionalidad { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Barrio { get; set; } = string.Empty;
        public string TelefonoAlternativo { get; set; } = string.Empty;
        public string RedSocialInstagram { get; set; } = string.Empty;
        public string ProfesionOcupacion { get; set; } = string.Empty;
        public string EmpresaLugarTrabajo { get; set; } = string.Empty;
        public string NivelEstudios { get; set; } = string.Empty;
        public string Estado { get; set; } = "ACTIVO";
        public string MetodoPagoPreferido { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
    }
}
