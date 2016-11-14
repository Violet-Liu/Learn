/* Reference from N2E project
 * = - - - - - - - - - - - - -  -- - -  - - =
 * Object structure as the following:
 *                          Bucket
 *                       /          \ 
 *                  Cluster       Cluster
 *                /         \
 *            Chunk        Chunk
 *          /       \
 *        Item     Item    
 *        
 * Sha Jianjian
 * 2016-11-3  
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Foundation.Collection
{
    /// <summary>
    /// This type is used to storage a large number of items. Eack hierarchy size is fixed
    /// <para>Not thread-safe</para>
    /// </summary>
    public class FixedBucket<T>
    {
        /// <summary>
        /// max count of items contained in a chunk
        /// </summary>
        public const int CHUNK_CAPACITY = 0x400;
        /// <summary>
        /// max count of chunks contained in cluster
        /// </summary>
        public const int CLUSTER_CAPACITY = 0x100;
        /// <summary>
        /// max count of items contained in a cluster.
        /// <para>
        /// <seealso cref="CHUNK_CAPACITY"/> * <seealso cref="CLUSTER_CAPACITY"/>
        /// </para>
        /// </summary>
        public const int CLUSTER_SIZE = 0x40000;
        /// <summary>
        /// max count of cluster contained in a bucket
        /// </summary>
        public const int BUCKET_CAPACITY = 0x100;

        /// <summary>
        /// Denotes index of the last item added in this bucket
        /// </summary>
        private int _cursor;

        /// <summary>
        /// The bucket to storage items
        /// </summary>
        private List<List<T[]>> _bucket;

        public FixedBucket()
        {
            _bucket = new List<List<T[]>>(BUCKET_CAPACITY);
            for(int i = 0; i < BUCKET_CAPACITY; i++)
            {
                _bucket.Add(null);
            }
        }
    }
}
