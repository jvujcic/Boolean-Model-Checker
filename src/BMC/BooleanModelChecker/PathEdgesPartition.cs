using System;
using System.Collections.Generic;
using System.Text;

using BDDlib;

namespace BooleanModelChecker
{
    class PathEdgesPartition : IComparable
    {
        private Bdd m_PathEdges;
        internal Bdd PathEdges
        {
            get { return m_PathEdges; }
            set 
            {
                m_PathEdges = value;
            }
        }
        private int m_Length;
        internal int Length
        {
            get { return m_Length; }
        }

        internal PathEdgesPartition(int pathLength, Bdd pathEdge)
        {
            m_PathEdges = pathEdge;
            m_Length = pathLength;
        }

        internal bool AddPaths(Bdd newPaths, BddManager Manager)
        {
            Bdd tempBdd = m_PathEdges;

            m_PathEdges = Manager.LogicalOr(m_PathEdges, newPaths);

            bool changed = true;
            if (tempBdd == m_PathEdges)
            {
                changed = false;
            }

            tempBdd.FreeBdd();
            return changed;
        }

        #region Comparison operator overloading
        public int CompareTo(object obj)
        {
            if (obj is PathEdgesPartition)
            {
                PathEdgesPartition path = (PathEdgesPartition)obj;

                return m_Length.CompareTo(path.Length);
            }

            throw new ArgumentException("object is not a PathEdgePartition");
        }

        public static bool operator <(PathEdgesPartition path1, PathEdgesPartition path2)
        {
            return (path1.Length < path2.Length);
        }
        public static bool operator >(PathEdgesPartition path1, PathEdgesPartition path2)
        {
            return (path1.Length > path2.Length);
        }
        public static bool operator <=(PathEdgesPartition path1, PathEdgesPartition path2)
        {
            return (path1.Length <= path2.Length);
        }
        public static bool operator >=(PathEdgesPartition path1, PathEdgesPartition path2)
        {
            return (path1.Length >= path2.Length);
        }
        public static bool operator ==(PathEdgesPartition path1, PathEdgesPartition path2)
        {
            return (path1.Length == path2.Length);
        }
        public static bool operator !=(PathEdgesPartition path1, PathEdgesPartition path2)
        {
            return (path1.Length != path2.Length);
        }

        #endregion
    }
}
