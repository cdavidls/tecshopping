using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controller;

namespace Test
{
    [TestClass]
    public class VendaTest
    {
        [TestMethod]
        public void Test_Cancelar()
        {
            try
            {
                ControllerVenda controllerVenda = new ControllerVenda();
                bool retorno = controllerVenda.Cancelar(4);

                // Verifica se o retorno é verdadeiro para passar no teste.
                Assert.IsTrue(retorno);
            }
            catch (Exception erro)
            {

                throw erro;
            }
        }
        
    }
}
