using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using StackExchange.Redis;

namespace CasheManager
{
	public class RedisCache<T> : ICache<T>
	{
		private ConnectionMultiplexer redisConnection;
		string prefix;
		DataContractSerializer serializer;

		public RedisCache(string hostName, string prefix)
		{
            redisConnection = ConnectionMultiplexer.Connect(hostName);
		    serializer = new DataContractSerializer(typeof(IEnumerable<T>));
            this.prefix = prefix;
        }

		public IEnumerable<T> Get(string id)
		{
			var db = redisConnection.GetDatabase();
			byte[] s = db.StringGet(prefix + id);
			if (s == null)
				return null;

			return (IEnumerable<T>) serializer.ReadObject(new MemoryStream(s));

		}

		//public void Set(string id, T item)
		//{
		//	var db = redisConnection.GetDatabase();
		//	var key = prefix + id;

		//	if (item == null)
		//	{
		//		db.StringSet(key, RedisValue.Null);
		//	}
		//	else
		//	{
		//		var stream = new MemoryStream();
		//		serializer.WriteObject(stream, item);
		//		db.StringSet(key, stream.ToArray());
		//	}
		//}

        public void Set(string id, IEnumerable<T> item)
        {
            var db = redisConnection.GetDatabase();
            var key = prefix + id;

            if (item == null)
            {
                db.StringSet(key, RedisValue.Null);
            }
            else
            {
                var stream = new MemoryStream();
                serializer.WriteObject(stream, item);
                db.StringSet(key, stream.ToArray());
            }
        }
    }
}
