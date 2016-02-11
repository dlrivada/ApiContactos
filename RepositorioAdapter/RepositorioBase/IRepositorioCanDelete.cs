﻿using DomainModels.Base;

namespace Repositorio.RepositorioBase
{
    public interface IRepositorioCanDelete<TAuth, TModel> where TModel : DomainModel
    {
        void Delete(TAuth authentication, TModel model);
    }
}