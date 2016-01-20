using System.Collections.Generic;
using System.Linq;

namespace RepositorioAdapter.Adapter
{
    public abstract class BaseAdapter<TEntityModel, TDtoModel> : IAdapter<TEntityModel, TDtoModel>
    {
        public abstract TEntityModel FromViewModel(TDtoModel model);
        public abstract TDtoModel FromModel(TEntityModel model);

        public ICollection<TEntityModel> FromViewModel(ICollection<TDtoModel> models) => models.Select(FromViewModel).ToList();
        public ICollection<TDtoModel> FromModel(ICollection<TEntityModel> models) => models.Select(FromModel).ToList();
    }
}