using System.Collections.Generic;

namespace RepositorioAdapter.Adapter
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityModel">Objeto de entidad de la BBDD</typeparam>
    /// <typeparam name="TDtoModel">Objeto de transferencia, es lo que mando al movil...</typeparam>
    public interface IAdapter<TEntityModel, TDtoModel>
    {
        TEntityModel FromViewModel(TDtoModel model);
        TDtoModel FromModel(TEntityModel model);
        ICollection<TEntityModel> FromViewModel(ICollection<TDtoModel> models);
        ICollection<TDtoModel> FromModel(ICollection<TEntityModel> models);
    }
}