using System.Collections.Generic;

namespace CasheManager
{
	public interface ICache<T>
	{
		//void Set(string id, T item);
        void Set(string id, IEnumerable<T> item);
        IEnumerable<T> Get(string id);
    }
}
