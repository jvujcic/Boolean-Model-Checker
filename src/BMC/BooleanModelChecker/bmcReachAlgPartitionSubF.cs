
using BooleanModelChecker.ControlFlowGraph;
using BDDlib;
using System.Collections.Generic;
using antlr;

namespace BooleanModelChecker
{
    public partial class BMC
    {
        private void Propagate(CFGNode node, PathEdges pathEdge, LinkedList<CFGNode> workList)
        {
            PathEdges nodePaths;
            m_PathEdges.TryGetValue(node, out nodePaths);

            if (pathEdge != null)
            {
                List<PathEdgesPartition> newPaths = pathEdge.PathEdgesByLength;

                int index = 0;

                bool updated = false;
                while (index < newPaths.Count)
                {
                    if (nodePaths.AddPathEdge(newPaths[index], m_BddManager))
                    {
                        updated = true;
                        newPaths[index].PathEdges.UnfreeBdd();
                    }

                    index++;
                }

                if (updated)
                {
                    workList.AddLast(node);
                }
            }
        }

        private PathEdges Join(PathEdges pathEdge, Bdd TransitionBdd, Variables localVariables)
        {
            PathEdges tempPathEdge = new PathEdges();
            
            List<PathEdgesPartition> Partitions = pathEdge.PathEdgesByLength;

            foreach (PathEdgesPartition part in Partitions)
            {
                Bdd tempBdd = Join(part.PathEdges, TransitionBdd, localVariables);

                tempPathEdge.AddPathEdge(new PathEdgesPartition(part.Length+1, tempBdd), m_BddManager);
                
               // tempBdd.FreeBdd();
            }

            return tempPathEdge;
        }

        private PathEdges SelfLoop(PathEdges pathEdge, Variables localVariables1, Variables localVariables2)
        {
            List<PathEdgesPartition> Partitions = pathEdge.PathEdgesByLength;
            PathEdges tempPathEdge = new PathEdges();
            //Bdd tempBdd;

            foreach (PathEdgesPartition part in Partitions)
            {
                //tempBdd = part.PathEdges;

                //part.PathEdges = SelfLoop(tempBdd, localVariables1, localVariables2);

                //tempBdd.FreeBdd();
                tempPathEdge.AddPathEdge(new PathEdgesPartition(part.Length, SelfLoop(part.PathEdges, localVariables1, localVariables2)), m_BddManager);
            }

            //return pathEdge;
            return tempPathEdge;
        }

    }
}
