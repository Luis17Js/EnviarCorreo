namespace SendEmail.Models
{
    public class EmailDTO
    {
        public string Compañia { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Para { get; set; } = string.Empty;
       
        public string Contenido { get; set; } = string.Empty;
    }
}
