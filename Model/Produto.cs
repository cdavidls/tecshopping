namespace Model
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public string UnidadeMedida { get; set; }
        public int QtdMinima { get; set; }
        public int QtdAtual { get; set; }
        public int QtdMaxima { get; set; }
        public double CustoUnitario { get; set; }
        public double PercentualLucro { get; set; }
        public double PrecoVenda { get; set; }
        public string Ativo { get; set; }
    }
}
