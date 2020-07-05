namespace Model
{
    public class Movimentacao
    {
        public int Codigo { get; set; }
        public int Quantidade { get; set; }
        public int CodigoProduto { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public string Motivo { get; set; }
        public string Acao { get; set; }
    }
}
