using MySql.Data.MySqlClient;

namespace Agenda_Contactos.Datos
{
    public class AgendaDatos
    {
        private readonly string _conexionString = "Server=localhost;Database=agenda_db;Uid=root;Pwd=;";
        private const string Columnas = "DNI, Apellido, Nombres, Calle, Depto, Piso, Ciudad, Telefono, Email, CuilCuit, FechaAlta, EstadoCivil, Nacionalidad, Provincia, CodigoPostal, Barrio, TelefonoAlternativo, RedSocialInstagram, ProfesionOcupacion, EmpresaLugarTrabajo, NivelEstudios, Estado, MetodoPagoPreferido, Observaciones";

        public bool Agregar(ContactoRegistro contacto)
        {
            string query = "INSERT INTO contacto (DNI, Apellido, Nombres, Calle, Depto, Piso, Ciudad, Telefono, Email, CuilCuit, FechaAlta, EstadoCivil, Nacionalidad, Provincia, CodigoPostal, Barrio, TelefonoAlternativo, RedSocialInstagram, ProfesionOcupacion, EmpresaLugarTrabajo, NivelEstudios, Estado, MetodoPagoPreferido, Observaciones) VALUES (@DNI, @Apellido, @Nombres, @Calle, @Depto, @Piso, @Ciudad, @Telefono, @Email, @CuilCuit, @FechaAlta, @EstadoCivil, @Nacionalidad, @Provincia, @CodigoPostal, @Barrio, @TelefonoAlternativo, @RedSocialInstagram, @ProfesionOcupacion, @EmpresaLugarTrabajo, @NivelEstudios, @Estado, @MetodoPagoPreferido, @Observaciones)";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                AgregarParametros(comando, contacto);
                conexion.Open();
                return comando.ExecuteNonQuery() > 0;
            }
        }

        public List<ContactoRegistro> BuscarPorDni(string dni)
        {
            return BuscarPorCampo("DNI", dni, true);
        }

        public List<ContactoRegistro> BuscarPorApellido(string apellido)
        {
            return BuscarPorCampo("Apellido", apellido, false);
        }

        public List<ContactoRegistro> BuscarPorNombres(string nombres)
        {
            return BuscarPorCampo("Nombres", nombres, false);
        }

        public List<ContactoRegistro> BuscarPorCalle(string calle)
        {
            return BuscarPorCampo("Calle", calle, false);
        }

        public List<ContactoRegistro> ObtenerTodos()
        {
            string query = $"SELECT {Columnas} FROM contacto ORDER BY Apellido, Nombres";
            return EjecutarConsulta(query);
        }

        public bool Modificar(string dniActual, ContactoRegistro contacto)
        {
            string query = "UPDATE contacto SET DNI = @DNI, Apellido = @Apellido, Nombres = @Nombres, Calle = @Calle, Depto = @Depto, Piso = @Piso, Ciudad = @Ciudad, Telefono = @Telefono, Email = @Email, CuilCuit = @CuilCuit, FechaAlta = @FechaAlta, EstadoCivil = @EstadoCivil, Nacionalidad = @Nacionalidad, Provincia = @Provincia, CodigoPostal = @CodigoPostal, Barrio = @Barrio, TelefonoAlternativo = @TelefonoAlternativo, RedSocialInstagram = @RedSocialInstagram, ProfesionOcupacion = @ProfesionOcupacion, EmpresaLugarTrabajo = @EmpresaLugarTrabajo, NivelEstudios = @NivelEstudios, Estado = @Estado, MetodoPagoPreferido = @MetodoPagoPreferido, Observaciones = @Observaciones WHERE DNI = @DniActual";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                AgregarParametros(comando, contacto);
                comando.Parameters.AddWithValue("@DniActual", dniActual);
                conexion.Open();
                return comando.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(string dni)
        {
            string query = "DELETE FROM contacto WHERE DNI = @DNI";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@DNI", dni);
                conexion.Open();
                return comando.ExecuteNonQuery() > 0;
            }
        }

        private List<ContactoRegistro> BuscarPorCampo(string campo, string valor, bool exacto)
        {
            string columna = campo switch
            {
                "DNI" => "DNI",
                "Apellido" => "Apellido",
                "Nombres" => "Nombres",
                "Calle" => "Calle",
                _ => throw new ArgumentException("Campo de busqueda invalido.")
            };

            string condicion = exacto ? "= @Valor" : "LIKE @Valor";
            string query = $"SELECT {Columnas} FROM contacto WHERE {columna} {condicion} ORDER BY Apellido, Nombres";
            string valorBusqueda = exacto ? valor : $"%{valor}%";
            return EjecutarConsulta(query, valorBusqueda);
        }

        private List<ContactoRegistro> EjecutarConsulta(string query, string? valorParametro = null)
        {
            List<ContactoRegistro> contactos = new List<ContactoRegistro>();

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);

