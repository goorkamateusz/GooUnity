using System;
using UnityEngine;

namespace Assets.Goo.EnviroTools
{
    public class EnviroChunkOrganizer : MonoBehaviour
    {
        private enum ChunkStrategyName
        {
            SameSizes
        }

        internal abstract class ChunkStrategy
        {
            internal abstract void Chunk(Transform transform);
            internal abstract void DrawGizmos(Transform transform);
        }

        [SerializeField] private ChunkStrategyName _strategy;

        [SerializeField] private SameSizeChunkStrategy _sameSize = new SameSizeChunkStrategy();

        private ChunkStrategy[] Strategies => new ChunkStrategy[]
        {
            _sameSize,
        };

        private int StrategyId => (int)_strategy;

        [ContextMenu("Run")]
        public void Run()
        {
            if (StrategyId > Strategies.Length)
                throw new NotImplementedException();

            Strategies[StrategyId].Chunk(transform);
        }

        protected virtual void OnDrawGizmos()
        {
            if (StrategyId < Strategies.Length)
            {
                Strategies[StrategyId].DrawGizmos(transform);
            }
        }
    }
}