using Agenda_Contactos.Datos;

namespace Agenda_Contactos.Negocio
{
    public class AgendaNegocio
    {
        private readonly AgendaDatos _datos = new AgendaDatos();

        public List<Contacto> BuscarPorDni(string? dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                return new List<Contacto>();
            }

            return ConvertirLista(_datos.BuscarPorDni(dni.Trim()));
        }

        public List<Contacto> BuscarPorApellido(string? apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
            {
                return new List<Contacto>();
            }

            return ConvertirLista(_datos.BuscarPorApellido(apellido.Trim()));
        }

        public List<Contacto> BuscarPorNombres(string? nombres)
        {
            if (string.IsNullOrWhiteSpace(nombres))
            {
                return new List<Contacto>();
            }

            return ConvertirLista(_datos.BuscarPorNombres(nombres.Trim()));
        }

        public List<Contacto> BuscarPorCalle(string? calle)
        {
            if (string.IsNullOrWhiteSpace(calle))
            {
                return new List<Contacto>();
            }

            return ConvertirLista(_datos.BuscarPorCalle(calle.Trim()));
        }

        public List<Contacto> ObtenerTodos()
        {
            return ConvertirLista(_datos.ObtenerTodos());
        }

        public bool AgregarContacto(Contacto contacto)
        {
            if (!EsValido(contacto))
            {
                return false;
            }

            if (_datos.BuscarPorDni(contacto.Dni.Trim()).Count > 0)
            {
                return false;
            }

            contacto.Estado = contacto.Estado.Trim().ToUpperInvariant();
            return _datos.Agregar(ConvertirRegistro(contacto));
        }

        public bool ModificarContacto(string? dniActual, Contacto contacto)
        {
            if (string.IsNullOrWhiteSpace(dniActual) || !EsValido(contacto))
            {
                return false;
            }

            string dniAnterior = dniActual.Trim();
            string dniNuevo = contacto.Dni.Trim();

            if (_datos.BuscarPorDni(dniAnterior).Count == 0)
            {
                return false;
            }

            if (!dniAnterior.Equals(dniNuevo, StringComparison.OrdinalIgnoreCase) && _datos.BuscarPorDni(dniNuevo).Count > 0)
            {
                return false;
            }

            contacto.Estado = contacto.Estado.Trim().ToUpperInvariant();
            return _datos.Modificar(dniAnterior, ConvertirRegistro(contacto));
        }

        public bool EliminarContacto(string? dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                return false;
            }

            return _datos.Eliminar(dni.Trim());
        }

        private bool EsValido(Contacto contacto)
        {
            if (contacto == null)
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(contacto.Dni) &&
                   !string.IsNullOrWhiteSpace(contacto.Apellido) &&
                   !string.IsNullOrWhiteSpace(contacto.Nombres) &&
                   !string.IsNullOrWhiteSpace(contacto.Calle) &&
                   !string.IsNullOrWhiteSpace(contacto.Ciudad) &&
                   !string.IsNullOrWhiteSpace(contacto.Telefono) &&
                   !string.IsNullOrWhiteSpace(contacto.Email) &&
                   contacto.FechaAlta != DateTime.MinValue &&
                   (contacto.Estado.Equals("ACTIVO", StringComparison.OrdinalIgnoreCase) ||
                    contacto.Estado.Equals("INACTIVO", StringComparison.OrdinalIgnoreCase));
        }

        private List<Contacto> ConvertirLista(List<ContactoRegistro> registros)
        {
            List<Contacto> contactos = new List<Contacto>();

            foreach (ContactoRegistro registro in registros)
            {
                contactos.Add(new Contacto
                {
                    Dni = registro.Dni,
                    Apellido = registro.Apellido,
                    Nombres = registro.Nombres,
                    Calle = registro.Calle,
                    Depto = registro.Depto,
                    Piso = registro.Piso,
                    Ciudad = registro.Ciudad,
                    Telefono = registro.Telefono,
                    Email = registro.Email,
                    CuilCuit = registro.CuilCuit,
                    FechaAlta = registro.FechaAlta,
                    EstadoCivil = registro.EstadoCivil,
                    Nacionalidad = registro.Nacionalidad,
                    Provincia = registro.Provincia,
                    CodigoPostal = registro.CodigoPostal,
                    Barrio = registro.Barrio,
                    TelefonoAlternativo = registro.TelefonoAlternativo,
                    RedSocialInstagram = registro.RedSocialInstagram,
                    ProfesionOcupacion = registro.ProfesionOcupacion,
                    EmpresaLugarTrabajo = registro.EmpresaLugarTrabajo,
                    NivelEstudios = registro.NivelEstudios,
                    Estado = registro.Estado,
                    MetodoPagoPreferido = registro.MetodoPagoPreferido,
                    Observaciones = registro.Observaciones
                });
            }

            return contactos;
        }

        private ContactoRegistro ConvertirRegistro(Contacto contacto)
        {
            return new ContactoRegistro
            {
                Dni = contacto.Dni.Trim(),
                Apellido = contacto.Apellido.Trim(),
                Nombres = contacto.Nombres.Trim(),
                Calle = contacto.Calle.Trim(),
                Depto = contacto.Depto.Trim(),
                Piso = contacto.Piso.Trim(),
                Ciudad = contacto.Ciudad.Trim(),
                Telefono = contacto.Telefono.Trim(),
                Email = contacto.Email.Trim(),
                CuilCuit = contacto.CuilCuit.Trim(),
                FechaAlta = contacto.FechaAlta.Date,
                EstadoCivil = contacto.EstadoCivil.Trim(),
                Nacionalidad = contacto.Nacionalidad.Trim(),
                Provincia = contacto.Provincia.Trim(),
                CodigoPostal = contacto.CodigoPostal.Trim(),
                Barrio = contacto.Barrio.Trim(),
                TelefonoAlternativo = contacto.TelefonoAlternativo.Trim(),
                RedSocialInstagram = contacto.RedSocialInstagram.Trim(),
                ProfesionOcupacion = contacto.ProfesionOcupacion.Trim(),
                EmpresaLugarTrabajo = contacto.EmpresaLugarTrabajo.Trim(),
                NivelEstudios = contacto.NivelEstudios.Trim(),
                Estado = contacto.Estado.Trim(),
                MetodoPagoPreferido = contacto.MetodoPagoPreferido.Trim(),
                Observaciones = contacto.Observaciones.Trim()
            };
        }
    }
}
