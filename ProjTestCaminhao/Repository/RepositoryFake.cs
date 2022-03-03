using DomainApp.Entities;
using DomainApp.Interfaces;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjTestCaminhao.Repository
{
    public class RepositoryFake : IRepositoryCaminhao
    {
        public RepositoryFake(AppMainDbContext context)
        {

        }

        public void Dispose()
        {
            //
        }

        public async Task<Caminhao?> Add(Caminhao item)
        {
            return Task.FromResult<Caminhao>(item).Result;
        }

        public async Task AddRange(IEnumerable<Caminhao> itens)
        {

        }

        public Task<IEnumerable<Caminhao>> GetAll()
        {
            List<Caminhao> listaCaminhoes = new List<Caminhao>();
            listaCaminhoes.Add(new Caminhao
            {
                Id = 2000,
                AnoFabricacao = 2010,
                AnoModelo = 2011,
                Fabricante = "Ford",
                Modelo = "FH"
            });
            listaCaminhoes.Add(new Caminhao
            {
                Id = 2001,
                AnoFabricacao = 2020,
                AnoModelo = 2021,
                Fabricante = "Volvo",
                Modelo = "FH"
            });

            listaCaminhoes.Add(new Caminhao
            {
                Id = 2002,
                AnoFabricacao = 2019,
                AnoModelo = 2020,
                Fabricante = "Scania",
                Modelo = "FH"
            });

            return Task.FromResult<IEnumerable<Caminhao>>(listaCaminhoes);
        }

        public Task<Caminhao?> GetById(long? id)
        {
            Caminhao caminhao = new Caminhao
            {
                Id = id??0,
                AnoFabricacao = 2019,
                AnoModelo = 2020,
                Fabricante = "Scania",
                Modelo = "FH"
            };

            return Task.FromResult<Caminhao?>(caminhao);
        }

        public async Task Remove(long? id)
        {
            //
        }

        public async Task Remove(Caminhao item)
        {
            //
        }

        public void RemoveRange(IEnumerable<Caminhao> itens)
        {
            //
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult<int>(1);
        }

        public void Update(Caminhao item)
        {
            //
        }
    }
}
