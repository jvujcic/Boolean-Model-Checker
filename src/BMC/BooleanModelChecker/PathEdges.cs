using System;
using System.Collections.Generic;
using System.Text;

using BDDlib;

namespace BooleanModelChecker
{
    class PathEdges
    {
        private List<PathEdgesPartition> m_PathEdgesByLength;
        internal List<PathEdgesPartition> PathEdgesByLength
        {
            get { return m_PathEdgesByLength; }
        }

        internal PathEdges()
        {
            m_PathEdgesByLength = new List<PathEdgesPartition>();
        }

        internal bool AddPathEdge(PathEdgesPartition NewPathEdge, BddManager Manager)
        {
            Bdd allPaths = GetJointPaths(Manager);
            Bdd tempBdd = Manager.LogicalOr(allPaths, NewPathEdge.PathEdges);
            if (tempBdd == allPaths)
            {
                allPaths.FreeBdd();
                return false;
            }

            allPaths.FreeBdd();

            m_PathEdgesByLength.Sort();
            int position = m_PathEdgesByLength.BinarySearch(NewPathEdge);

            bool updated = false;
            if (position >= 0)
            {
                if (m_PathEdgesByLength[position].AddPaths(NewPathEdge.PathEdges, Manager))
                {
                    updated = true;
                }
            }
            else
            {
                m_PathEdgesByLength.Add(NewPathEdge);
                updated = true;
                m_PathEdgesByLength.Sort();
            }

            return updated;
        }

        internal Bdd GetJointPaths(BddManager Manager)
        {
            Bdd JointPaths = Manager.CreateBddZero();

            foreach (PathEdgesPartition part in m_PathEdgesByLength)
            {
                Bdd tempBdd = JointPaths;

                JointPaths = Manager.LogicalOr(JointPaths, part.PathEdges);

                tempBdd.FreeBdd();
            }

            return JointPaths;
        }

        internal Bdd GetPathEdgesOfLength(int len)
        {
            int low, high, mid;

            low = 0;
            high = m_PathEdgesByLength.Count-1;
            while (low <= high)
            {
                mid = (low + high) / 2;
                if (m_PathEdgesByLength[mid].Length > len)
                    high = mid - 1;
                else if (m_PathEdgesByLength[mid].Length < len)
                    low = mid + 1;
                else return m_PathEdgesByLength[mid].PathEdges;
            }

            return null;
        }

        public void FreeAllBdd()
        {
            foreach(PathEdgesPartition pathPart in m_PathEdgesByLength)
            {
                pathPart.PathEdges.FreeBdd();
            }
        }
    }
}
