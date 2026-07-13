namespace web_hello_world.Dto
{
    public class Respuesta
    {
        public int Estado { get; set; }
        public Object? Valor { get; set; }
        public string? ErrorMessage { get; set; }
        public bool? Success { get; set; }

    }
}
