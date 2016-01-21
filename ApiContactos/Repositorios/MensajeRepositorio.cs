using System.Data.Entity;
using ApiContactos.Adapter;
using ApiContactos.Models;
using ContactosModel.Model;
using RepositorioAdapter.Repositorio;

namespace ApiContactos.Repositorios
{
    public class MensajeRepositorio : BaseRepositorioEntity<Mensaje, MensajeModel, MensajeAdapter>
    {
        public MensajeRepositorio(DbContext context) : base(context)
        {
        }
    }
}