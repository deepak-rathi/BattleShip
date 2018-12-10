using System;
using System.Collections.Generic;

namespace BattleShipGame.Models
{
    internal class GamePlayBoard : IDisposable
    {
        // Flag: Has Dispose already been called?
        private bool disposed = false;

        public int Id { get; private set; }
        public bool IsAllShipsDistroyed { get; set; }
        public int[,] Board { get; set; }
        public IList<string> Target { get; set; }

        private static IList<bool> _usedCounter = new List<bool>();
        private static object _lock = new object();

        public GamePlayBoard()
        {
            lock (_lock)
            {
                int nextIndex = GetAvailableIndex();
                if (nextIndex == -1)
                {
                    nextIndex = _usedCounter.Count;
                    _usedCounter.Add(true);
                }

                Id = nextIndex;
            }
        }

        private int GetAvailableIndex()
        {
            for (int i = 0; i < _usedCounter.Count; i++)
            {
                if (_usedCounter[i] == false)
                {
                    return i;
                }
            }

            // Nothing available.
            return -1;
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                lock (_lock)
                {
                    _usedCounter[Id] = false;
                }
            }

            disposed = true;
        }
    }
}