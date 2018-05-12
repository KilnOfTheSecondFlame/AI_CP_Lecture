using Google.OrTools.ConstraintSolver;

namespace AI_CP_Lecture
{

    /*
     * Node Evaluator for Distances 
     */

    public abstract class Distance : NodeEvaluator2
    {

        public abstract int MapSize();

        public abstract string ToString(int node);

    }
}
