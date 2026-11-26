using System.Globalization;
using System.Text;
using Agenda_Contactos.Negocio;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

AgendaNegocio negocio = new AgendaNegocio();
bool salir = false;

while (!salir)
{
    Console.WriteLine();
    Console.WriteLine("--- Agenda de Contactos ---");
    Console.WriteLine("1. Agregar contacto");
    Console.WriteLine("2. Buscar contacto");
    Console.WriteLine("3. Modificar contacto");
    Console.WriteLine("4. Eliminar contacto");
    Console.WriteLine("5. Listar contactos");
    Console.WriteLine("6. Salir");
    Console.Write("Seleccione una opción: ");
    string? opcion = Console.ReadLine();

    try
    {
        switch (opcion)
        {
            case "1":
                Contacto nuevo = LeerContacto();
                bool agregado = negocio.AgregarContacto(nuevo);
                Console.WriteLine(agregado ? "Contacto agregado correctamente." : "No se pudo agregar el contacto. Verifique los datos o que el DNI no exista.");
                break;

            case "2":
                BuscarContactos(negocio);
                break;

            case "3":
                string dniActual = LeerTexto("Ingrese el DNI del contacto a modificar: ");
                Contacto? contactoActual = negocio.BuscarPorDni(dniActual).FirstOrDefault();

                if (contactoActual == null)
                {
                    Console.WriteLine("No se encontró un contacto con ese DNI.");
                    break;
                }

                Console.WriteLine("Ingrese los nuevos datos. Presione Enter para conservar el valor actual.");
                Contacto contactoModificado = LeerContacto(contactoActual);
                bool modificado = negocio.ModificarContacto(dniActual, contactoModificado);
                Console.WriteLine(modificado ? "Contacto modificado correctamente." : "No se pudo modificar el contacto. Verifique los datos y el DNI.");
                break;

            case "4":
                string dniEliminar = LeerTexto("Ingrese el DNI del contacto a eliminar: ");
                string confirmacion = LeerTexto("¿Confirma la eliminación? (S/N): ");

                if (confirmacion.Equals("S", StringComparison.OrdinalIgnoreCase))
                {
                    bool eliminado = negocio.EliminarContacto(dniEliminar);
                    Console.WriteLine(eliminado ? "Contacto eliminado correctamente." : "No se encontró un contacto con ese DNI.");
                }
                else
                {
                    Console.WriteLine("Eliminación cancelada.");
                }
                break;

            case "5":
                MostrarContactos(negocio.ObtenerTodos());
                break;

            case "6":
                salir = true;
                break;

            default:
                Console.WriteLine("Opción inválida.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ocurrió un error: {ex.Message}");
    }
}

static void BuscarContactos(AgendaNegocio negocio)
{
    Console.WriteLine();
    Console.WriteLine("--- Buscar contacto ---");
    Console.WriteLine("1. Buscar por DNI");
    Console.WriteLine("2. Buscar por Apellido");
    Console.WriteLine("3. Buscar por Nombres");
    Console.WriteLine("4. Buscar por Calle");
    Console.Write("Seleccione una opción: ");
    string? opcion = Console.ReadLine();

    List<Contacto> encontrados;

    switch (opcion)
    {
        case "1":
            encontrados = negocio.BuscarPorDni(LeerTexto("Ingrese DNI: "));
            break;
        case "2":
            encontrados = negocio.BuscarPorApellido(LeerTexto("Ingrese Apellido: "));
            break;
        case "3":
            encontrados = negocio.BuscarPorNombres(LeerTexto("Ingrese Nombres: "));
            break;
        case "4":
            encontrados = negocio.BuscarPorCalle(LeerTexto("Ingrese Calle: "));
            break;
        default:
            Console.WriteLine("Opción inválida.");
            return;
    }

    MostrarContactos(encontrados);
}

static Contacto LeerContacto(Contacto? actual = null)
{
    Contacto contacto = new Contacto
    {
        Dni = LeerDato("DNI", actual?.Dni ?? string.Empty),
        Apellido = LeerDato("Apellido", actual?.Apellido ?? string.Empty),
        Nombres = LeerDato("Nombres", actual?.Nombres ?? string.Empty),
        Calle = LeerDato("Calle", actual?.Calle ?? string.Empty),
        Depto = LeerDato("Depto", actual?.Depto ?? string.Empty),
        Piso = LeerDato("Piso", actual?.Piso ?? string.Empty),
        Ciudad = LeerDato("Ciudad", actual?.Ciudad ?? string.Empty),
        Telefono = LeerDato("Teléfono", actual?.Telefono ?? string.Empty),
        Email = LeerDato("Email", actual?.Email ?? string.Empty),
        CuilCuit = LeerDato("CUIL/CUIT", actual?.CuilCuit ?? string.Empty),
        FechaAlta = LeerFecha("Fecha de alta (dd/MM/yyyy)", actual?.FechaAlta ?? DateTime.Today),
        EstadoCivil = LeerDato("Estado civil", actual?.EstadoCivil ?? string.Empty),
        Nacionalidad = LeerDato("Nacionalidad", actual?.Nacionalidad ?? string.Empty),
        Provincia = LeerDato("Provincia", actual?.Provincia ?? string.Empty),
        CodigoPostal = LeerDato("Código postal", actual?.CodigoPostal ?? string.Empty),
        Barrio = LeerDato("Barrio", actual?.Barrio ?? string.Empty),
        TelefonoAlternativo = LeerDato("Teléfono alternativo", actual?.TelefonoAlternativo ?? string.Empty),
        RedSocialInstagram = LeerDato("Instagram", actual?.RedSocialInstagram ?? string.Empty),
        ProfesionOcupacion = LeerDato("Profesión/Ocupación", actual?.ProfesionOcupacion ?? string.Empty),
        EmpresaLugarTrabajo = LeerDato("Empresa/Lugar de trabajo", actual?.EmpresaLugarTrabajo ?? string.Empty),
        NivelEstudios = LeerDato("Nivel de estudios", actual?.NivelEstudios ?? string.Empty),
        Estado = LeerDato("Estado (ACTIVO/INACTIVO)", actual?.Estado ?? "ACTIVO"),
        MetodoPagoPreferido = LeerDato("Método de pago preferido", actual?.MetodoPagoPreferido ?? string.Empty),
        Observaciones = LeerDato("Observaciones/Notas", actual?.Observaciones ?? string.Empty)
    };

    return contacto;
}

static string LeerDato(string etiqueta, string valorActual)
{
    if (string.IsNullOrEmpty(valorActual))
    {
        Console.Write($"Ingrese {etiqueta}: ");
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    Console.Write($"Ingrese {etiqueta} [{valorActual}]: ");
    string? valor = Console.ReadLine();
    return string.IsNullOrWhiteSpace(valor) ? valorActual : valor.Trim();
}

static DateTime LeerFecha(string etiqueta, DateTime valorActual)
{
    while (true)
    {
        Console.Write($"Ingrese {etiqueta} [{valorActual:dd/MM/yyyy}]: ");
        string? valor = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(valor))
        {
            return valorActual;
        }

        if (DateTime.TryParseExact(valor.Trim(), "dd/MM/yyyy", CultureInfo.GetCultureInfo("es-AR"), DateTimeStyles.None, out DateTime fecha))
        {
            return fecha.Date;
        }

        Console.WriteLine("Fecha inválida. Utilice el formato dd/MM/yyyy.");
    }
}

static string LeerTexto(string mensaje)
{
    Console.Write(mensaje);
    return Console.ReadLine()?.Trim() ?? string.Empty;
}

static void MostrarContactos(List<Contacto> contactos)
{
    if (contactos.Count == 0)
    {
        Console.WriteLine("No se encontraron contactos.");
        return;
    }

    foreach (Contacto contacto in contactos)
    {
        Console.WriteLine();
        Console.WriteLine($"DNI: {contacto.Dni}");
        Console.WriteLine($"Apellido: {contacto.Apellido}");
        Console.WriteLine($"Nombres: {contacto.Nombres}");
        Console.WriteLine($"Calle: {contacto.Calle}");
        Console.WriteLine($"Depto: {contacto.Depto}");
        Console.WriteLine($"Piso: {contacto.Piso}");
        Console.WriteLine($"Ciudad: {contacto.Ciudad}");
        Console.WriteLine($"Teléfono: {contacto.Telefono}");
        Console.WriteLine($"Email: {contacto.Email}");
        Console.WriteLine($"CUIL/CUIT: {contacto.CuilCuit}");
        Console.WriteLine($"Fecha de alta: {contacto.FechaAlta:dd/MM/yyyy}");
        Console.WriteLine($"Estado civil: {contacto.EstadoCivil}");
        Console.WriteLine($"Nacionalidad: {contacto.Nacionalidad}");
        Console.WriteLine($"Provincia: {contacto.Provincia}");
        Console.WriteLine($"Código postal: {contacto.CodigoPostal}");
        Console.WriteLine($"Barrio: {contacto.Barrio}");
        Console.WriteLine($"Teléfono alternativo: {contacto.TelefonoAlternativo}");
        Console.WriteLine($"Instagram: {contacto.RedSocialInstagram}");
        Console.WriteLine($"Profesión/Ocupación: {contacto.ProfesionOcupacion}");
        Console.WriteLine($"Empresa/Lugar de trabajo: {contacto.EmpresaLugarTrabajo}");
        Console.WriteLine($"Nivel de estudios: {contacto.NivelEstudios}");
        Console.WriteLine($"Estado: {contacto.Estado}");
        Console.WriteLine($"Método de pago preferido: {contacto.MetodoPagoPreferido}");
        Console.WriteLine($"Observaciones/Notas: {contacto.Observaciones}");
        Console.WriteLine("----------------------------------------");
    }
}
