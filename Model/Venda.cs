namespace Model
{
    public class Venda
    {
        public int Codigo { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public double ValorTotal { get; set; }
        public double Desconto { get; set; }
        public string Ativo { get; set; }
    }
}