                if (valorParametro != null)
                {
                    comando.Parameters.AddWithValue("@Valor", valorParametro);
                }

                conexion.Open();

                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contactos.Add(MapearContactoRegistro(reader));
                    }
                }
            }

            return contactos;
        }

        private ContactoRegistro MapearContactoRegistro(MySqlDataReader reader)
        {
            return new ContactoRegistro
            {
                Dni = LeerTexto(reader, "DNI"),
                Apellido = LeerTexto(reader, "Apellido"),
                Nombres = LeerTexto(reader, "Nombres"),
                Calle = LeerTexto(reader, "Calle"),
                Depto = LeerTexto(reader, "Depto"),
                Piso = LeerTexto(reader, "Piso"),
                Ciudad = LeerTexto(reader, "Ciudad"),
                Telefono = LeerTexto(reader, "Telefono"),
                Email = LeerTexto(reader, "Email"),
                CuilCuit = LeerTexto(reader, "CuilCuit"),
                FechaAlta = Convert.ToDateTime(reader["FechaAlta"]),
                EstadoCivil = LeerTexto(reader, "EstadoCivil"),
                Nacionalidad = LeerTexto(reader, "Nacionalidad"),
                Provincia = LeerTexto(reader, "Provincia"),
                CodigoPostal = LeerTexto(reader, "CodigoPostal"),
                Barrio = LeerTexto(reader, "Barrio"),
                TelefonoAlternativo = LeerTexto(reader, "TelefonoAlternativo"),
                RedSocialInstagram = LeerTexto(reader, "RedSocialInstagram"),
                ProfesionOcupacion = LeerTexto(reader, "ProfesionOcupacion"),
                EmpresaLugarTrabajo = LeerTexto(reader, "EmpresaLugarTrabajo"),
                NivelEstudios = LeerTexto(reader, "NivelEstudios"),
                Estado = LeerTexto(reader, "Estado"),
                MetodoPagoPreferido = LeerTexto(reader, "MetodoPagoPreferido"),
                Observaciones = LeerTexto(reader, "Observaciones")
            };
        }

        private string LeerTexto(MySqlDataReader reader, string columna)
        {
            return reader[columna] == DBNull.Value ? string.Empty : reader[columna].ToString() ?? string.Empty;
        }

        private void AgregarParametros(MySqlCommand comando, ContactoRegistro contacto)
        {
            comando.Parameters.AddWithValue("@DNI", contacto.Dni);
            comando.Parameters.AddWithValue("@Apellido", contacto.Apellido);
            comando.Parameters.AddWithValue("@Nombres", contacto.Nombres);
            comando.Parameters.AddWithValue("@Calle", contacto.Calle);
            comando.Parameters.AddWithValue("@Depto", ValorOpcional(contacto.Depto));
            comando.Parameters.AddWithValue("@Piso", ValorOpcional(contacto.Piso));
            comando.Parameters.AddWithValue("@Ciudad", contacto.Ciudad);
            comando.Parameters.AddWithValue("@Telefono", contacto.Telefono);
            comando.Parameters.AddWithValue("@Email", contacto.Email);
            comando.Parameters.AddWithValue("@CuilCuit", ValorOpcional(contacto.CuilCuit));
            comando.Parameters.AddWithValue("@FechaAlta", contacto.FechaAlta.Date);
            comando.Parameters.AddWithValue("@EstadoCivil", ValorOpcional(contacto.EstadoCivil));
            comando.Parameters.AddWithValue("@Nacionalidad", ValorOpcional(contacto.Nacionalidad));
            comando.Parameters.AddWithValue("@Provincia", ValorOpcional(contacto.Provincia));
            comando.Parameters.AddWithValue("@CodigoPostal", ValorOpcional(contacto.CodigoPostal));
            comando.Parameters.AddWithValue("@Barrio", ValorOpcional(contacto.Barrio));
            comando.Parameters.AddWithValue("@TelefonoAlternativo", ValorOpcional(contacto.TelefonoAlternativo));
            comando.Parameters.AddWithValue("@RedSocialInstagram", ValorOpcional(contacto.RedSocialInstagram));
            comando.Parameters.AddWithValue("@ProfesionOcupacion", ValorOpcional(contacto.ProfesionOcupacion));
            comando.Parameters.AddWithValue("@EmpresaLugarTrabajo", ValorOpcional(contacto.EmpresaLugarTrabajo));
            comando.Parameters.AddWithValue("@NivelEstudios", ValorOpcional(contacto.NivelEstudios));
            comando.Parameters.AddWithValue("@Estado", contacto.Estado);
            comando.Parameters.AddWithValue("@MetodoPagoPreferido", ValorOpcional(contacto.MetodoPagoPreferido));
            comando.Parameters.AddWithValue("@Observaciones", ValorOpcional(contacto.Observaciones));
        }

        private object ValorOpcional(string? valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? DBNull.Value : valor.Trim();
        }
    }
}
