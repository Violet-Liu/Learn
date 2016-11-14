using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Foundation.Collection
{
    public class FlexibleBucket<T>
    {
        public int ChunkCapacity { get; private set; }
        public int ClusterCapacity { get; private set; }
        public int ClusterSize { get; private set; }
        public int BucketCapacity { get; private set; }

        private int _cursor = -1;
        private List<List<T[]>> _bucket;

        private int _count;
        public int Count { get { return _count; } }
        /// <summary>
        /// Param must be large than 1024
        /// </summary>
        /// <param name="total"></param>
        public FlexibleBucket(int total)
        {
            var chkCount = total >> 10;
            Util.Int_Assert(i => i != 0, chkCount);                // make sure: many chunks

            
            if ((total >> 18) == 0 || (total ^ 0x40000) == 0)      // total <= 2^18，a single cluster
            {
                if (total % 0x400 != 0)
                    chkCount++;
                Init(0x400, chkCount, 1);
            }
            else                                                    // total > 2^18，many clusters
            {
                var clsCount = total >> 18;
                if (total % 0x40000 != 0)
                    clsCount++;
                Init(0x400, 0x100, clsCount);
            }
        }

        /// <summary>
        /// Each param must be large than 0x32
        /// </summary>
        /// <param name="chkCap"></param>
        /// <param name="clstCap"></param>
        /// <param name="buckCap"></param>
        public FlexibleBucket(int chkCap, int clstCap, int buckCap)
        {
            Util.Int_Assert(i => (i >> 5) != 0, chkCap, clstCap, buckCap);

            Init(chkCap, clstCap, buckCap);
        }

        private void Init(int chkCap, int clstCap, int buckCap)
        {
            ChunkCapacity = chkCap;
            ClusterCapacity = clstCap;
            BucketCapacity = buckCap;
            ClusterSize = chkCap * clstCap;

            _bucket = new List<List<T[]>>(BucketCapacity);
            for(int i = 0; i < BucketCapacity; i++)
            {
                _bucket.Add(null);
            }
        }

        public T Get(int termId)
        {
            var clsIndex = GetClusterIndex(termId);
            var cluster = GetCluster(clsIndex, false);
            if (cluster == null)
                return default(T);

            int chkIndex = GetChunkIndex(termId);
            var chunk = GetChunk(cluster, chkIndex, false);
            if (chunk == null)
                return default(T);

            int termIndex = GetTermIndex(termId);
            return chunk[termIndex];
        }

        public int Set(T term)
        {
            int termId = CreateTermId();

            var clsIndex = GetClusterIndex(termId);
            var cluster = GetCluster(clsIndex, true);

            var chkIndex = GetChunkIndex(termId);
            var chunk = GetChunk(cluster, chkIndex, true);

            var termIndex = GetTermIndex(termId);
            chunk[termIndex] = term;

            _count++;
            return termId;
        }

        private int CreateTermId() => ++_cursor;

        private int GetClusterIndex(int termId) => termId / ClusterSize;

        private int GetChunkIndex(int termId) => (termId % ClusterSize) / ChunkCapacity;

        private int GetTermIndex(int termId) => termId % ClusterSize % ChunkCapacity;

        private List<T[]> GetCluster(int clsIndex, bool ini)
        {
            var cluster = _bucket[clsIndex];
            if (cluster != null) return cluster;

            if(ini)
            {
                cluster = new List<T[]>(ClusterCapacity);
                for(int i = 0; i < ClusterCapacity; i++)
                {
                    cluster.Add(null);
                }
                _bucket[clsIndex] = cluster;
            }
            return cluster;
        }

        private T[] GetChunk(List<T[]> cluster, int chkIndex, bool ini)
        {
            var chunk = cluster[chkIndex];
            if(chunk == null && ini)
            {
                chunk = new T[ChunkCapacity];
                cluster[chkIndex] = chunk;
            }
            return chunk;
        }
    }
}
