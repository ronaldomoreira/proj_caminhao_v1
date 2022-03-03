using BusinessSevice;
using DomainApp.Entities;
using DomainApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjTestCaminhao.Repository;
using Repository.Context;
using System;

namespace ProjTestCaminhao
{
    [TestClass]
    public class CaminhaoBusinessServiceTest
    {
        private readonly ICaminhaoBusinessService _caminhaoBusinessService;

        public CaminhaoBusinessServiceTest()
        {
            _caminhaoBusinessService = new CaminhaoBusinessService(null, new RepositoryFake(null));
        }

        [TestMethod]
        public void TestRegraIntervaloAnoModeloOk()
        {
            bool intervaloValido =  _caminhaoBusinessService.ValidarDifAnos(2011, 2010);

            Assert.IsTrue(intervaloValido);
        }

        [TestMethod]
        public void TestRegraIntervaloAnoModeloNotOk()
        {

            bool intervaloValido = _caminhaoBusinessService.ValidarDifAnos(2009, 2010);

            Assert.IsFalse(intervaloValido);
        }

        [TestMethod]
        public void TestRegraAddCaminhaoOk()
        {
            Caminhao caminhaMok = new Caminhao
            {
                Id = 20010,
                AnoFabricacao = 2021,
                AnoModelo = 2022,
                Fabricante = "Volvo",
                Modelo = "FH"
            };

            try
            {
                Caminhao caminhao = _caminhaoBusinessService.Add(caminhaMok).Result;
                Assert.AreEqual(caminhao, caminhaMok);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [TestMethod]
        public void TestRegraAddCaminhaoNoOk()
        {
            Caminhao caminhaMok = new Caminhao
            {
                Id = 20010,
                AnoFabricacao = 2000,
                AnoModelo = 1999,
                Fabricante = "Volvo",
                Modelo = "FH"
            };

            Assert.ThrowsException<Exception>(() => _caminhaoBusinessService.Add(caminhaMok));
        }

        [TestMethod]
        public void TestRegraEditCaminhaoOK()
        {
            Caminhao caminhaMok = new Caminhao
            {
                Id = 20011,
                AnoFabricacao = 2021,
                AnoModelo = 2021,
                Fabricante = "Volvo",
                Modelo = "FH"
            };

            try
            {
                _caminhaoBusinessService.Update(caminhaMok);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestRegraEditCaminhaoNoOK()
        {
            Caminhao caminhaMok = new Caminhao
            {
                Id = 20012,
                AnoFabricacao = 2021,
                AnoModelo = 2010,
                Fabricante = "Volvo",
                Modelo = "FH"
            };

            Assert.ThrowsException<Exception>(() => _caminhaoBusinessService.Update(caminhaMok));
        }

        [TestMethod]
        public void TestRegraDeleteCaminhao()
        {
            try
            {
                _caminhaoBusinessService.Remove(100);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}